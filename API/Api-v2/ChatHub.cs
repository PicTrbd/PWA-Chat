using System;
using System.Threading.Tasks;
using Api_v2.Controllers;
using Api_v2.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Api_v2
{
    public class ChatHub : Hub
    {
        private readonly ChatController _chatController = Dependencies.ChatController;

        public override Task OnConnected()
        {
            Console.WriteLine("User {0} is connected !", Context.ConnectionId);

            const string defaultRoom = "Main";

            Groups.Add(Context.ConnectionId, defaultRoom);
            _chatController.AddUserToRoom(defaultRoom, Guid.Parse(Context.QueryString["id"]), Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("User {0} is now disconnected !", Context.ConnectionId);

            _chatController.FindUserAndRemoveItFromRooms(Context.ConnectionId);

            var room = _chatController.GetUserRoomFromId(Context.ConnectionId);
            if (room != null)
                Groups.Remove(Context.ConnectionId, room);

            return base.OnDisconnected(stopCalled);
        }

        public void GetAllRooms()
        {
            Clients.Caller.RetrieveAllRooms(_chatController.GetRooms());
        }

        public void GetRoomDetails(string name)
        {
            Clients.Caller.RetrieveRoomDetails(_chatController.GetRoom(name));
        }

        public void CreateRoom(string roomName)
        {
            _chatController.AddRoom(roomName);
            Clients.All.RetrieveAllRooms(_chatController.GetRooms());

            const string iconUrl = "https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678092-sign-add-128.png";
            var data = JsonConvert.SerializeObject(
                new
                {
					Type = "NewChannel",
                    ChannelName = roomName,
                    ChannelOwner = Context.ConnectionId,
                    Icon = iconUrl
                });

            Dependencies.NotificationsController.SendNotifications(data);
        }

        public void JoinRoom(string oldRoom, string newRoom, Guid userId)
        {
            Groups.Remove(Context.ConnectionId, oldRoom);
            Groups.Add(Context.ConnectionId, newRoom);

            _chatController.AddUserToRoom(newRoom, userId, Context.ConnectionId);

            Clients.Caller.RetrieveRoomDetails(_chatController.GetRoom(newRoom));
			Clients.All.RetrieveAllRooms(_chatController.GetRooms());
			Clients.Group(newRoom).NewUserJoinTheRoom(userId);
        }

        public void SendMessage(string roomName, string json)
        {
            var message = _chatController.JsonToMessageModel(json);

            _chatController.AddMessageToRoom(roomName, message);

            Clients.Group(roomName).AddMessage(message);

			const string iconUrl = "https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678092-sign-add-128.png";
			var data = JsonConvert.SerializeObject(
			new
			{
				Type = "NewMessage",
				Sender = message.UserId.ToString().Substring(0, 8),
				Icon = iconUrl
			});
			Dependencies.NotificationsController.SendNotifications(data);
		}

        


    }
}
