using AntDesign;
using BOL.LISTING;
using BOL.SHARED;
using FRONTEND.BLAZOR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Services.Contracts;
using DAL.Models;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Company
    {

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        IUserService userService { get; set; }

        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-company";
        public bool buttonBusy { get; set; }


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

        // Begin: Listing Properties
        public string Name { get; set; }
        public string Gender { get; set; }
        public string CompanyName { get; set; }
        public DateTime? YearOfEstablishment { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string DesignationV { get; set; }
        public string NOB { get; set; }
        public string Turnover { get; set; }
        public string ListingUrl { get; set; }
        // End: Listing Properties

        // Begin: Gender Dropdown List
        public class Genders {
            public string Name { get; set; }
            public string Value { get; set; }
            public bool NotAvailable { get; set; }
        }

        public IList<Genders> genderList;
        public async Task PopulateGenderAsync()
        {
            genderList = new List<Genders>
            {
                new Genders
                {
                    Name = "Male",
                    Value = "Male"
                },
                new Genders
                {
                    Name = "Female",
                    Value = "Female"
                },
                new Genders
                {
                    Name = "Undisclosed",
                    Value = "Undisclosed"
                },
            };

            await Task.Delay(100);
        }
        // End: Gender Dropdown List

        // Begin: Turnover Dropdown List
        public class Turnovers
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public IList<Turnovers> turnoverList;
        public async Task PopulateTurnoverAsync()
        {
            turnoverList = new List<Turnovers>
            {
                new Turnovers
                {
                    Name = "Upto 1 Lac",
                    Value = "Upto 1 Lac"
                },
                new Turnovers
                {
                    Name = "Upto 2 Lac",
                    Value = "Upto 2 Lac"
                },
                new Turnovers
                {
                    Name = "Upto 3 Lac",
                    Value = "Upto 3 Lac"
                },
                new Turnovers
                {
                    Name = "Upto 5 Lac",
                    Value = "Upto 5 Lac"
                },
                new Turnovers
                {
                    Name = "Upto 10 Lac",
                    Value = "Upto 10 Lac"
                },
                new Turnovers
                {
                    Name = "Upto 50 Lac",
                    Value = "Upto 50 Lac"
                },
                new Turnovers
                {
                    Name = "Upto 1 Crore",
                    Value = "Upto 1 Crore"
                },
                new Turnovers
                {
                    Name = "Upto 10 Crore & Above",
                    Value = "Upto 10 Crore & Above"
                }
            };

            await Task.Delay(100);
        }
        // End: Turnover Dropdown List

        // Begin: Nature of Business Dropdown List
        public IList<NatureOfBusiness> natureOfBusinessList;
        public async Task PopulateNatureOfBusinessesAsync()
        {
            natureOfBusinessList = await sharedContext.NatureOfBusiness.ToListAsync();

            await Task.Delay(100);
        }
        // End: Nature of Business Dropdown List

        // Begin: Designation Dropdown List
        public IList<Designation> designationList;
        public async Task PopulateDesignationAsync()
        {
            designationList = await sharedContext.Designation.ToListAsync();

            await Task.Delay(100);
        }
        // End: Designation Dropdown List

        // Begin: Create Company
        public async Task CreateCompanyAsync()
        {
            buttonBusy = true;

            try
            {
                if (IpAddress != null && Name != null && Gender != null && CompanyName != null && YearOfEstablishment != null && NumberOfEmployees != null && DesignationV != null && NOB != null && Turnover != null)
                {
                    string listingUrl = CompanyName.Replace(" ", "-");

                    Listing listing = new Listing
                    {
                        OwnerGuid = CurrentUserGuid,
                        CreatedDate = CreatedDate,
                        CreatedTime = CreatedTime,
                        IPAddress = IpAddress,
                        Name = Name,
                        Gender = Gender,
                        CompanyName = CompanyName,
                        YearOfEstablishment = YearOfEstablishment.Value,
                        NumberOfEmployees = NumberOfEmployees.Value,
                        Designation = DesignationV,
                        NatureOfBusiness = NOB,
                        Turnover = Turnover,
                        ListingURL = listingUrl,
                        Approved = false
                    };

                    await listingContext.AddAsync(listing);
                    await listingContext.SaveChangesAsync();

                    navManager.NavigateTo($"/MyAccount/ListingWizard/Communication/{listing.ListingID}");
                }
                else
                {
                    await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "All fields are compulsory.");

                    buttonBusy = false;
                }
            }
            catch (Exception exc)
            {
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);

                buttonBusy = false;
            }
        }
        // End: Create Company

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

                    iUser = await userService.GetUserByUserNameOrEmail(user.Identity.Name);
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

                    await PopulateGenderAsync();
                    await PopulateTurnoverAsync();
                    await PopulateNatureOfBusinessesAsync();
                    await PopulateDesignationAsync();

                    if (companyExist == true)
                    {
                        navManager.NavigateTo($"/MyAccount/ListingWizard/CompanyEdit/{listingId}");
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
