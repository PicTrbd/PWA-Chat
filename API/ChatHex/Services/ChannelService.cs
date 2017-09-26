using System;
using System.Linq;
using ChatHexagone.Models;
using System.Collections.Generic;

namespace ChatHexagone.Services
{
    public interface IChannelService
    {
        List<Channel> Chanels { get; set; }
        Channel GetChanel(string channelName);
        bool CreateChannel(string channelName);
        void RemoveUserFromChannel(string socketId);
        void AddMessageToChannel(string channelName, Message message);
        void AddUserToChanel(string room, Guid clientId, string socketId);
        Channel FindUserChannel(string socketId);
    }

    public class ChannelService : IChannelService
    {
        public List<Channel> Chanels { get; set; }

        public ChannelService()
            => Chanels = new List<Channel>();

        public Channel GetChanel(string channelName)
            => Chanels.FirstOrDefault(x => x.ChannelName == channelName);

        private Channel GetRoomFromName(string channelName)
            => Chanels.FirstOrDefault(x => x.ChannelName == channelName);

        public void RemoveUserFromChannel(string socketId)
            => Chanels.ForEach(r => r.Users.RemoveAll(x => x.SocketId == socketId));

        public void AddUserToChanel(string room, Guid clientId, string socketId)
        {
            Chanels.ForEach(channel => channel.Users.RemoveAll(u => u.ClientId == clientId || u.SocketId == socketId));
            GetChanel(room).Users.Add(new User(){ ClientId = clientId, SocketId = socketId});            
        }

        public Channel FindUserChannel(string socketId)
        {
            var channel = Chanels.Where(r => r.Users.Any(user => user.SocketId == socketId));
            return channel.Any() ? channel.First() : null;
        } 

        public void AddMessageToChannel(string channelName, Message message)
        {
            var channel = GetRoomFromName(channelName);
            channel.Messages.Add(message);
        }

        public bool CreateChannel(string channelName)
        {
            if (Chanels.Any(x => x.ChannelName == channelName))
                return false;

            var channel = new Channel()
            {
                ChannelName = channelName,
                Messages = new List<Message>(),
                Users = new List<User>()
            };

            Chanels.Add(channel);
            return true;
        } 


    }
}
