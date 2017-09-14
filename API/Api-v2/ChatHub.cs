using System;
using System.Threading.Tasks;
using Api_v2.Controllers;
using Api_v2.Models;
using Microsoft.AspNet.SignalR;

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

            return base.OnDisconnected(stopCalled);
        }

        public void RetrieveRoomDetails(string name)
        {
            Clients.Caller.GetRoomDetails(_chatController.GetRoom(name));
        }

        public void CreateRoom(string roomName)
        {
            _chatController.AddRoom(roomName);

            Clients.All.AddRoom(_chatController.GetRooms());
        }

        public void JoinRoom(string oldRoom, string newRoom, Guid userId)
        {
            Groups.Remove(Context.ConnectionId, oldRoom);
            Groups.Add(Context.ConnectionId, newRoom);

            _chatController.AddUserToRoom(newRoom, userId, Context.ConnectionId);

            Clients.Caller.GetRoomDetails(_chatController.GetRoom(newRoom));
            Clients.Group(newRoom).NewUserJoinTheRoom(userId);
        }

        public void SendMessage(string roomName, string json)
        {
            var message = _chatController.JsonToMessageModel(json);

            _chatController.AddMessageToRoom(roomName, message);

            Clients.Group(roomName).AddMessage(message);

            Dependencies.NotificationsController.SendNotifications();
        }

        


    }
}
