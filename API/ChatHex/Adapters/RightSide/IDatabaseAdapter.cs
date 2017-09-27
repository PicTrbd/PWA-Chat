using System.Collections.Generic;
using ChatHexagone.Models;

namespace ChatHexagone.Adapters.RightSide
{
    public interface IDatabaseAdapter
    {
        void AddUser(User user);
        List<User> GetUsers();
        void CreateChannel(Channel channel);
        void TryCreateMainChanel();
        List<Channel> GetChanels();
        void AddMessageToChannel(string channel, Message message);
    }
}
