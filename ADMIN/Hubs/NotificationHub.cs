using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task Push()
        {
            string product = "Laptop";
            int amount = 2500;

            await Clients.All.SendAsync("SendNotification", product, amount);
        }
    }
}
