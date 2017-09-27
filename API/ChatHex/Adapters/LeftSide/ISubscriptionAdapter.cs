using System;
using ChatHexagone.Models;

namespace ChatHexagone.Adapters.LeftSide
{
    public interface ISubscriptionAdapter
    {
        Guid AddNewPushSuscription(PushSubscription subscription);
    }
}
