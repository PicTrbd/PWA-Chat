using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Api_v2.Models
{
    public class RoomModel
    {
        public string RoomName { get; set; }
        public HashSet<Tuple<string, Guid>> Users { get; set; }
        private ConcurrentBag<MessageModel> _messages;

        public ConcurrentBag<MessageModel> Messages
        {
            get => new ConcurrentBag<MessageModel>(_messages.OrderBy(x => x.Date).ToList());
            set => _messages = value;
        }


    }
}
