using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN.Hubs
{
    public class DashboardHub : Hub
    {
        private static int count = 0;

        public override Task OnConnectedAsync()
        {
            count++;
            base.OnConnectedAsync();
            Clients.All.SendAsync("UsersOnline", count);
            return Task.CompletedTask;
        }
    }
}
