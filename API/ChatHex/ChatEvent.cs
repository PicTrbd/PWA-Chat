using System;
using System.Collections.Generic;
using ChatHexagone.Models;

namespace ChatHexagone
{
    public abstract class ChatEvent { }

    public class ChanelEvent : ChatEvent { }

    public class SubscriptionEvent : ChatEvent { }


    public class UserSubscriptionCreated : SubscriptionEvent
    {
        public Guid ClientId { get; set; }

        public UserSubscriptionCreated(Guid id)
            => ClientId = id;
    }

    public class ChanelsRetrieved : ChanelEvent
    {
        public List<Channel> Chanels { get; }

        public ChanelsRetrieved(List<Channel> channels)
            => Chanels = channels;
    }

    public class ChanelDetailsRetrieved : ChanelEvent
    {
        public Channel Channel { get; }

        public ChanelDetailsRetrieved(Channel channel)
            => Channel = channel;
    }

    public class UserChannelFounded : ChanelEvent
    {
        public Channel Channel { get; }

        public UserChannelFounded(Channel channel)
            => Channel = channel;
    }

}
