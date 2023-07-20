using Microsoft.AspNetCore.Components;

namespace Components.Shared
{
    public partial class SideNavigation
    {
        [Parameter]
        public bool isVendor { get; set; }

        [Parameter]
        public int currentPage { get; set; }

        public string ActiveLink(int pageId)
        {
            return currentPage == pageId ? "active" : "";
        }
    }
}
