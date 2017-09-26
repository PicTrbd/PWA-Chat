using System;

namespace ChatHexagone.Models
{
    public class Message
    {
		public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
