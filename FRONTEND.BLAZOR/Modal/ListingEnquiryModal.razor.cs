using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.Listings;
using BOL.LISTING;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modal
{
    public partial class ListingEnquiryModal
    {
        [Parameter] public string CurrentUserGuid { get; set; }
        [Parameter] public int ListingId { get; set; }

        [Inject] private IListingService listingService { get; set; }
        [Inject] Helper helper { get; set; }

        public bool showEnquiryModal { get; set; } = false;

        public ListingEnquiryVM ListingEnquiryVM { get; set; } = new ListingEnquiryVM();

        public async Task HideEnquiryModal()
        {
            showEnquiryModal = false;
            await Task.Delay(5);
        }

        public async Task ShowEnquiryModal()
        {
            showEnquiryModal = true;
            await Task.Delay(5);
        }

        public async Task CreateEnquiry()
        {
            if (!ListingEnquiryVM.isValid())
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"All Fields are mandatory!.");
                return;
            }
            try
            {
                ListingEnquiryVM.ListingEnquiry.OwnerGuid = Convert.ToString(CurrentUserGuid);
                ListingEnquiryVM.ListingEnquiry.ListingID = ListingId;
                await listingService.AddAsync(ListingEnquiryVM.ListingEnquiry);

                await HideEnquiryModal();
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Your Enquiry has been sent.");
                ListingEnquiryVM.ListingEnquiry = new ListingEnquiry();
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
    }
}
