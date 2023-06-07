using BAL.Dashboard.History;
using BAL.Dashboard.Listing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COM.Components.DashboardHistory
{
    public class DashboardHistoryUsersBookmarkedListing : ViewComponent
    {
        private readonly IDashboardUserHistory userHistory;
        public DashboardHistoryUsersBookmarkedListing(IDashboardUserHistory userHistory)
        {
            this.userHistory = userHistory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int subtractDays)
        {
            var result = await userHistory.UsersBookmarkedListingsAsync(subtractDays);
            return View(result);
        }
    }
}