using BOL.ComponentModels.Listings;
using FRONTEND.BLAZOR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modal
{
    public partial class ShareModal
    {
        [Parameter] public ListingDetailVM listingDetailVM { get; set; }

        [Inject] IJSRuntime jsRuntime { get; set; }

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

        public async Task CopyToClipboard()
        {
            await jsRuntime.InvokeVoidAsync("copyToClipboard", listingDetailVM.shareUrl);
        }
    }
}
