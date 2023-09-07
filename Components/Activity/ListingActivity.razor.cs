using BAL;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Components.Activity
{
    public partial class ListingActivity
    {
        [Parameter]
        public ListingActivityVM listingActivityVM { get; set; }

        private string classIcon = string.Empty;
        private string classColor = string.Empty;


        protected async override Task OnInitializedAsync()
        {
            if (listingActivityVM.ActivityType == Constants.Like)
            {
                classIcon = "fa-thumbs-up";
                if (listingActivityVM.isNotification)
                    classColor = "text-primary";
            }
            else if (listingActivityVM.ActivityType == Constants.Bookmark)
            {
                classIcon = "fa-bookmark";
                if (listingActivityVM.isNotification)
                    classColor = "text-success";
            }
            else if (listingActivityVM.ActivityType == Constants.Subscribe)
            {
                classIcon = "fa-link";
                if (listingActivityVM.isNotification)
                    classColor = "text-danger";
            }
        }
    }
}
