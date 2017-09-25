using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_v2.Models;
using Api_v2.Repositories;
using WebPush;

namespace Api_v2.Controllers
{
    public class PushNotificationsController
    {
        //private readonly List<(string, string, string DSDDF)> _subscriptions;
        private readonly IPushSubscriptionRepository _repository;

        public PushNotificationsController()
        {
            //_subscriptions = new List<Tuple<string, string, string>>();
            _repository = new PushSubscriptionRepository();
        }

        public async Task AddClientSubscription(PushSubscriptionModel subscription)
        {
            IEnumerable<PushSubscriptionModel> subscriptions = null;

            await Task.Run(() =>
            {
                subscriptions = _repository.GetSubscriptions();
            });

            var isAlreadySubscribed = subscriptions != null && subscriptions
                                          .Any(x => (x.Endpoint == subscription.Endpoint)
                                                    && (x.P256Dh == subscription.P256Dh)
                                                    && (x.Auth == subscription.Auth));
            //overide Equals
            //var isAlreadySubscribed = _subscriptions.Any(x => (x.Item1 == endpoint)
            //                                               && (x.Item2 == p256Dh)
            //                                               && (x.Item3 == auth));
            if (!isAlreadySubscribed)
                _repository.AddSubscription(subscription);
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

                //_subscriptions.ForEach(x => webPushClient.SendNotification(
                //    new PushSubscription(x.Item1, x.Item2, x.Item3),
                //    json, vapidDetails));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


    }
}