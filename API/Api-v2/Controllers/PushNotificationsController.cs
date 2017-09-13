using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPush;

namespace Api_v2.Controllers
{
    public class PushNotificationsController
    {
        public PushNotificationsController()
        {
            
        }

        private void InitConnection()
        {
            var pushEndpoint = "";
            var p256dh = "";
            var auth = "";

            var subscription = new PushSubscription();
        }


    }
}
