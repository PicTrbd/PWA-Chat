using System;
using System.Threading.Tasks;
using ApiCore.Infrastructure;
using ChatHexagone.Adapters.LeftSide;
using Microsoft.AspNetCore.SignalR;

namespace ApiCore
{
    public class ChatHub : Hub
    {
        private readonly IChanelAdapter _chanelAdapter = Dependencies.Resolve<IChanelAdapter>();

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("User {0} is connected !", Context.ConnectionId);

            const string defaultRoom = "Main";

            Groups.AddAsync(Context.ConnectionId, defaultRoom);
            object clientId = "";
            if (Context.Connection.Metadata.TryGetValue("id", out clientId))
                _chanelAdapter.AddUserToChanel(defaultRoom, Guid.Parse(clientId as string), Context.ConnectionId);

            return base.OnConnectedAsync();
        }


        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    Console.WriteLine("User {0} is now disconnected !", Context.ConnectionId);

        //    _chatController.FindUserAndRemoveItFromRooms(Context.ConnectionId);

        //    var room = _chatController.GetUserRoomFromId(Context.ConnectionId);
        //    if (room != null)
        //        Groups.Remove(Context.ConnectionId, room);

        //    return base.OnDisconnectedAsync(exception);
        //}

        //public void GetAllRooms()
        //{
        //    Clients.Caller.RetrieveAllRooms(_chatController.GetRooms());
        //}

        //public void GetRoomDetails(string name)
        //{
        //    Clients.Caller.RetrieveRoomDetails(_chatController.GetRoom(name));
        //}

        //public void CreateRoom(string roomName)
        //{
        //    _chatController.AddRoom(roomName);
        //    Clients.All.RetrieveAllRooms(_chatController.GetRooms());

        //    const string iconUrl = "https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678092-sign-add-128.png";
        //    var data = JsonConvert.SerializeObject(
        //        new
        //        {
        //            ChannelName = roomName,
        //            ChannelOwner = $"Pablo ({Context.ConnectionId})",
        //            Icon = iconUrl
        //        });

        //    Dependencies.NotificationsController.SendNotifications(data);
        //}

        //public void JoinRoom(string oldRoom, string newRoom, Guid userId)
        //{
        //    Groups.Remove(Context.ConnectionId, oldRoom);
        //    Groups.Add(Context.ConnectionId, newRoom);

        //    _chatController.AddUserToRoom(newRoom, userId, Context.ConnectionId);

        //    Clients.Caller.RetrieveRoomDetails(_chatController.GetRoom(newRoom));
        //    Clients.All.RetrieveAllRooms(_chatController.GetRooms());
        //    Clients.Group(newRoom).NewUserJoinTheRoom(userId);
        //}

        //public void SendMessage(string roomName, string json)
        //{
        //    var message = _chatController.JsonToMessageModel(json);

        //    _chatController.AddMessageToRoom(roomName, message);

        //    Clients.Group(roomName).AddMessage(message);
        //}




    }
}
