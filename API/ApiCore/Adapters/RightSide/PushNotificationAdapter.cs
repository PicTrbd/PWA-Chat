using System;
using System.Collections.Generic;
using ChatHexagone.Adapters.RightSide;
using WebPush;
using PushSubscription = ChatHexagone.Models.PushSubscription;

namespace ApiCore.Adapters.RightSide
{
    public class PushNotificationAdapter : IPushNotificationAdapter
    {
        public void SendNotification(List<PushSubscription> clients, string data)
        {
            try
            {
                var vapidDetails = new VapidDetails(
                    @"mailto:paulmonnier75@gmail.com",
                    "BMiZDeWBmOzC1PVd4FFK5BKFzF36jzlfsOjq4kOLoDfnEgNIuubR1upxNBwgLm5b5c7RAHppSkG9V6ewntGvenw",
                    "BdmgRXaRCRI8diBnzu91edH9TTDsfX-9gq9SE9RkCHg");

                var webPushClient = new WebPushClient();

                clients.ForEach(x 
                    => webPushClient.SendNotification(new WebPush.PushSubscription(x.Endpoint, x.P256dh, x.Auth), data,vapidDetails));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
