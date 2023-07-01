using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.LISTING;
using BAL.Services.Contracts;

namespace HUBS.Listing
{
    public class UsersOnlineOnListingHub : Hub
    {
        private readonly IUserService _userService;
        private readonly ListingDbContext listingManager;

        public UsersOnlineOnListingHub(IUserService userService, ListingDbContext listingManager)
        {
            this._userService = userService;
            this.listingManager = listingManager;
        }

        private static int count = 0;

        public async override Task OnConnectedAsync()
        {
            // Shafi: Get listing and get User ID
            string listingId = Context.GetHttpContext().Request.Query["listingId"];
            var listing = await listingManager.Listing.Where(x => x.ListingID == Int32.Parse(listingId)).FirstOrDefaultAsync();
            var userId = await _userService.GetUserById(listing.OwnerGuid);
            // End:

            count++;
            await base.OnConnectedAsync();
            await Clients.User(userId.UserName).SendAsync("UsersOnline", count);
            await Task.CompletedTask;
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            // Shafi: Get listing and get User ID
            string listingId = Context.GetHttpContext().Request.Query["listingId"];
            var listing = await listingManager.Listing.Where(x => x.ListingID == Int32.Parse(listingId)).FirstOrDefaultAsync();
            var userId = await _userService.GetUserById(listing.OwnerGuid);
            // End:

            count--;
            await base.OnDisconnectedAsync(exception);
            await Clients.All.SendAsync("UsersOnline", count);
            await Task.CompletedTask;
        }
    }
}
