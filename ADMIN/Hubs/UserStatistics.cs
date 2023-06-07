using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using BAL.Audit;
using DAL.AUDIT;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ADMIN.Hubs
{
    public class UserStatistics : Hub
    {
        private readonly AuditDbContext auditContext;
        public UserStatistics(AuditDbContext auditContext)
        {
            this.auditContext = auditContext;
        }

        // Shafi: Count current logged in users
        private static int Count = 0;
        public override Task OnConnectedAsync()
        {
            Count++;
            base.OnConnectedAsync();
            Clients.All.SendAsync("updateCount", Count);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Count--;
            base.OnDisconnectedAsync(exception);
            Clients.All.SendAsync("updateCount", Count);
            return Task.CompletedTask;
        }
        // End:


        public async Task NotificationAsync(string name, string message)
        {
            Count++;
            await base.OnConnectedAsync();
            var count = Task.CompletedTask;
            await Clients.Others.SendAsync("Notify", name, message, count);
        }
    }
}
