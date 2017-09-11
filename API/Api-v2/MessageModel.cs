using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_v2
{
    public class MessageModel
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
