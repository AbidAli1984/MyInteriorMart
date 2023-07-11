using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BAL.Addresses;
using BOL.VIEWMODELS.Hub;
using BAL.Services.Contracts;

namespace HUBS.Admin
{
    public class AllConnectedUsersHub : Hub
    {

        private readonly IUserProfileService _userProfileService;
        private readonly IUserService _userService;
        private readonly IAddresses addressManager;

        public AllConnectedUsersHub(IUserService userService, IUserProfileService userProfileService, IAddresses addressManager)
        {
            this._userService = userService;
            this._userProfileService = userProfileService;
            this.addressManager = addressManager;
        }

        public async override Task OnConnectedAsync()
        {
            // Shafi: Begin connection
            await base.OnConnectedAsync();
            // End:

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
            string url = Context.GetHttpContext().Request.Query["url"];
            string referrer = Context.GetHttpContext().Request.Query["referrer"];
            // End:

            // Shafi: Create new ConnectedUserViewModel object
            ConnectedUsersViewModel userDetails = new ConnectedUsersViewModel();
            // End:

            if (Context.User.Identity.IsAuthenticated == true)
            {
                // Shafi: Get current user id and check if profile for this user exists
                string profileImage = "";
                var user = await _userService.GetUserByUserName(Context.User.Identity.Name);
                var profile = await _userProfileService.GetProfileByOwnerGuid(user.Id);
                if (profile != null)
                {
                    profileImage = "/FileManager/ProfileImages/" + profile.ProfileID.ToString() + ".jpg";
                    var countryDetails = await addressManager.CountryDetailsAsync(profile.CountryID);
                    var stateDetails = await addressManager.StateDetailsAsync(profile.StateID);
                    var cityDetails = await addressManager.CityDetailsAsync(profile.CityID);
                    var postalDetails = await addressManager.PincodeDetailsAsync(profile.PincodeID);

                    country = countryDetails.Name;
                    state = stateDetails.Name;
                    city = cityDetails.Name;
                    postal = postalDetails.PincodeNumber.ToString();
                }
                else
                {
                    profileImage = "/img/profile-thumbnail.jpg";
                }
                // End:

                // Shafi: Get current connected user details
                string userType = "Registered User";
                string userName = Context.User.Identity.Name;
                string connectionId = Context.ConnectionId;
                // End:

                // Shafi: Populate userDetails object with values
                userDetails.userType = userType;
                userDetails.userName = userName;
                userDetails.profileImage = profileImage;
                userDetails.connectionId = connectionId;
                userDetails.country = country;
                userDetails.state = state;
                userDetails.city = city;
                userDetails.postal = postal;
                userDetails.url = url;
                userDetails.referrer = referrer;
                userDetails.latitude = latitude;
                userDetails.longitude = longitude;
                // End:

                await Clients.All.SendAsync("UserConnected", userDetails);
            }
            else
            {
                // Shafi: Get current connected user details
                string profileImage = "/img/profile-thumbnail.jpg";
                string userType = "Anonymous User";
                string userName = "Unknown";
                string connectionId = Context.ConnectionId;
                // End:

                // Shafi: Populate userDetails object with values
                userDetails.userType = userType;
                userDetails.userName = userName;
                userDetails.profileImage = profileImage;
                userDetails.connectionId = connectionId;
                userDetails.country = country;
                userDetails.state = state;
                userDetails.city = city;
                userDetails.postal = postal;
                userDetails.url = url;
                userDetails.referrer = referrer;
                userDetails.latitude = latitude;
                userDetails.longitude = longitude;
                // End:

                await Clients.All.SendAsync("UserConnected", userDetails);
            }
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            await base.OnDisconnectedAsync(exception);
            await Clients.Others.SendAsync("UserDisconnected", connectionId);
        }
    }
}
