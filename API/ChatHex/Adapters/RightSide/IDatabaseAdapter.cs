using System.Collections.Generic;
using ChatHexagone.Models;

namespace ChatHexagone.Adapters.RightSide
{
    public interface IDatabaseAdapter
    {
        void AddSubscription(PushSubscription subscription);
        List<PushSubscription> GetSubscriptions();
        void CreateChannel(Channel channel);
        void TryCreateMainChanel();
        List<Channel> GetChanels();
    }
}
