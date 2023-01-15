using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Infrastracture
{
    public interface INotificationClient
    {
        Task ReceiveMessage(string user, string message);
        Task Connected(string connectionId);
        Task OnlineUsers(IEnumerable<string> connections);
        Task SendAsync(string title, object viewModel);
    }
}
