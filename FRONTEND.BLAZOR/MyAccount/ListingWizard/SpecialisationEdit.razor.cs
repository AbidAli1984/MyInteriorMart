using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FRONTEND.BLAZOR.Services;
using AntDesign;
using BOL.CATEGORIES;
using BOL.LISTING;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class SpecialisationEdit
    {
        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-specialisation";
        public bool buttonBusy { get; set; }

        // Begin: Toggle Edit
        public bool toggleEdit { get; set; } = true;

        public async Task ToggleEditAsync()
        {
            toggleEdit = !toggleEdit;
            await Task.Delay(5);
        }
        // End: Toggle Edit

        [Inject]
        private IListingService listingService { get; set; }

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public IdentityUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }

        // Begin: Check if record exists
        public bool companyExist { get; set; }
        public bool communicationExist { get; set; }
        public bool addressExist { get; set; }
        public bool categoryExist { get; set; }
        public bool specialisationExist { get; set; }
        public bool workingHoursExist { get; set; }
        public bool paymentModeExist { get; set; }

        public async Task CompanyExistAsync()
        {
            if (listingId != null)
            {
                var company = await listingContext.Listing.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (company != null)
                {
                    companyExist = true;
                }
                else
                {
                    companyExist = false;
                }
            }
        }

        public async Task CommunicationExistAsync()
        {
            if (listingId != null)
            {
                var communication = await listingContext.Communication.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (communication != null)
                {
                    communicationExist = true;
                }
                else
                {
                    communicationExist = false;
                }
            }
        }

        public async Task AddressExistAsync()
        {
            if (listingId != null)
            {
                var address = await listingContext.Address.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (address != null)
                {
                    addressExist = true;
                }
                else
                {
                    addressExist = false;
                }
            }
        }

        public async Task CategoryExistAsync()
        {
            if (listingId != null)
            {
                var category = await listingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (category != null)
                {
                    categoryExist = true;
                }
                else
                {
                    categoryExist = false;
                }
            }
        }

        public async Task SpecialisationExistAsync()
        {
            if (listingId != null)
            {
                var specialisation = await listingContext.Specialisation.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (specialisation != null)
                {
                    specialisationExist = true;
                }
                else
                {
                    specialisationExist = false;
                }
            }
        }

        public async Task WorkingHoursExistAsync()
        {
            if (listingId != null)
            {
                var wh = await listingContext.WorkingHours.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (wh != null)
                {
                    workingHoursExist = true;
                }
                else
                {
                    workingHoursExist = false;
                }
            }
        }

        public async Task PaymentModeExistAsync()
        {
            if (listingId != null)
            {
                var pm = await listingContext.PaymentMode.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (pm != null)
                {
                    paymentModeExist = true;
                }
                else
                {
                    paymentModeExist = false;
                }
            }
        }
        // End: Check if record exists

        // Properties
        public bool acceptTenderWork { get; set; } = false;
        public bool bank { get; set; } = false;
        public bool beautyParlors { get; set; } = false;
        public bool bungalow { get; set; } = false;
        public bool callCenter { get; set; } = false;
        public bool church { get; set; } = false;
        public bool company { get; set; } = false;
        public bool computerInstitute { get; set; } = false;
        public bool dispensary { get; set; } = false;
        public bool exhibitionStall { get; set; } = false;
        public bool factory { get; set; } = false;
        public bool farmhouse { get; set; } = false;
        public bool gurudwara { get; set; } = false;
        public bool gym { get; set; } = false;
        public bool healthClub { get; set; } = false;
        public bool home { get; set; } = false;
        public bool hospital { get; set; } = false;
        public bool hotel { get; set; } = false;
        public bool laboratory { get; set; } = false;
        public bool mandir { get; set; } = false;
        public bool mosque { get; set; } = false;
        public bool office { get; set; } = false;
        public bool plazas { get; set; } = false;
        public bool residentialSociety { get; set; } = false;
        public bool resorts { get; set; } = false;
        public bool restaurants { get; set; } = false;
        public bool salons { get; set; } = false;
        public bool shop { get; set; } = false;
        public bool shoppingMall { get; set; } = false;
        public bool showroom { get; set; } = false;
        public bool warehouse { get; set; } = false;

        public async Task SelectAllAsync()
        {
            acceptTenderWork = true;
            bank = true;
            beautyParlors = true;
            bungalow = true;
            callCenter = true;
            church = true;
            company = true;
            computerInstitute = true;
            dispensary = true;
            exhibitionStall = true;
            factory = true;
            farmhouse = true;
            gurudwara = true;
            gym = true;
            healthClub = true;
            home = true;
            hospital = true;
            hotel = true;
            laboratory = true;
            mandir = true;
            mosque = true;
            office = true;
            plazas = true;
            residentialSociety = true;
            resorts = true;
            restaurants = true;
            salons = true;
            shop = true;
            shoppingMall = true;
            showroom = true;
            warehouse = true;
            await Task.Delay(1);
        }

        // Begin: Get Specialisation
        public async Task GetSpecialisationAsync()
        {
            var specialisation = await listingContext.Specialisation.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            if (specialisation != null)
            {
                acceptTenderWork = specialisation.AcceptTenderWork;
                bank = specialisation.Banks;
                beautyParlors = specialisation.BeautyParlors;
                bungalow = specialisation.Bungalow;
                callCenter = specialisation.CallCenter;
                church = specialisation.Church;
                company = specialisation.Company;
                computerInstitute = specialisation.ComputerInstitute;
                dispensary = specialisation.Dispensary;
                exhibitionStall = specialisation.ExhibitionStall;
                factory = specialisation.Factory;
                farmhouse = specialisation.Farmhouse;
                gurudwara = specialisation.Gurudwara;
                gym = specialisation.Gym;
                healthClub = specialisation.HealthClub;
                home = specialisation.Home;
                hospital = specialisation.Hospital;
                hotel = specialisation.Hotel;
                laboratory = specialisation.Laboratory;
                mandir = specialisation.Mandir;
                mosque = specialisation.Mosque;
                office = specialisation.Office;
                plazas = specialisation.Plazas;
                residentialSociety = specialisation.ResidentialSociety;
                resorts = specialisation.Resorts;
                restaurants = specialisation.Restaurants;
                salons = specialisation.Salons;
                shop = specialisation.Shop;
                shoppingMall = specialisation.ShoppingMall;
                showroom = specialisation.Showroom;
                warehouse = specialisation.Warehouse;

            }
        }
        // End: Get Specialisation

        // Begin: Update Specialisation
        public bool loadSpecialisation { get; set; }
        public async Task UpdateSpecialisationAsync()
        {
            buttonBusy = true;

            var specialisation = await listingContext.Specialisation.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            if (specialisation != null)
            {
                specialisation.IPAddress = IpAddress;
                specialisation.AcceptTenderWork = acceptTenderWork;
                specialisation.Banks = bank;
                specialisation.BeautyParlors = beautyParlors;
                specialisation.Bungalow = bungalow;
                specialisation.CallCenter = callCenter;
                specialisation.Church = church;
                specialisation.Company = company;
                specialisation.ComputerInstitute = computerInstitute;
                specialisation.Dispensary = dispensary;
                specialisation.ExhibitionStall = exhibitionStall;
                specialisation.Factory = factory;
                specialisation.Farmhouse = farmhouse;
                specialisation.Gurudwara = gurudwara;
                specialisation.Gym = gym;
                specialisation.HealthClub = healthClub;
                specialisation.Home = home;
                specialisation.Hospital = hospital;
                specialisation.Hotel = hotel;
                specialisation.Laboratory = laboratory;
                specialisation.Mandir = mandir;
                specialisation.Mosque = mosque;
                specialisation.Office = office;
                specialisation.Plazas = plazas;
                specialisation.ResidentialSociety = residentialSociety;
                specialisation.Resorts = resorts;
                specialisation.Restaurants = restaurants;
                specialisation.Salons = salons;
                specialisation.Shop = shop;
                specialisation.ShoppingMall = shoppingMall;
                specialisation.Showroom = showroom;
                specialisation.Warehouse = warehouse;

                listingContext.Update(specialisation);
                await listingContext.SaveChangesAsync();

                navManager.NavigateTo($"/MyAccount/ListingWizard/WorkingHours/{listingId}");
            }
            else
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Specialisation with Listing ID {listingId} does not exists.");

                buttonBusy = false;
            }
        }
        // End: Update Specialisation

        // Begin: Antdesign Blazor Notification
        private async Task NoticeWithIcon(NotificationType type, NotificationPlacement placement, string message, string description)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = message,
                Description = description,
                NotificationType = type,
                Placement = placement
            });
        }
        // End: Antdesign Blazor Notification

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    // Shafi: Assign Time Zone to CreatedDate & Created Time
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    IpAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    CreatedDate = timeZoneDate;
                    CreatedTime = timeZoneDate;
                    // End:

                    iUser = await applicationContext.Users.Where(i => i.UserName == user.Identity.Name).FirstOrDefaultAsync();
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;

                    // Begin: Check if record exists
                    await CompanyExistAsync();
                    await CommunicationExistAsync();
                    await AddressExistAsync();
                    await CategoryExistAsync();
                    await SpecialisationExistAsync();
                    await WorkingHoursExistAsync();
                    await PaymentModeExistAsync();
                    // End: Check if record exists

                    await GetSpecialisationAsync();

                    if (specialisationExist == false)
                    {
                        string url = "/MyAccount/ListingWizard/Specialisation/" + listingId;
                        navManager.NavigateTo(url);
                    }
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}