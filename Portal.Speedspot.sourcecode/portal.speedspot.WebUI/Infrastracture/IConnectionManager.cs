using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Infrastracture
{
    public interface IConnectionManager
    {
        void AddConnection(string userId, string connectionId);
        void RemoveConnection(string connectionId);
        HashSet<string> GetConncetions(string userId);
        IEnumerable<string> OnlineUsers { get; }
    }
}
