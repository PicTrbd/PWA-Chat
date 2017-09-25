using System;
using Api_v2.Models;

namespace Api_v2.Repositories
{
    public interface IChatRepository
    {
        void AddMessage(MessageModel message);

    }

    public class SqlChatRepository : IChatRepository
    {
        public SqlChatRepository()
        {
            
        }

        public void AddMessage(MessageModel message)
        {
            throw new NotImplementedException();
        }
    }
}
