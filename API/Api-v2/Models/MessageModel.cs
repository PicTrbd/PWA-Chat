using System;

namespace Api_v2.Models
{
    public class MessageModel
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
		public Guid Id { get; set; }
    }
}
