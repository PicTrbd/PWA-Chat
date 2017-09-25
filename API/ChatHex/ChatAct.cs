using ChatHexagone.Models;

namespace ChatHexagone
{
    public abstract class ChatAct { }

    public class SubscribtionAct : ChatAct { }

    public class ChanelAct : ChatAct { }



    public class CreateSubscription : SubscribtionAct
    {
        public PushSubscription Subscription { get; }

        public CreateSubscription(PushSubscription sub)
        {
            Subscription = sub;
        }
    }

}
