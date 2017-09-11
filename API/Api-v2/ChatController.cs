using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api_v2
{
    public class ChatController
    {
        public ConcurrentBag<MessageModel> Messages { get; set; }

        public ChatController()
        {
            Messages = new ConcurrentBag<MessageModel>();
        }

        public MessageModel AddMessage(string json)
        {
            var msg = FormatMessageFromJson(json);
            Messages.Add(msg);

            return msg;
        }

        private MessageModel FormatMessageFromJson(string jsonMsg)
        {
            var message = JsonConvert.DeserializeObject<MessageModel>(jsonMsg);
            message.Date = DateTime.UtcNow;

            return message;
        }

    }
}
