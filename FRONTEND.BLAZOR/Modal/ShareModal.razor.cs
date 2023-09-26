using BOL.ComponentModels.Listings;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modal
{
    public partial class ShareModal
    {
        [Parameter] public ListingDetailVM listingDetailVM { get; set; }

        public bool showShareModal { get; set; }

        public async Task HideShareModal()
        {
            showShareModal = false;
            await Task.Delay(5);
        }

        public async Task ShowShareModal()
        {
            showShareModal = true;
            await Task.Delay(5);
        }
    }
}
