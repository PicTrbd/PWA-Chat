using System.Collections.Generic;
using ChatHexagone.Models;

namespace ChatHexagone.Adapters.RightSide
{
    public interface IDatabaseAdapter
    {
        void AddSubscription(PushSubscription subscription);
        List<PushSubscription> GetSubscriptions();
    }
}
