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

        public void SendNotifications()
        {
            try
            {
                //var vapidDetails = VapidHelper.GenerateVapidKeys();
                //vapidDetails.Subject = "Test";
                var vapidDetails = new VapidDetails(
                    @"mailto:paulmonnier75@gmail.com",
                    "BJFz_q_sr7R96bNl8-pubhJccjL1e3M9SHWA8xu_hR6wQ04ZqlSMNOkdLvyqaJnKdWPlzNF10-IJDFqPSbIgAkU",
                    "iVCnBFupyQeiU9j2edOMbnilSozKPdtcSg51sy5i_HM");

                var data = JsonConvert.SerializeObject(new { Name = "Pablo", Message = "Et sa jolie Cyntia !" });

                var webPushClient = new WebPushClient();
                _subscriptions.ForEach(x => webPushClient.SendNotification(new PushSubscription(x.Item1, x.Item2, x.Item3), data, vapidDetails));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


    }
}




//BJFz_q_sr7R96bNl8-pubhJccjL1e3M9SHWA8xu_hR6wQ04ZqlSMNOkdLvyqaJnKdWPlzNF10-IJDFqPSbIgAkU
//iVCnBFupyQeiU9j2edOMbnilSozKPdtcSg51sy5i_HM