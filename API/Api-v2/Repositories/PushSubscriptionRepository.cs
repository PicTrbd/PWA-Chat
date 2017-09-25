using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Api_v2.Models;

namespace Api_v2.Repositories
{
    public interface IPushSubscriptionRepository
    {
        void AddSubscription(PushSubscriptionModel subscription);
        IEnumerable<PushSubscriptionModel> GetSubscriptions();
    }

    public class PushSubscriptionRepository : IPushSubscriptionRepository
    {
        public void AddSubscription(PushSubscriptionModel subscription)
        {
            using (var db = new DataAccess())
                try
                {
                    db.Subscriptions.AddOrUpdate(subscription);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        public IEnumerable<PushSubscriptionModel> GetSubscriptions()
        {
            using (var db = new DataAccess())
            {
                try
                {
                    return db.Subscriptions.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    db.Subscriptions.Create();
                    db.SaveChanges();
                    return new List<PushSubscriptionModel>();
                }
            }
        }

    }
}
