using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulgarian_Apparel.Web.Hubs
{
    public class Chat : Hub
    {
        public void SendMessage(string message)
        {
            string chatMessage = $"Message from {Context.ConnectionId}: {message}";
            Clients.All.receivedMessage(chatMessage);
        }
    }
}