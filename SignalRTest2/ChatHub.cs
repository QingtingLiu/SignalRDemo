using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTest2
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        static List<CurrentUSer> ConnectedUsers = new List<CurrentUSer>();


        public void Connect(string url, string userId)
        {
            var id = Context.ConnectionId;
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new CurrentUSer() { ConnectionId = id, UserId = userId });
                Clients.Caller.onConnected(id, userId, url);
                Clients.Client(id).onNewUserConnected(id, userId);
            }
            else
            {
                Clients.Caller.onConnected(id, userId, url);
                Clients.Client(id).onExistUserConnected(id, userId);
            }
        }

        public void Exit(string userId)
        {
            var id = Context.ConnectionId;
            OnDisconnected(true);
            Clients.Caller.onConnected(id, userId, "");
            Clients.Client(id).onExit(id, userId);
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == id);
            if (item != null)
            {
                ConnectedUsers.Remove(item);
                Clients.All.onUserDisconnected(id, item.UserId);
            }
            return base.OnDisconnected(true);

        }
    }
}