using System.Linq;
using ChatHexagone.Models;
using System.Collections.Generic;

namespace ApiCore
{
    public interface IDatabaseRepository
    {
        void AddSubscription(PushSubscription subscription);
        List<PushSubscription> GetSubscriptions();
        List<Channel> GetChanels();
        void TryCreateMainChanel();
        void CreateChannel(Channel channel);
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

        public List<PushSubscription> GetSubscriptions()
        {
            using (var db = new DataAccess())
            {
                var subscriptions = db.Set<PushSubscription>();
                return subscriptions.ToList();
            }
        }

        public List<Channel> GetChanels()
        {
            using (var db = new DataAccess())
            {
                var channels = db.Set<Channel>();
                return channels.ToList();
            }
        }

        public void TryCreateMainChanel()
        {
            using (var db = new DataAccess())
            {
                var channels = db.Set<Channel>();
                if (channels.All(x => x.ChannelName != "Main"))
                {
                    db.Chanels.Add(new Channel() { ChannelName = "Main", Messages = new List<Message>() });
                    db.SaveChanges();
                }
            }
        }

        public void CreateChannel(Channel channel)
        {
            using (var db = new DataAccess())
            {
                db.Chanels.Add(channel);
                db.SaveChanges();
            }
        }
    }
}