using Api_v2.Controllers;

namespace Api_v2
{
    public static class Dependencies
    {
        public static ChatController ChatController { get; set; }
        public static PushNotificationsController NotificationsController { get; set; }
    }
}
