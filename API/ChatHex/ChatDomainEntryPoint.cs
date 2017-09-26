using System;
using System.Collections.Generic;
using System.Linq;
using ChatHexagone.Services;
using ChatHexagone.Adapters.RightSide;
using ChatHexagone.Models;

namespace ChatHexagone
{
    public interface IChatDomainEntryPoint
    {
        ChatEvents HandleActions(ChatAct act);
    }

    public class ChatDomainEntryPoint : IChatDomainEntryPoint
    {
        private bool _isStartUp = true;
        private readonly IChannelService _channelService;
        private readonly IDatabaseAdapter _databaseAdapter;
        private readonly ISubscriptionService _subscriptionService;

        public ChatDomainEntryPoint(IDatabaseAdapter databaseAdapter)
        {
            _subscriptionService = new SubscriptionService();
            _channelService = new ChannelService();
            _databaseAdapter = databaseAdapter;
        }

        private void InitializeFirstLaunch()
        {
            _databaseAdapter.TryCreateMainChanel();
            RetrieveSavedSubscriptions();
            RetrieveSavedChanels();
            CheckIfChannelsPropertiesAreNull();
            _isStartUp = false;
        }

        private void CheckIfChannelsPropertiesAreNull()
        {
            foreach (var channel in _channelService.Chanels)
            {
                if (channel.Users == null)
                    channel.Users = new List<User>();
                if (channel.Messages == null)
                    channel.Messages = new List<Message>();
            }
        }

        private void RetrieveSavedSubscriptions()
            => _subscriptionService.Subscriptions = _databaseAdapter.GetSubscriptions();

        private void RetrieveSavedChanels()
            => _channelService.Chanels = _databaseAdapter.GetChanels();

        private ChanelEvents HandleChanelActions(ChanelAct act)
        {
            if (act is GetAllChanels)
                return new ChanelsRetrieved(_channelService.Chanels);
            if (act is RemoveUserFromChannel removeUserAct)
                _channelService.RemoveUserFromChannel(removeUserAct.SocketId);
            if (act is FindUserChannel findUserChannelAct)
                return new UserChannelFounded(_channelService.FindUserChannel(findUserChannelAct.SocketId));
            if (act is AddMessageToChannel addMessageAct)
                _channelService.AddMessageToChannel(addMessageAct.ChannelName, addMessageAct.Message);
            if (act is GetChanelDetails channelDetailAct)
                return new ChanelDetailsRetrieved(_channelService.GetChanel(channelDetailAct.ChanelName));
            if (act is CreateChannel createChannelAct)
                if (_channelService.CreateChannel(createChannelAct.ChannelName))
                    _databaseAdapter.CreateChannel(_channelService.GetChanel(createChannelAct.ChannelName));
            if (act is AddUserToChanel addUserToChannel)
                _channelService.AddUserToChanel(addUserToChannel.ChanelName, addUserToChannel.UserId, addUserToChannel.UserSocketId);
            return null;
        }

        private void HandleSubscriptionActions(SubscribtionAct act)
        {
            if (act is CreateSubscription createSubscription)
                if (_subscriptionService.AddSubscription(createSubscription.Subscription))
                    _databaseAdapter.AddSubscription(createSubscription.Subscription);
        }

        public ChatEvents HandleActions(ChatAct act)
        {
            if (_isStartUp)
                InitializeFirstLaunch();
            if (act is ChanelAct channelAct)
                return HandleChanelActions(channelAct);
            if (act is SubscribtionAct subscribtionAct)
                HandleSubscriptionActions(subscribtionAct);
            else
                throw new Exception("Act not handled");
            return null;
        }
    }
}
