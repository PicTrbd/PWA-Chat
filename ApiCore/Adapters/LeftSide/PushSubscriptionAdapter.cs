using System;
using ChatHexagone;
using ChatHexagone.Models;
using ApiCore.Infrastructure;
using ChatHexagone.Adapters.LeftSide;

namespace ApiCore.Adapters.LeftSide
{
    public class PushSubscriptionAdapter : ISubscriptionAdapter
    {
        private readonly IChatDomainEntryPoint _chatDomainEntryPoint;

        public PushSubscriptionAdapter() 
            => _chatDomainEntryPoint = Dependencies.Resolve<IChatDomainEntryPoint>();

        public Guid AddNewPushSuscription(PushSubscription subscription)
        {
            var act = new CreateSubscription(subscription);
            return ((UserSubscriptionCreated) _chatDomainEntryPoint.HandleActions(act)).ClientId;
        }

    }
}
