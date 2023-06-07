using BAL.Dashboard.History;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COM.Components.DashboardHistory
{
    // Shafi: Dashboard History
    public class DashboardHistoryUsersUpdatedListing : ViewComponent
    {
        private readonly IDashboardUserHistory userHistory;
        public DashboardHistoryUsersUpdatedListing(IDashboardUserHistory userHistory)
        {
            this.userHistory = userHistory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int subtractDays)
        {
            var result = await userHistory.UsersUpdatedListingAsync(subtractDays);
            return View(result);
        }
    }
    // End:
}