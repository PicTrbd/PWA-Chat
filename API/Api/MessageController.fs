namespace Api.Controller

open Suave.RequestErrors
open Suave.Writers
open Suave.Successful
open Suave
open Suave.WebSocket
open Suave.Sockets
open Suave.Sockets.Control
open Newtonsoft.Json
open Api.Model
open Suave.Filters
open Suave.Operators
open Newtonsoft.Json.Serialization
open Suave.Sockets.Control.SocketMonad

module Controller = 

    let fromJson<'a> json =
        let obj = JsonConvert.DeserializeObject(json, typeof<'a>) 
        if isNull obj then
            None
        else
            Some(obj :?> 'a)

    let getResourceFromReq<'a> (req : HttpRequest) =
        let getString rawForm =
            System.Text.Encoding.UTF8.GetString(rawForm)
        req.rawForm |> getString |> fromJson<'a>

    let JSON value =
        let settings = new JsonSerializerSettings()
        settings.ContractResolver <- new CamelCasePropertyNamesContractResolver()
        JsonConvert.SerializeObject(value, settings)
            |> Successful.OK
            >=> Writers.setMimeType "application/json; charset=utf-8"

    let handleResource f requestError = function
        | Some r -> r |> f
        | _ -> requestError

    let handleResourceBADREQUEST = 
        (fun f -> handleResource f (RequestErrors.BAD_REQUEST "No Resource from request"))

    let handleResourceNOTFOUND = 
        (fun f -> handleResource f (RequestErrors.NOT_FOUND "Resource not found"))

    let handleResourceCONFLICT = 
        (fun f -> handleResource f (RequestErrors.CONFLICT "Resource already exists"))

    

module MessageController = 
    open System.Text

    let getAll db =
        warbler (fun _ -> db.GetAll() |> Controller.JSON)

    let add db =
        let addDb = db.Add >> (Controller.handleResourceCONFLICT Controller.JSON)
        request (Controller.getResourceFromReq >> (Controller.handleResourceBADREQUEST addDb))        

    let initWebSocket (webSocket: WebSocket) (context: HttpContext) = 
        socket {
            let mutable loop = true
            
            while loop do 
                let! msg = webSocket.read()

                match msg with 
                | (Text, data, true) ->
                    let str = UTF8Encoding.UTF8.GetString data
                    let response = sprintf "response to %s" str
                    let byteResponse = 
                        response 
                        |> System.Text.Encoding.ASCII.GetBytes 
                        |> ByteSegment
                    do! webSocket.send Text byteResponse true
                
                | (Close, _, _) ->
                    let emptyResponse = [||] |> ByteSegment
                    do! webSocket.send Close emptyResponse true
                    loop <- false
                
                | _ -> ()       
        }

    let setCORSHeaders =
        addHeader  "Access-Control-Allow-Origin" "*" 
        >=> addHeader "Access-Control-Allow-Headers" "*" 
        >=> addHeader "Access-Control-Allow-Methods" "GET,POST,PUT"

    let messageController (db:MessageRepository) = 
        pathStarts "/api" >=> choose [ 
            path "/api/webSocket" >=> handShake initWebSocket
            POST >=> setCORSHeaders >=> path "/api/message" >=> (add db)
            GET >=> setCORSHeaders >=> path "/api/messages" >=> (getAll db)
            NOT_FOUND "Route not found"
        ]