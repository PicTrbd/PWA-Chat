using System;
using System.Threading.Tasks;
using ChatHexagone.Services;
using ChatHexagone.Adapters.RightSide;

namespace ChatHexagone
{
    public interface IChatDomainEntryPoint
    {
        void HandleActions(ChatAct act);
    }

    public class ChatDomainEntryPoint : IChatDomainEntryPoint
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IDatabaseAdapter _databaseAdapter;

        public ChatDomainEntryPoint(IDatabaseAdapter databaseAdapter)
        {
            _subscriptionService = new SubscriptionService();
            _databaseAdapter = databaseAdapter;
        }

        private void RetrieveSavedSubscriptions()
        {
            _subscriptionService.Subscriptions = _databaseAdapter.GetSubscriptions();
        }

        private void HandleChanelActions(ChanelAct act)
        {
            
        }

        private async void HandleSubscriptionActions(SubscribtionAct act)
        {
            if (act is CreateSubscription createSubscription)
            {
                await Task.Run(() => RetrieveSavedSubscriptions());
                if (_subscriptionService.AddSubscription(createSubscription.Subscription))
                    _databaseAdapter.AddSubscription(createSubscription.Subscription);          
            }
        }

        public void HandleActions(ChatAct act)
        {
            if (act is ChanelAct chanelAct)
                HandleChanelActions(chanelAct);
            else if (act is SubscribtionAct subscribtionAct)
                HandleSubscriptionActions(subscribtionAct);
            else
                throw new Exception("Act not handled");
        }
    }
}
