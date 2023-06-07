using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HUBS.Admin
{
    public class UsersOnlineHomeHub : Hub
    {
        private static int count = 0;

        public override Task OnConnectedAsync()
        {
            count++;
            base.OnConnectedAsync();
            Clients.All.SendAsync("UsersOnline", count);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            count--;
            base.OnDisconnectedAsync(exception);
            Clients.All.SendAsync("UsersOnline", count);
            return Task.CompletedTask;
        }
    }
}