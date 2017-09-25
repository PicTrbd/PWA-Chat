using System;

namespace ChatHexagone.Models
{
    public class Message
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
		public Guid Id { get; set; }
    }
}
