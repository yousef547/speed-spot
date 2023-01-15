using portal.speedspot.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Helpers
{
    public class ConnectionManager : IConnectionManager
    {
        private static Dictionary<string, HashSet<string>> userMap = new Dictionary<string, HashSet<string>>();

        public IEnumerable<string> OnlineUsers { get { return userMap.Keys; } }

        public void AddConnection(string userId, string connectionId)
        {
            lock (userMap)
            {
                if (!userMap.ContainsKey(userId))
                {
                    userMap[userId] = new HashSet<string>();
                }
                userMap[userId].Add(connectionId);
            }
        }

        public void RemoveConnection(string connectionId)
        {
            lock (userMap)
            {
                foreach (var userId in userMap.Keys)
                {
                    if (userMap.ContainsKey(userId))
                    {
                        if (userMap[userId].Contains(connectionId))
                        {
                            userMap[userId].Remove(connectionId);
                            break;
                        }
                    }
                }
            }
        }

        public HashSet<string> GetConncetions(string userId)
        {
            var conn = new HashSet<string>();
            try
            {
                lock (userMap)
                {
                    conn = userMap[userId];
                }
            }
            catch
            {
                conn = null;
            }
            return conn;
        }
    }
}
