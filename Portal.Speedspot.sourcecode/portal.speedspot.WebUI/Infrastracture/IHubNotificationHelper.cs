using portal.speedspot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Infrastracture
{
    public interface IHubNotificationHelper
    {
        Task SendNotificationToAll(string message);
        IEnumerable<string> GetOnlineUsers();
        Task<Task> SendNotificationParallel(string username);
        Task SendNotification(NotificationViewModel model);
    }
}
