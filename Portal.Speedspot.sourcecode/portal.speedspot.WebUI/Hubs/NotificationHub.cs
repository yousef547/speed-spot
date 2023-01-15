using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Hubs
{
    [Authorize]
    public class NotificationHub : Hub<INotificationClient>
    {
        private IConnectionManager _manager;
        private readonly ApplicationUserManager _userManager;

        public NotificationHub(IConnectionManager manager,
            ApplicationUserManager userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }

        public async Task GetConnectionId()
        {
            await Clients.Client(Context.ConnectionId).Connected(Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).OnlineUsers(_manager.OnlineUsers);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            _manager.AddConnection((await _userManager.GetUserAsync(httpContext.User)).Id, Context.ConnectionId);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _manager.RemoveConnection(Context.ConnectionId);

            return Task.CompletedTask;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }
    }
}
