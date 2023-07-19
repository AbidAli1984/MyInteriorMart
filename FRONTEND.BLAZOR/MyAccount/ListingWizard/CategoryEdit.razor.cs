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
using BOL.ComponentModels.MyAccount.ListingWizard;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class CategoryEdit
    {
        [Inject]
        public IListingService listingService { get; set; }
        [Inject]
        public ICategoryService categoryService { get; set; }
        [Inject]
        public Helper helper { get; set; }

        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }
        public int listingID { get; set; }
        public CategoryVM CategoryVM { get; set; } = new CategoryVM();

        public string currentPage = "nav-category";
        public bool buttonBusy { get; set; }
        public bool preventEdit { get; set; } = true;
        public int firstCatId { get; set; }
        public int secondCatId { get; set; }

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
                    if(categoryExist)
                    {
                        if (CategoryVM.Category.FirstCategoryID != null)
                        {
                            await GetSecondCategoryIdByFirstCategoryId(null);
                        }

                        if (CategoryVM.Category.SecondCategoryID != null)
                        {
                            await GetOtherCategoriesBySecondCategoryId(null);
                        }
                    }

                    //if (categoryExist == true)
                    //{
                    //    navManager.NavigateTo($"/MyAccount/ListingWizard/CategoryEdit/{listingId}");
                    //}
                    //else
                    //{
                    //    navManager.NavigateTo($"/MyAccount/ListingWizard/Category/{listingId}");
                    //}
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }

        public async Task ToggleEditAsync()
        {
            preventEdit = !preventEdit;
            await Task.Delay(5);
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
            CategoryVM.Category = await listingService.GetCategoryByListingId(listingID);
            categoryExist = CategoryVM.Category != null;
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

        public async Task GetSecondCategoryIdByFirstCategoryId(ChangeEventArgs e)
        {
            try
            {
                if (e != null)
                    CategoryVM.Category.FirstCategoryID = Convert.ToInt32(e.Value.ToString());
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
                if (e != null)
                    CategoryVM.Category.SecondCategoryID = Convert.ToInt32(e.Value.ToString());
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

        public async Task UpdateListingCategory()
        {
            buttonBusy = true;

            try
            {
                var category = CategoryVM.Category;
                if (category != null)
                {
                    
                    if (listingId != null && category.FirstCategoryID != null && category.SecondCategoryID != null)
                    {
                        categoryService.GetOtherCategoriesToUpdate(CategoryVM);
                        listingContext.Update(CategoryVM.Category);
                        await listingContext.SaveChangesAsync();

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
                else
                {
                    // Show notification
                    await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Categories for Listing ID {listingId} does not exists.");

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
