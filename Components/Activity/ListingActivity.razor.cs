using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;

namespace Components.Activity
{
    public partial class ListingActivity
    {
        [Parameter]
        public ListingActivityVM listingActivityVM { get; set; }
    }
}
