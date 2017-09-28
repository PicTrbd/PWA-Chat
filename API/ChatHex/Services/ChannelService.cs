using System;
using System.Linq;
using ChatHexagone.Models;
using System.Collections.Generic;

namespace ChatHexagone.Services
{
    public interface IChannelService
    {
        List<Channel> Channels { get; set; }
        Channel GetChannel(string channelName);
        bool CreateChannel(string channelName);
        Channel FindUserChannel(string socketId);
        void RemoveUserFromChannel(string socketId);
        void AddMessageToChannel(string channelName, Message message);
        void AddUserToChannel(string room, Guid clientId, string socketId);
        List<User> GetChannelUsersWithoutTheSender(string channelName, Guid senderId);
    }

    public class ChannelService : IChannelService
    {
        public List<Channel> Channels { get; set; }

        public ChannelService()
            => Channels = new List<Channel>();

        public Channel GetChannel(string channelName)
            => Channels.FirstOrDefault(x => x.ChannelName == channelName);

        private Channel GetRoomFromName(string channelName)
            => Channels.FirstOrDefault(x => x.ChannelName == channelName);

        public void RemoveUserFromChannel(string socketId)
            => Channels.ForEach(r => r.Users.RemoveAll(x => x.SocketId == socketId));

        public List<User> GetChannelUsersWithoutTheSender(string channelName, Guid senderId)
            => Channels.FirstOrDefault(c => c.ChannelName == channelName)?.Users.Where(u => u.ClientId != senderId).ToList();

        public void AddUserToChannel(string room, Guid clientId, string socketId)
        {
            Channels.ForEach(channel => channel.Users.RemoveAll(u => u.ClientId == clientId || u.SocketId == socketId));
            GetChannel(room).Users.Add(new User() { ClientId = clientId, SocketId = socketId });
        }

        public Channel FindUserChannel(string socketId)
        {
            var channel = Channels.Where(r => r.Users.Any(user => user.SocketId == socketId));
            return channel.Any() ? channel.First() : null;
        }

        public void AddMessageToChannel(string channelName, Message message)
        {
            var channel = GetRoomFromName(channelName);
            channel.Messages.Add(message);
        }

        public bool CreateChannel(string channelName)
        {
            if (Channels.Any(x => x.ChannelName == channelName))
                return false;

            var channel = new Channel()
            {
                ChannelName = channelName,
                Messages = new List<Message>(),
                Users = new List<User>()
            };

            Channels.Add(channel);
            return true;
        }


    }
}
