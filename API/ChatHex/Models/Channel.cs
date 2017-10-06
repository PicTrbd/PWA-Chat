using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ChatHexagone.Models
{
    public class Channel
    {
        [Key]
        public Guid Id { get; set; }

        private List<Message> _messages;

        public string ChannelName { get; set; }

        [NotMapped]
        public List<User> Users { get; set; }

        public List<Message> Messages
        {
            get
            {
                _messages = SortMessages();
                return _messages;
            }
            set => _messages = value;
        }

        private List<Message> SortMessages()
            => _messages.OrderBy(x => x.Date).ToList();

    }
}
