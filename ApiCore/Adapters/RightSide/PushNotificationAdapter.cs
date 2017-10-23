using System;
using WebPush;
using ChatHexagone.Models;
using System.Collections.Generic;
using System.Text;
using ChatHexagone.Adapters.RightSide;
using Newtonsoft.Json;
using PushSubscription = WebPush.PushSubscription;

namespace ApiCore.Adapters.RightSide
{
    public class PushNotificationAdapter : IPushNotificationAdapter
    {
        public void SendNewMessageNotification(List<User> users, Guid senderId)
        {
            if (users == null || senderId == null)
                throw new ArgumentException("One of your paramater is null");

            var data = JsonConvert.SerializeObject(new {From = senderId.ToString() });

            var vapidDetails = new VapidDetails(
                @"mailto:paulmonnier75@gmail.com",
                "BMiZDeWBmOzC1PVd4FFK5BKFzF36jzlfsOjq4kOLoDfnEgNIuubR1upxNBwgLm5b5c7RAHppSkG9V6ewntGvenw",
                "BdmgRXaRCRI8diBnzu91edH9TTDsfX-9gq9SE9RkCHg");

            var webPushClient = new WebPushClient();

            users.ForEach(x => webPushClient.SendNotification(
                new PushSubscription(x.PushSubscription.Endpoint, x.PushSubscription.P256dh, x.PushSubscription.Auth)
                , data, vapidDetails));
        }
    }
}
