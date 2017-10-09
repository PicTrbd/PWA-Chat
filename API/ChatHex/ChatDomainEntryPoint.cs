using System;
using ChatHexagone.Models;
using ChatHexagone.Services;
using System.Collections.Generic;
using System.Diagnostics;
using ChatHexagone.Adapters.RightSide;

namespace ChatHexagone
{
    public interface IChatDomainEntryPoint
    {
        ChatEvent HandleActions(ChatAct act);
    }

    public class ChatDomainEntryPoint : IChatDomainEntryPoint
    {
        private bool _isStartUp = true;
        private readonly IUserService _userService;
        private readonly IChannelService _channelService;
        private readonly IDatabaseAdapter _databaseAdapter;
        private readonly IPushNotificationAdapter _pushNotificationAdapter;

        public ChatDomainEntryPoint(IDatabaseAdapter databaseAdapter, IPushNotificationAdapter pushNotificationAdapter)
        {
            _userService = new UserService();
            _databaseAdapter = databaseAdapter;
            _channelService = new ChannelService();
            _pushNotificationAdapter = pushNotificationAdapter;
        }

        private void InitializeFirstLaunch()
        {
            _databaseAdapter.TryCreateMainChanel();
            RetrieveSavedSubscribedUsers();
            RetrieveSavedChanels();
            CheckIfPropertiesAreNull();
            _isStartUp = false;
        }

        private void CheckIfPropertiesAreNull()
        {
            foreach (var channel in _channelService.Channels)
            {
                if (channel.Users == null)
                    channel.Users = new List<User>();
                if (channel.Messages == null)
                    channel.Messages = new List<Message>();
            }
            if (_userService.Users == null)
                _userService.Users = new List<User>();
        }

        private void RetrieveSavedSubscribedUsers()
            => _userService.Users = _databaseAdapter.GetUsers();

        private void RetrieveSavedChanels()
            => _channelService.Channels = _databaseAdapter.GetChanels();

        private ChanelEvent HandleChanelActions(ChanelAct act)
        {
            if (act is GetAllChanels)
                return new ChanelsRetrieved(_channelService.Channels);
            if (act is RemoveUserFromChannel removeUserAct)
                _channelService.RemoveUserFromChannel(removeUserAct.SocketId);
            if (act is FindUserChannel findUserChannelAct)
                return new UserChannelFounded(_channelService.FindUserChannel(findUserChannelAct.SocketId));
            if (act is GetChanelDetails channelDetailAct)
                return new ChanelDetailsRetrieved(_channelService.GetChannel(channelDetailAct.ChanelName));
            if (act is CreateChannel createChannelAct)
                if (_channelService.CreateChannel(createChannelAct.ChannelName))
                    _databaseAdapter.CreateChannel(_channelService.GetChannel(createChannelAct.ChannelName));
            if (act is AddUserToChanel addUserToChannel)
            {
                _channelService.AddUserToChannel(addUserToChannel.ChanelName, addUserToChannel.UserId, addUserToChannel.UserSocketId);
                _channelService.MatchSubscribedUsersWithChannelUsers(_userService.Users);
            }
            if (act is AddMessageToChannel addMessageAct)
            {
                _channelService.AddMessageToChannel(addMessageAct.ChannelName, addMessageAct.Message);
                _databaseAdapter.AddMessageToChannel(addMessageAct.ChannelName, addMessageAct.Message);
                _channelService.MatchSubscribedUsersWithChannelUsers(_userService.Users);
                var channelUsers =
                    _channelService.GetChannelUsersWithoutTheSender(addMessageAct.ChannelName,
                        addMessageAct.Message.UserId);
                _pushNotificationAdapter.SendNewMessageNotification(channelUsers, addMessageAct.Message.UserId);
            }
            return null;
        }

        private SubscriptionEvent HandleSubscriptionActions(SubscribtionAct act)
        {
            if (act is CreateSubscription createSubscription)
            {
                var (subscriptionWasAdded, user) = _userService.AddUserSubscription(createSubscription.Subscription);
                if (subscriptionWasAdded)
                    _databaseAdapter.AddUser(user);
                return new UserSubscriptionCreated(user.ClientId);
            }
            return null;
        }

        public ChatEvent HandleActions(ChatAct act)
        {
            if (_isStartUp)
                InitializeFirstLaunch();
            if (act is ChanelAct channelAct)
                return HandleChanelActions(channelAct);
            if (act is SubscribtionAct subscribtionAct)
                return HandleSubscriptionActions(subscribtionAct);
            throw new Exception("Act not handled");
        }
    }
}
