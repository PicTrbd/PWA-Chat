using System;
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

    public class GetAllChanels : ChanelAct { }

    public class GetChanelDetails : ChanelAct
    {
        public string ChanelName { get; set; }

        public GetChanelDetails(string channelName)
        {
            ChanelName = channelName;
        }
    }

    public class AddUserToChanel : ChanelAct
    {
        public Guid UserId { get; set; }
        public string ChanelName { get; set; }
        public string UserSocketId { get; set; }

        public AddUserToChanel(Guid userId, string channelName, string userSocketId)
        {
            UserId = userId;
            ChanelName = channelName;
            UserSocketId = userSocketId;
        }

    }

    public class AddMessageToChannel : ChanelAct
    {
        public string ChannelName { get; set; }
        public Message Message { get; set; }

        public AddMessageToChannel(string channelName, Message message)
        {
            ChannelName = channelName;
            Message = message;
        }

    }

    public class CreateChannel : ChanelAct
    {
        public string ChannelName { get; set; }

        public CreateChannel(string channelName)
            => ChannelName = channelName;
    }

    public class RemoveUserFromChannel : ChanelAct
    {
        public string SocketId { get; set; }

        public RemoveUserFromChannel(string socketId)
            => SocketId = socketId;
    }

    public class FindUserChannel : ChanelAct
    {
        public string SocketId { get; set; }

        public FindUserChannel(string socketId)
            => SocketId = socketId;
    }


}
