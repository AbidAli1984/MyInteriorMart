using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace HUBS.Admin
{
    public class AllConnectedUsersMapHub : Hub
    {
        public AllConnectedUsersMapHub()
        {
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
