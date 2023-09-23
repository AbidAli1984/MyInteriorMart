using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Shared
{
    public partial class FreeListingSideNav
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
