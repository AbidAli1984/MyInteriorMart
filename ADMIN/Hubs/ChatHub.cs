using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ADMIN.Hubs
{
    public class ChatHub : Hub
    {
        // Shafi: Count current logged in users
        private static int Count = 0;
        //public override Task OnConnectedAsync()
        //{
        //    Count++;
        //    base.OnConnectedAsync();
        //    Clients.All.SendAsync("updateCount", Count);
        //    return Task.CompletedTask;
        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    Count--;
        //    base.OnDisconnectedAsync(exception);
        //    Clients.All.SendAsync("updateCount", Count);
        //    return Task.CompletedTask;
        //}
        // End:

        public async Task SendMessage(string user, string message)
        {
            Count++;
            await base.OnConnectedAsync();
            var count = Task.CompletedTask;

            await Clients.All.SendAsync("ReceiveMessage", user, message, count);
        }

        public async Task Shafi()
        {
            string message = "Shafi Shaikh";

            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
