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

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class ListingWizardMenu
    {
        public string ErrorMessage { get; set; }

        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        [Parameter]
        public string currentPage { get; set; }
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
                var record = await listingContext.Listing.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (record != null)
                {
                    companyExist = true;
                }
                else
                {
                    companyExist = false;
                }
            }
        }

        public async Task AddressExistAsync()
        {
            if (listingId != null)
            {
                var record = await listingContext.Address.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (record != null)
                {
                    addressExist = true;
                }
                else
                {
                    addressExist = false;
                }
            }
        }

        public async Task CommunicationExistAsync()
        {
            if (listingId != null)
            {
                var record = await listingContext.Communication.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (record != null)
                {
                    communicationExist = true;
                }
                else
                {
                    communicationExist = false;
                }
            }
        }

        public async Task CategoryExistAsync()
        {
            if (listingId != null)
            {
                var record = await listingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (record != null)
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
                var record = await listingContext.Specialisation.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (record != null)
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
                var record = await listingContext.WorkingHours.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (record != null)
                {
                    workingHoursExist = true;
                }
                else
                {
                    workingHoursExist = false;
                }
            }
        }

        public async Task PaymentExistAsync()
        {
            if (listingId != null)
            {
                var record = await listingContext.PaymentMode.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (record != null)
                {
                    paymentModeExist = true;
                }
                else
                {
                    paymentModeExist = false;
                }
            }
        }
        // End: Check for record exisit with listingId

        protected async override Task OnInitializedAsync()
        {
            try
            {
                await CompanyExistAsync();
                await CommunicationExistAsync();
                await AddressExistAsync();
                await CategoryExistAsync();
                await SpecialisationExistAsync();
                await WorkingHoursExistAsync();
                await PaymentExistAsync();
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}
