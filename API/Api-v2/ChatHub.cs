using System;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Api_v2
{
    public class ChatHub : Hub
    {
       
        public void SendMessage(string json)
        {
            try
            {
                var formatedMsg = Dependencies.ChatController.AddMessage(json);
                Clients.All.addMessage(JsonConvert.SerializeObject(formatedMsg));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error on SendMessage => {0}", e);
            }
        }
    }
}
