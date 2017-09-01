namespace API.MessageModel

open System
open System.Collections.Concurrent


type Message = {
    Id: Guid
    Date: DateTime
    Sender: string 
    Message: string
}

type MessageRepository = {
    Add : Message -> Option<Message>
    GetAll: unit -> seq<Message>    
}


module MessageRepositoryDb = 

    let private messages = new ConcurrentDictionary<Guid, Message>()

    let add msg = 
        let msgCopy = { msg with Id = Guid.NewGuid() }
        if messages.TryAdd(msgCopy.Id, msgCopy) then
            Some(msgCopy)
        else
            None

    let getAll () = 
        messages.Values |> Seq.cast