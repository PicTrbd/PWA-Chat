using System;
using System.Threading.Tasks;
using ApiCore.Infrastructure;
using ChatHexagone.Adapters.LeftSide;
using Microsoft.AspNetCore.SignalR;
using ChatHexagone.Models;
using Newtonsoft.Json;

namespace ApiCore
{
    public class ChatHub : Hub
    {
        private readonly IChannelAdapter _channelAdapter = Dependencies.Resolve<IChannelAdapter>();

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("User {0} is connected !", Context.ConnectionId);

            const string defaultRoom = "Main";
            var httpContext = Context.Connection.GetHttpContext();
            var userId = Guid.Parse(httpContext.Request.Query["UserId"]);
            Groups.AddAsync(Context.ConnectionId, defaultRoom);

            _channelAdapter.AddUserToChanel(defaultRoom, userId, Context.ConnectionId);

            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("User {0} is now disconnected !", Context.ConnectionId);

            _channelAdapter.FindUserAndRemoveItFromChannel(Context.ConnectionId);

            var channel = _channelAdapter.FindUserChannel(Context.ConnectionId);
            if (channel != null)
                Groups.RemoveAsync(Context.ConnectionId, channel.ChannelName);

            return base.OnDisconnectedAsync(exception);
        }

        public void GetAllChannels()
        {
            Clients.Client(Context.ConnectionId).InvokeAsync("RetrieveAllChannels", _channelAdapter.GetAllChanels());
        }

        public void GetChannelDetails(string name)
        {
            Clients.Client(Context.ConnectionId).InvokeAsync("RetrieveChannelDetails", _channelAdapter.GetChanelDetail(name));
        }

        public void CreateChannel(string roomName)
        {
            _channelAdapter.CreateChannel(roomName);
            Clients.All.InvokeAsync("RetrieveAllChannels",_channelAdapter.GetAllChanels());

            //const string iconUrl = "https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678092-sign-add-128.png";
            //var data = JsonConvert.SerializeObject(
            //    new
            //    {
            //        SocketId = roomName,
            //        ChannelOwner = $"Pablo ({Context.ConnectionId})",
            //        Icon = iconUrl
            //    });

            //Dependencies.NotificationsController.SendNotifications(data);
        }

        public async void JoinChannel(string oldRoom, string newRoom, Guid userId)
        {
            await Groups.RemoveAsync(Context.ConnectionId, oldRoom);
            await Groups.AddAsync(Context.ConnectionId, newRoom);

            _channelAdapter.AddUserToChanel(newRoom, userId, Context.ConnectionId);

            await Clients.Client(Context.ConnectionId).InvokeAsync("RetrieveChannelDetails", _channelAdapter.GetChanelDetail(newRoom));
            await Clients.All.InvokeAsync("RetrieveAllChannels", _channelAdapter.GetAllChanels());
        }

        public void SendMessage(string channelName, string json)
        {
            var message = JsonConvert.DeserializeObject<Message>(json);
            message.Date = DateTime.UtcNow;

            _channelAdapter.AddMessage(channelName, message);
            Clients.Group(channelName).InvokeAsync("AddMessage", message);
        }




    }
}
