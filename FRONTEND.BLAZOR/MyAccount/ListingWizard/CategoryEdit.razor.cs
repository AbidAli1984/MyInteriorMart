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
        public IListingService listingService{ get; set; }
        [Inject]
        public Helper helper { get; set; }

        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }
        public int listingID { get; set; }

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

        public string ErrorMessage { get; set; }
        // Begin: Check if record exists
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
                        await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Categories for Listing ID {listingCat.ListingID} does not exists.");

                        buttonBusy = false;
                    }

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
