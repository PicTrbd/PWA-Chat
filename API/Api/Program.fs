open Api.Model
open Suave.Web
open Api.Controller
open MessageController
open MessageRepositoryDb

[<EntryPoint>]
let main argv =
  startWebServer defaultConfig (messageController messageRepositoryDb)
  0