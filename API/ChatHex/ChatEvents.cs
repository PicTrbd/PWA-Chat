using System.Collections.Generic;
using ChatHexagone.Models;

namespace ChatHexagone
{
    public abstract class ChatEvents { }

    public class ChanelEvents : ChatEvents { }


    public class ChanelsRetrieved : ChanelEvents
    {
        public List<Channel> Chanels { get; }

        public ChanelsRetrieved(List<Channel> channels)
            => Chanels = channels;
    }

    public class ChanelDetailsRetrieved : ChanelEvents
    {
        public Channel Channel { get; }

        public ChanelDetailsRetrieved(Channel channel)
            => Channel = channel;
    }

    public class UserChannelFounded : ChanelEvents
    {
        public Channel Channel { get; }

        public UserChannelFounded(Channel channel)
            => Channel = channel;
    }

}
