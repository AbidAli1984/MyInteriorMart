using BAL.Listings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DAL.Models;

namespace COM.Components.ListingDashboard
{
    public class ListingViewAsync : ViewComponent
    {
        private readonly IListingManager listingRepo;
        private readonly UserManager<ApplicationUser> userManager;
        public ListingViewAsync(IListingManager listingRepo, UserManager<ApplicationUser> userManager)
        {
            this.listingRepo = listingRepo;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? ListingID)
        {
            // Shafi: Get date time by India Standard Time
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            // End:

            // Shafi: Get client location details using https://geolocation-db.com/ free service with unlimited calls forever
            var webClient = new System.Net.WebClient();
            var data = webClient.DownloadString("https://geolocation-db.com/json/697de680-a737-11ea-9820-af05f4014d91");
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

            // Shafi: If user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Shafi: Get user guid
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                string remoteIpAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
                string OwnerGuid = user.Id;
                // End:

                // Shafi: Save listing through repository
                await listingRepo.RecordListingViewAsync(ListingID, "Registered User", OwnerGuid, remoteIpAddress, timeZoneDate.Date, country, city, postal, state, ipv4, latitude, longitude);
                // End:
            }
            // Shafi: If user is not authorized then execute this code
            else
            {
                // Shafi: Get IP Address
                string remoteIpAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
                // End:

                // Shafi: Save listing through repository
                await listingRepo.RecordListingViewAsync(ListingID, "Anonymous User", "Not Available", remoteIpAddress, timeZoneDate.Date, country, city, postal, state, ipv4, latitude, longitude);
                // End:
            }

            return View();
        }
    }
}