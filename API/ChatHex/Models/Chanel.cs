using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatHexagone.Models
{
    public class Chanel
    {
        public Guid Id { get; set; }
        private List<Message> _messages;
        public string RoomName { get; set; }
        public List<User> Users { get; set; }

        //Trier ailleurs
        public List<Message> Messages
        {
            get => _messages = _messages.OrderBy(x => x.Date).ToList();
            set => _messages = value;
        }


    }
}
