using ApiCore.Infrastructure;
using ChatHexagone;
using ChatHexagone.Adapters.LeftSide;
using ChatHexagone.Models;

namespace ApiCore.Adapters.LeftSide
{
    public class PushSubscriptionAdapter : ISubscriptionAdapter
    {
        private readonly IChatDomainEntryPoint _chatDomainEntryPoint;

        public PushSubscriptionAdapter() 
            => _chatDomainEntryPoint = Dependencies.Resolve<IChatDomainEntryPoint>();

        public void AddNewPushSuscription(PushSubscription subscription)
        {
            var act = new CreateSubscription(subscription);
            _chatDomainEntryPoint.HandleActions(act);
        }

    }
}
