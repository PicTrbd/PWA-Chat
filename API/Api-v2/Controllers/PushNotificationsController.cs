using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebPush;

namespace Api_v2.Controllers
{
    public class PushNotificationsController
    {
        //BJFz_q_sr7R96bNl8-pubhJccjL1e3M9SHWA8xu_hR6wQ04ZqlSMNOkdLvyqaJnKdWPlzNF10-IJDFqPSbIgAkU
        //iVCnBFupyQeiU9j2edOMbnilSozKPdtcSg51sy5i_HM


        public PushNotificationsController()
        {
            InitConnection();
        }

        private void InitConnection()
        {
            var vapidDetails = new VapidDetails(
                @"mailto:paulmonnier75@gmail.com",
                "BJFz_q_sr7R96bNl8-pubhJccjL1e3M9SHWA8xu_hR6wQ04ZqlSMNOkdLvyqaJnKdWPlzNF10-IJDFqPSbIgAkU",
                "iVCnBFupyQeiU9j2edOMbnilSozKPdtcSg51sy5i_HM");

            var data = JsonConvert.SerializeObject(new {Name = "Pablo", Message = "Thomas tu vas prendre chère"});

            var webPushClient = new WebPushClient();

            //Client data to fill PushSubscription Object
            var subscription = new PushSubscription();

            try
            {
                webPushClient.SendNotification(subscription, data, vapidDetails);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}
