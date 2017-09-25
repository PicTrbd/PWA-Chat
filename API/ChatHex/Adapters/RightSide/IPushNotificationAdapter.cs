using System.Collections.Generic;
using ChatHexagone.Models;

namespace ChatHexagone.Adapters.RightSide
{
    public interface IPushNotificationAdapter
    {
        void SendNotification(List<PushSubscription> clients, string data);
    }
}
