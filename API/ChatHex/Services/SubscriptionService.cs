using System.Collections.Generic;
using ChatHexagone.Models;

namespace ChatHexagone.Services
{
    public interface ISubscriptionService
    {
        bool AddSubscription(PushSubscription subscriptions);
        List<PushSubscription> Subscriptions { get; set; }
    }

    public class SubscriptionService : ISubscriptionService
    {
        public List<PushSubscription> Subscriptions { get; set; }

        public SubscriptionService()
        {
            Subscriptions = new List<PushSubscription>();
        }

        public bool AddSubscription(PushSubscription subscription)
        {
            if (Subscriptions.Contains(subscription))
                return false;

            Subscriptions.Add(subscription);
            return true;
        }

    }
}
