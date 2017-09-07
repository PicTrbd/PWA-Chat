namespace Api.Model

open System
open System.Collections.Concurrent


type Message = {
    Id: Guid
    Sender: Guid 
    Date: DateTime
    Message: string
}

type MessageRepository = {
    Add : Message -> Option<Message>
    GetAll: unit -> seq<Message>    
}


module MessageRepositoryDb = 

    let private messages = new ConcurrentDictionary<Guid, Message>()

    let add msg = 
        let msgCopy = { msg with Id = Guid.NewGuid(); Date = DateTime.UtcNow.ToUniversalTime()}
        if messages.TryAdd(msgCopy.Id, msgCopy) then
            Some msgCopy
        else
            None

    let getAll () = 
        messages.Values |> Seq.cast

    let messageRepositoryDb = {
        Add = add
        GetAll = getAll
    }