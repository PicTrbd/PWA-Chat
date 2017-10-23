using System;
using ChatHexagone;
using ChatHexagone.Models;
using ApiCore.Infrastructure;
using System.Collections.Generic;
using ChatHexagone.Adapters.LeftSide;

namespace ApiCore.Adapters.LeftSide
{
    public class ChannelAdapter : IChannelAdapter
    {
        private readonly IChatDomainEntryPoint _chatDomainEntryPoint;

        public ChannelAdapter()
            => _chatDomainEntryPoint = Dependencies.Resolve<IChatDomainEntryPoint>();

        public void AddMessage(string channel, Message message)
            => _chatDomainEntryPoint.HandleActions(new AddMessageToChannel(channel, message));

        public void CreateChannel(string channelName)
            => _chatDomainEntryPoint.HandleActions(new CreateChannel(channelName));

        public void AddUserToChanel(string channelName, Guid userId, string userSocketId)
            => _chatDomainEntryPoint.HandleActions(new AddUserToChanel(userId, channelName, userSocketId));

        public void FindUserAndRemoveItFromChannel(string socketId)
            => _chatDomainEntryPoint.HandleActions(new RemoveUserFromChannel(socketId));

        public Channel FindUserChannel(string socketId)
        {
            var channel = _chatDomainEntryPoint.HandleActions(new FindUserChannel(socketId));
            return (channel as UserChannelFounded)?.Channel;
        }

        public List<Channel> GetAllChanels()
        {
            var channelEvent = _chatDomainEntryPoint.HandleActions(new GetAllChanels());
            return (channelEvent as ChanelsRetrieved)?.Chanels;
        }

        public Channel GetChanelDetail(string roomName)
        {
            var channelDetailEvent = _chatDomainEntryPoint.HandleActions(new GetChanelDetails(roomName));
            return (channelDetailEvent as ChanelDetailsRetrieved)?.Channel;
        }
    }
}
