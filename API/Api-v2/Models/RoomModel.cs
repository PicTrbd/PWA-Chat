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

        public ConcurrentBag<MessageModel> Messages
        {
            get => new ConcurrentBag<MessageModel>(Messages.OrderBy(x => x.Date).ToList());
            set => Messages = value;
        }


    }
}
