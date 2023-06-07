using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using IDENTITY.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Web;
using BAL.Addresses;
using BOL.VIEWMODELS.Hub;
using Microsoft.VisualBasic.CompilerServices;
using DAL.LISTING;
using Microsoft.AspNetCore.Connections;

namespace HUBS.Listing
{
    public class UsersOnlineOnListingHub : Hub
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IAddresses addressManager;
        private readonly ListingDbContext listingManager;

        public UsersOnlineOnListingHub(UserManager<IdentityUser> userManager, ApplicationDbContext context, IAddresses addressManager, ListingDbContext listingManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.addressManager = addressManager;
            this.listingManager = listingManager;
        }

        private static int count = 0;

        public async override Task OnConnectedAsync()
        {
            // Shafi: Get listing and get User ID
            string listingId = Context.GetHttpContext().Request.Query["listingId"];
            var listing = await listingManager.Listing.Where(x => x.ListingID == Int32.Parse(listingId)).FirstOrDefaultAsync();
            var userId = await userManager.FindByIdAsync(listing.OwnerGuid);
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
            var userId = await userManager.FindByIdAsync(listing.OwnerGuid);
            // End:

            count--;
            await base.OnDisconnectedAsync(exception);
            await Clients.All.SendAsync("UsersOnline", count);
            await Task.CompletedTask;
        }
    }
}
