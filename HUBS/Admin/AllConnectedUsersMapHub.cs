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
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;
using MailKit.Net.Proxy;

namespace HUBS.Admin
{
    public class AllConnectedUsersMapHub : Hub
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IAddresses addressManager;

        public AllConnectedUsersMapHub(UserManager<IdentityUser> userManager, ApplicationDbContext context, IAddresses addressManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.addressManager = addressManager;
        }

        //public IList<LatitudeLongitudeViewModel> allUsers = new List<LatitudeLongitudeViewModel>();
        static HashSet<string> CurrentConnections = new HashSet<string>();

        public async override Task OnConnectedAsync()
        {

            // Shafi: Get client location details using https://geolocation-db.com/ free service with unlimited calls forever
            var webClient = new System.Net.WebClient();
            var data = webClient.DownloadString("https://geolocation-db.com/json");
            dynamic d = JsonConvert.DeserializeObject<dynamic>(data);
            string countryCode = d["country_code"];
            string country = d["country_name"];
            string city = d["city"];
            string postal = d["postal"];
            string state = d["state"];
            string ipv4 = d["IPv4"];
            string latitude = d["latitude"];
            string longitude = d["longitude"];
            // End:

            // Shafi: Get current user page url
            var connectionId = Context.ConnectionId;
            // End:

            string currentUser = connectionId + ":" + latitude + ":" + longitude;
            CurrentConnections.Add(currentUser);
            await base.OnConnectedAsync();

            await Clients.All.SendAsync("GetAllOnlineUsers", GetAllActiveConnections());
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connection = CurrentConnections.Where(x => x.Contains(Context.ConnectionId)).FirstOrDefault();
            if(connection != null)
            {
                CurrentConnections.Remove(connection);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public List<string> GetAllActiveConnections()
        {
            return CurrentConnections.ToList();
        }

        //public List<LatitudeLongitudeViewModel> GetAllOnlineUsers()
        //{
        //    return allUsers.ToList();
        //}
    }
}
