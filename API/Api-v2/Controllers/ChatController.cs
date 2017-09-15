using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Api_v2.Models;
using Newtonsoft.Json;

namespace Api_v2.Controllers
{
    public class ChatController
    {
        private readonly List<RoomModel> _rooms;

        public ChatController()
        {
            _rooms = new List<RoomModel>()
            {
                new RoomModel()
                {
                    RoomName = "Main",
                    Messages = new List<MessageModel>(),
                    Users = new HashSet<Tuple<string, Guid>>()
                },
				new RoomModel()
				{
					RoomName = "Kid Channel",
					Messages = new List<MessageModel>(),
					Users = new HashSet<Tuple<string, Guid>>()
				}
            };
        }

        private RoomModel GetRoomFromName(string roomName)
        {
            return _rooms.FirstOrDefault(x => x.RoomName == roomName);
        }

        public List<RoomModel> GetRooms()
        {
            return _rooms;
        }

        public RoomModel GetRoom(string roomName)
        {
            return _rooms.FirstOrDefault(x => x.RoomName == roomName);
        }

        public void AddMessageToRoom(string room, MessageModel message)
        {
            var channel = GetRoomFromName(room);
            channel.Messages.Add(message);
        }

        public string GetUserRoomFromId(string connectionId)
        {
            var rooms = _rooms.Where(r => r.Users.Any(user => user.Item1 == connectionId));
            return rooms.Any() ? rooms.First().RoomName : null;
        }

        public void AddUserToRoom(string room, Guid userId, string clientId)
        {
            _rooms.ForEach(r => r.Users.RemoveWhere(x => x.Item2 == userId || x.Item1 == clientId));

            GetRoomFromName(room).Users.Add(new Tuple<string, Guid>(clientId, userId));
        }

        public void RemoveUserFromRoom(string room, string clientId)
        {
            var client = GetRoomFromName(room).Users.FirstOrDefault(x => x.Item1 == clientId);
            GetRoomFromName(room).Users.Remove(client);
        }

        public void FindUserAndRemoveItFromRooms(string clientId)
        {
            _rooms.ForEach(r => r.Users.RemoveWhere(x => x.Item1 == clientId));
        }

        public MessageModel JsonToMessageModel(string json)
        {
            var message = JsonConvert.DeserializeObject<MessageModel>(json);
            message.Date = DateTime.UtcNow;

            return message;
        }

        public void AddRoom(string roomName)
        {
            _rooms.Add(new RoomModel()
            {
                RoomName = roomName,
                Messages = new List<MessageModel>(),
                Users = new HashSet<Tuple<string, Guid>>()
            });
        }

    }
}
