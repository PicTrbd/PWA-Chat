using System;

namespace ChatHexagone.Adapters.LeftSide
{
    public interface IChanelAdapter
    {
        void AddUserToChanel(string room, Guid userId, string connectionId);


    }
}
