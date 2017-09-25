    using System.ComponentModel.DataAnnotations;

namespace Api_v2.Models
{
    public class PushSubscriptionModel
    {
        [Key]
        public int Id { get; set; }
        public string Auth { get; set; }
        public string P256Dh { get; set; }
        public string Endpoint { get; set; }
        public object ExpirationTime { get; set; }
    }

    public class Keys
    {
        public string p256dh { get; set; }
        public string auth { get; set; }
    }

    public class RootObject
    {
        public int Id { get; set; }
        public string endpoint { get; set; }
        public object expirationTime { get; set; }
        public Keys keys { get; set; }
    }

}
