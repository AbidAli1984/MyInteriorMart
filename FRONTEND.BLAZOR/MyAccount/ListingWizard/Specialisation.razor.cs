using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AntDesign;
using DAL.Models;
using BAL.Services.Contracts;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Specialisation
    {
        [Inject]
        public IUserService userService { get; set; }

        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-specialisation";
        public bool buttonBusy { get; set; }

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public ApplicationUser iUser { get; set; }
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

        // Begin: Create Specialisation
        public async Task CreateSpecialisationAsync()
        {
            buttonBusy = true;

            // Check if specialisation already exists
            var duplicate = await listingContext.Specialisation.Where(i =>  i.ListingID == listingId).FirstOrDefaultAsync();

            await CountPropertyAsync();

            if (propertyCount > 0)
            {
                if (duplicate == null)
                {
                    BOL.LISTING.Specialisation spc = new BOL.LISTING.Specialisation
                    {
                        ListingID = listingId.Value,
                        OwnerGuid = CurrentUserGuid,
                        IPAddress = IpAddress,
                        AcceptTenderWork = acceptTenderWork,
                        Banks = bank,
                        BeautyParlors = beautyParlors,
                        Bungalow = bungalow,
                        CallCenter = callCenter,
                        Church = church,
                        Company = company,
                        ComputerInstitute = computerInstitute,
                        Dispensary = dispensary,
                        ExhibitionStall = exhibitionStall,
                        Factory = factory,
                        Farmhouse = farmhouse,
                        Gurudwara = gurudwara,
                        Gym = gym,
                        HealthClub = healthClub,
                        Home = home,
                        Hospital = hospital,
                        Hotel = hotel,
                        Laboratory = laboratory,
                        Mandir = mandir,
                        Mosque = mosque,
                        Office = office,
                        Plazas = plazas,
                        ResidentialSociety = residentialSociety,
                        Resorts = resorts,
                        Restaurants = restaurants,
                        Salons = salons,
                        Shop = shop,
                        ShoppingMall = shoppingMall,
                        Showroom = showroom,
                        Warehouse = warehouse
                    };

                    await listingContext.AddAsync(spc);
                    await listingContext.SaveChangesAsync();

                    buttonBusy = false;

                    // Navigate To
                    navManager.NavigateTo($"/MyAccount/ListingWizard/WorkingHours/{listingId}");
                }
                else
                {
                    // Show notification
                    await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", $"Categories for Listing ID {duplicate.ListingID} already exists.");

                    buttonBusy = false;
                }
            }
            else
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Please select at least one specialisation.");

                buttonBusy = false;
            }
        }
        // End: Create Specialisation

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

        // Begin: Count Properties
        public int propertyCount = 0;
        public async Task<int> CountPropertyAsync()
        {
            if (acceptTenderWork == true)
            {
                propertyCount++;
            }

            if (bank == true)
            {
                propertyCount++;
            }

            if (beautyParlors == true)
            {
                propertyCount++;
            }

            if (bungalow == true)
            {
                propertyCount++;
            }

            if (callCenter == true)
            {
                propertyCount++;
            }

            if (church == true)
            {
                propertyCount++;
            }

            if (company == true)
            {
                propertyCount++;
            }

            if (computerInstitute == true)
            {
                propertyCount++;
            }

            if (dispensary == true)
            {
                propertyCount++;
            }

            if (exhibitionStall == true)
            {
                propertyCount++;
            }

            if (factory == true)
            {
                propertyCount++;
            }

            if (farmhouse == true)
            {
                propertyCount++;
            }

            if (gurudwara == true)
            {
                propertyCount++;
            }

            if (gym == true)
            {
                propertyCount++;
            }

            if (healthClub == true)
            {
                propertyCount++;
            }

            if (home == true)
            {
                propertyCount++;
            }

            if (hospital == true)
            {
                propertyCount++;
            }

            if (hotel == true)
            {
                propertyCount++;
            }

            if (laboratory == true)
            {
                propertyCount++;
            }

            if (mandir == true)
            {
                propertyCount++;
            }

            if (mosque == true)
            {
                propertyCount++;
            }

            if (office == true)
            {
                propertyCount++;
            }

            if (plazas == true)
            {
                propertyCount++;
            }

            if (residentialSociety == true)
            {
                propertyCount++;
            }

            if (resorts == true)
            {
                propertyCount++;
            }

            if (restaurants == true)
            {
                propertyCount++;
            }

            if (salons == true)
            {
                propertyCount++;
            }

            if (shop == true)
            {
                propertyCount++;
            }

            if (shoppingMall == true)
            {
                propertyCount++;
            }

            if (showroom == true)
            {
                propertyCount++;
            }

            if (warehouse == true)
            {
                propertyCount++;
            }

            await Task.Delay(10);
            return propertyCount;
        }
        // End: Count Properties

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

                    iUser = await userService.GetUserByUserName(user.Identity.Name);
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

                    if (specialisationExist == true)
                    {
                        string url = "/MyAccount/ListingWizard/SpecialisationEdit/" + listingId;
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
