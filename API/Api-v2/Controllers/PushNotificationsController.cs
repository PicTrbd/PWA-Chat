using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WebPush;

namespace Api_v2.Controllers
{
    public class PushNotificationsController
    {

        private readonly List<Tuple<string, string, string>> _subscriptions;

        public PushNotificationsController()
        {
            _subscriptions = new List<Tuple<string, string, string>>();
        }

        public void AddClientSubscription(string endpoint, string p256Dh, string auth)
        {
            var isAlreadySubscribed = _subscriptions.Any(x => (x.Item1 == endpoint)
                                                           && (x.Item2 == p256Dh)
                                                           && (x.Item3 == auth));
            if (!isAlreadySubscribed)
                _subscriptions.Add(Tuple.Create(endpoint, p256Dh, auth));
        }

        public void SendNotifications(string json)
        {
            try
            {
                var vapidDetails = new VapidDetails(
                    @"mailto:paulmonnier75@gmail.com",
                    "BMiZDeWBmOzC1PVd4FFK5BKFzF36jzlfsOjq4kOLoDfnEgNIuubR1upxNBwgLm5b5c7RAHppSkG9V6ewntGvenw",
                    "BdmgRXaRCRI8diBnzu91edH9TTDsfX-9gq9SE9RkCHg");

                var webPushClient = new WebPushClient();

                _subscriptions.ForEach(x => webPushClient.SendNotification(
                    new PushSubscription(x.Item1, x.Item2, x.Item3),
                    json, vapidDetails));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


    }
}