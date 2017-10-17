using System;
using System.Linq;
using ChatHexagone.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiCore
{
    public interface IDatabaseRepository
    {
        void AddUser(User user);
        List<User> GetUsers();
        List<Channel> GetChanels();
        void TryCreateMainChanel();
        void CreateChannel(Channel channel);
        void AddMessageToChannel(string channel, Message message);
    }

    public class DatabaseRepository : IDatabaseRepository
    {
        public DatabaseRepository()
        {
            new DataAccess().Database.EnsureCreated();
        }

        public void AddUser(User user)
        {
            try
            {
                using (var db = new DataAccess())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error on subscribe user");
            }
        }

        public List<User> GetUsers()
        {
            using (var db = new DataAccess())
            {
                var users = db.Set<User>()
                    .Include(u => u.PushSubscription);
                return users.ToList();
            }
        }

        public List<Channel> GetChanels()
        {
            using (var db = new DataAccess())
            {
                var channels = db.Set<Channel>()
                    .Include(c => c.Messages);
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

        public void AddMessageToChannel(string channel, Message message)
        {
            using (var db = new DataAccess())
            {
                var channels = db.Chanels.Include(c => c.Messages).SingleOrDefault(x => x.ChannelName == channel);
                channels.Messages.Add(message);
                db.SaveChanges();
            }
        }
    }
}