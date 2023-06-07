using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BOL.CLAIMS
{
    public static class AdminClaimStore
    {
        public static List<Claim> Dashboard = new List<Claim>()
        {
            new Claim("View Real Time Dashboard", "Admin.Dashboard.Realtime.View"),
            new Claim("Admin.Dashboard", "Admin.Dashboard.Listings.View"),
            new Claim("Admin.Dashboard", "Admin.Dashboard.Users.View"),
        };
    }
}
