using ChatHexagone.Models;

namespace ChatHexagone.Adapters.LeftSide
{
    public interface ISubscriptionAdapter
    {
        void AddNewPushSuscription(PushSubscription subscription);
    }
}
