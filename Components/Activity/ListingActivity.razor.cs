using BAL;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;

namespace Components.Activity
{
    public partial class ListingActivity
    {
        [Parameter]
        public ListingActivityVM listingActivityVM { get; set; }

        public string ClassName { 
            get
            {
                return listingActivityVM.ActivityType == Constants.Like ? "fa-thumbs-up" :
                listingActivityVM.ActivityType == Constants.Bookmark ? "fa-bookmark" :
                listingActivityVM.ActivityType == Constants.Subscribe ? "fa-link" : string.Empty;
            }
        }
    }
}
