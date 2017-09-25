using System.ComponentModel.DataAnnotations;

namespace ChatHexagone.Models
{
    public class PushSubscription
    {
        [Key]
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is PushSubscription subs)
                return Endpoint == subs.Endpoint 
                    && P256dh == subs.P256dh 
                    && Auth == subs.Auth;
            return false;
        }
    }

}
