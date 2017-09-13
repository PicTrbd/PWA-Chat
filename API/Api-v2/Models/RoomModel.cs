using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Api_v2.Models
{
    public class RoomModel
    {
        public string RoomName { get; set; }
        public HashSet<Tuple<string, Guid>> Users { get; set; }
        public ConcurrentBag<MessageModel> Messages { get; set; }
    }
}
