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
using DAL.Models;
using BAL.Services.Contracts;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class CategoryEdit
    {
        [Inject]
        public IUserService userService { get; set; }

        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-category";
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
        public int? firstCatId { get; set; }
        public int? secondCatId { get; set; }

        // Begin: Get Selected First And Second Category
        public FirstCategory selectedFirstCategory { get; set; }
        public SecondCategory selectedSecondCategory { get; set; }

        public async Task GetSelectedCategory()
        {
            var cat = await listingContext.Categories
                .Where(i => i.ListingID == listingId)
                .FirstOrDefaultAsync();

            if(cat != null)
            {
                selectedFirstCategory = await categoriesContext.FirstCategory
                    .Where(i => i.FirstCategoryID == cat.FirstCategoryID)
                    .FirstOrDefaultAsync();

                selectedSecondCategory = await categoriesContext.SecondCategory
                    .Where(i => i.SecondCategoryID == cat.SecondCategoryID)
                    .FirstOrDefaultAsync();
            }
        }
        // End: Get Selected First And Second Category

        public IList<FirstCategory> listFirstCat { get; set; }
        public IList<SecondCategory> listSecondCat { get; set; }

        // Begin: First Category List
        public async Task ListFirstCategories()
        {
            try
            {
                listFirstCat = await categoriesContext.FirstCategory.OrderBy(i => i.Name).ToListAsync();
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
        // End: First Category List

        // Begin: Second Category List
        public async Task ListSecondCategories(ChangeEventArgs e)
        {
            try
            {
                firstCatId = Convert.ToInt32(e.Value.ToString());

                listSecondCat = await categoriesContext.SecondCategory.OrderBy(i => i.Name).Where(i => i.FirstCategoryID == firstCatId).ToListAsync();
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
        // End: Second Category List

        // Begin: Update Second Category ID
        public async Task GetSecondCatId(ChangeEventArgs e)
        {
            try
            {
                secondCatId = Convert.ToInt32(e.Value.ToString());
                await Task.Delay(500);
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
        // End: Update Second Category ID

        // Begin: Create Listing Category
        public async Task UpdateListingCategory()
        {
            buttonBusy = true;

            try
            {
                if (listingId != null && firstCatId != null && secondCatId != null)
                {
                    var listingCat = await listingContext.Categories
                        .Where(i => i.ListingID == listingId)
                        .FirstOrDefaultAsync();

                    if (listingCat != null)
                    {
                        listingCat.FirstCategoryID = firstCatId.Value;
                        listingCat.SecondCategoryID = secondCatId.Value;

                        listingContext.Update(listingCat);
                        await listingContext.SaveChangesAsync();

                        // Navigate To
                        navManager.NavigateTo($"/MyAccount/ListingWizard/Specialisation/{listingId}");
                    }
                    else
                    {
                        // Show notification
                        await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", $"Categories for Listing ID {listingCat.ListingID} does not exists.");

                        buttonBusy = false;
                    }

                }
                else
                {
                    // Show notification
                    await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", $"Listing ID, First Category and Second Category must not be blank.");

                    buttonBusy = false;
                }
            }
            catch (Exception exc)
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", exc.Message);

                buttonBusy = false;
            }
        }
        // End: Create Category

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

                    await ListFirstCategories();
                    await GetSelectedCategory();

                    if (categoryExist == true)
                    {
                        navManager.NavigateTo($"/MyAccount/ListingWizard/CategoryEdit/{listingId}");
                    }
                    else
                    {
                        navManager.NavigateTo($"/MyAccount/ListingWizard/Category/{listingId}");
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
