using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //Trier ailleurs
        public List<Message> Messages
        {
            get => _messages; /*= _messages.OrderBy(x => x.Date).ToList();*/
            set => _messages = value;
        }

    }
}
