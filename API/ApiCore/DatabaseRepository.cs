using System.Collections.Generic;
using System.Linq;
using ChatHexagone.Models;

namespace ApiCore
{
    public interface IDatabaseRepository
    {
        void AddSubscription(PushSubscription subscription);
        IEnumerable<PushSubscription> GetSubscriptions();
    }

    public class DatabaseRepository : IDatabaseRepository
    {
        public DatabaseRepository()
        {
            new DataAccess().Database.EnsureCreated();
        }

        public void AddSubscription(PushSubscription subscription)
        {
            using (var db = new DataAccess())
            {
                db.Subscriptions.Add(subscription);
                db.SaveChanges();
            }
        }

        public IEnumerable<PushSubscription> GetSubscriptions()
        {
            using (var db = new DataAccess())
            {
                var list = db.Set<PushSubscription>().ToList();
                return list;
            }
        }
    }
}
