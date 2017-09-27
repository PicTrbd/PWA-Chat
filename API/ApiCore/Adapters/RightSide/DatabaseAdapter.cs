using ChatHexagone.Models;
using System.Collections.Generic;
using ChatHexagone.Adapters.RightSide;

namespace ApiCore.Adapters.RightSide
{
    public class DatabaseAdapter : IDatabaseAdapter
    {
        private readonly IDatabaseRepository _databaseRepository;

        public DatabaseAdapter()
            => _databaseRepository = new DatabaseRepository();

        public void AddUser(User user)
            => _databaseRepository.AddUser(user);

        public List<User> GetUsers()
            => _databaseRepository.GetUsers();

        public List<Channel> GetChanels()
            => _databaseRepository.GetChanels();

        public void CreateChannel(Channel channel)
            => _databaseRepository.CreateChannel(channel);

        public void TryCreateMainChanel()
            => _databaseRepository.TryCreateMainChanel();

    }
}
