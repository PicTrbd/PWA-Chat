using System;
using ChatHexagone.Models;
using System.Collections.Generic;

namespace ChatHexagone.Adapters.LeftSide
{
    public interface IChannelAdapter
    {
        void AddUserToChanel(string channelName, Guid userId, string userSocketId);
        void AddMessage(string channel, Message message);
        Channel GetChanelDetail(string roomName);
        void CreateChannel(string channelName);
        List<Channel> GetAllChanels();
        void FindUserAndRemoveItFromChannel(string socketId);
        Channel FindUserChannel(string socketId);
    }
}
