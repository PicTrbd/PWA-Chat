using System;
using Api_v2.Models;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;

namespace Api_v2
{
    public class RestApiModule : NancyModule
    {
        public RestApiModule()
        {
            Post["/subscribe"] = param =>
            {
                Console.WriteLine("New user subscribed to notifications !");
                var subs = this.Bind<SubscriptionDetails>();
                Dependencies.NotificationsController.AddClientSubscription(subs.endpoint, subs.keys.p256dh, subs.keys.auth);
                return "OK";
            };
        }


    }
}
