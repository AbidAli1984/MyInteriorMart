using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using DAL.Models;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BAL;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Category
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IListingService listingService { get; set; }
        [Inject]
        public ICategoryService categoryService { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        public Helper helper { get; set; }

        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }
        public int listingID { get; set; }
        public CategoryVM CategoryVM { get; set; } = new CategoryVM();
        public ApplicationUser iUser { get; set; }

        public string currentPage = "nav-category";
        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool companyExist { get; set; }
        public bool communicationExist { get; set; }
        public bool addressExist { get; set; }
        public bool categoryExist { get; set; }
        public bool specialisationExist { get; set; }
        public bool workingHoursExist { get; set; }
        public bool paymentModeExist { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {

                int.TryParse(Convert.ToString(listingId), out int listId);
                listingID = listId;
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    iUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    if (listing == null)
                        navManager.NavigateTo("/MyAccount/Listing/Company");
                    else if (listing.Steps < Constants.AddressComplete)
                        helper.NavigateToPageByStep(listing.Steps, navManager);

                    if (listingId != null)
                    {
                        // Begin: Check if record exists
                        await CompanyExistAsync();
                        await CommunicationExistAsync();
                        await AddressExistAsync();
                        await CategoryExistAsync();
                        await SpecialisationExistAsync();
                        await WorkingHoursExistAsync();
                        await PaymentModeExistAsync();
                        // End: Check if record exists
                    }

                    CategoryVM.FirstCategories = await categoryService.GetFirstCategoriesAsync();
                    if (categoryExist == true)
                    {
                        string url = "/MyAccount/ListingWizard/CategoryEdit/" + listingId;
                        navManager.NavigateTo(url);
                    }
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }

        #region Check if record exists
        public async Task CompanyExistAsync()
        {
            var company = await listingService.GetListingByListingId(listingID);
            companyExist = company != null;
        }

        public async Task CommunicationExistAsync()
        {
            var communication = await listingService.GetCommunicationByListingId(listingID);
            communicationExist = communication != null;
        }

        public async Task AddressExistAsync()
        {
            var address = await listingService.GetAddressByListingId(listingID);
            addressExist = address != null;
        }

        public async Task CategoryExistAsync()
        {
            var category = await listingService.GetCategoryByListingId(listingID);
            categoryExist = category != null;
        }

        public async Task SpecialisationExistAsync()
        {
            var specialisation = await listingService.GetSpecialisationByListingId(listingID);
            specialisationExist = specialisation != null;
        }

        public async Task WorkingHoursExistAsync()
        {
            var wh = await listingService.GetWorkingHoursByListingId(listingID);
            workingHoursExist = wh != null;
        }

        public async Task PaymentModeExistAsync()
        {
            var pm = await listingService.GetPaymentModeByListingId(listingID);
            paymentModeExist = pm != null;
        }
        #endregion

        public async Task GetSecondCategoryIdByFirstCategoryId(ChangeEventArgs e)
        {
            try
            {
                int.TryParse(e.Value.ToString(), out int firstCatId);
                CategoryVM.Category.FirstCategoryID = firstCatId;
                await categoryService.GetSecCategoriesByFirstCategoryId(CategoryVM);
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }

        public async Task GetOtherCategoriesBySecondCategoryId(ChangeEventArgs e)
        {
            try
            {
                int.TryParse(e.Value.ToString(), out int secondCatId);
                CategoryVM.Category.SecondCategoryID = secondCatId;
                await categoryService.GetOtherCategoriesBySeconCategoryId(CategoryVM);
                await Task.Delay(500);
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }

        public async Task SelectAllCategories()
        {
            categoryService.MarkAllCategoriesSelected(CategoryVM);
            await Task.Delay(1);
        }

        public async Task CreateListingCategory()
        {
            buttonBusy = true;

            try
            {
                var category = CategoryVM.Category;
                if (listingID > 0 && category.FirstCategoryID > 0 && category.SecondCategoryID > 0)
                {
                    categoryService.GetOtherCategoriesToUpdate(CategoryVM);
                    CategoryVM.Category.ListingID = listingID;
                    CategoryVM.Category.OwnerGuid = CurrentUserGuid;
                    CategoryVM.Category.IPAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

                    await listingService.UpdateAsync(CategoryVM.Category);

                    // Navigate To
                    navManager.NavigateTo($"/MyAccount/ListingWizard/Specialisation/{listingId}");

                }
                else
                {
                    // Show notification
                    await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Listing ID, First Category and Second Category must not be blank.");

                    buttonBusy = false;
                }
            }
            catch (Exception exc)
            {
                // Show notification
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);

                buttonBusy = false;
            }
        }
    }
}
