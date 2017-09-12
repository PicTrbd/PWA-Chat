using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Api_v2.Models;
using Newtonsoft.Json;

namespace Api_v2
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
                    Messages = new ConcurrentBag<MessageModel>(),
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

        public MessageModel AddMessageToRoom(string room, MessageModel message)
        {
            message.Date = DateTime.UtcNow;

            GetRoomFromName(room)?.Messages.Add(message);

            return message;
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

        public void AddRoom(string roomName)
        {
            _rooms.Add(new RoomModel()
            {
                RoomName = roomName,
                Messages = new ConcurrentBag<MessageModel>(),
                Users = new HashSet<Tuple<string, Guid>>()
            });
        }

    }
}
