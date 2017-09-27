using System;
using System.Collections.Generic;
using System.Linq;
using ChatHexagone.Models;

namespace ChatHexagone.Services
{
    public interface IUserService
    {
        (bool, User) AddUserSubscription(PushSubscription subscriptions);
        List<User> Users { get; set; }
    }

    public class UserService : IUserService
    {
        public List<User> Users { get; set; }

        public UserService()
            => Users = new List<User>();

        public (bool, User) AddUserSubscription(PushSubscription subscription)
        {
            if (Users.Any(x => x.PushSubscription.Equals(subscription)))
                return (false, Users.FirstOrDefault(x => x.PushSubscription.Equals(subscription)));

            var user = new User() { ClientId = Guid.NewGuid(), PushSubscription = subscription };
            Users.Add(user);

            return (true, user);
        }

    }
}
