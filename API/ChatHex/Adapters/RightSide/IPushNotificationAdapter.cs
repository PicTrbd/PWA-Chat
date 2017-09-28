using System;
using System.Collections.Generic;
using ChatHexagone.Models;

namespace ChatHexagone.Adapters.RightSide
{
    public interface IPushNotificationAdapter
    {
        void SendNewMessageNotification(List<User> users, Guid senderId);
    }
}
