using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;
namespace Bulgarian_Apparel.Web.Hubs
{
    [HubName("discussionsHub")]
    public class CommunicationsHub : Hub
    {
        private readonly IUsersService usersService;

        public void Send(string name, string message, string connId)
        {
            Clients.Client(connId).appendNewMessage(name, message);
        }
    }
}