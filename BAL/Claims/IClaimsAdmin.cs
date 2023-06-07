using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BOL.CLAIMS.Admin;

namespace BAL.Claims
{
    public interface IClaimsAdmin
    {
        // Shafi: Get claim value by name
        public AdminClaim.Dashboard claimDashboard(string name);
        public AdminClaim.UserManager claimUser(string name);
        public AdminClaim.Listing claimListing(string name);
        public AdminClaim.Localities claimLocality(string name);
        public AdminClaim.Categories claimCategory(string name);
        public AdminClaim.Miscellaneous claimMiscellaneous(string name);
        public AdminClaim.Keywords claimKeyword(string name);
        public AdminClaim.Pages claimPage(string name);
        public AdminClaim.Notifications claimNotification(string name);
        public AdminClaim.Slideshow claimSlideshow(string name);
        public AdminClaim.HistoryAndCache claimHistoryAndCache(string name);
        // End:

        // Shafi: List Claims
        public IList<AdminClaim.Dashboard> Dashboard();
        public IList<AdminClaim.UserManager> UserManager();
        public IList<AdminClaim.Listing> Listing();
        public IList<AdminClaim.Localities> Locality();
        public IList<AdminClaim.Categories> Category();
        public IList<AdminClaim.Miscellaneous> Miscellaneous();
        public IList<AdminClaim.Keywords> Keywords();
        public IList<AdminClaim.Pages> Pages();
        public IList<AdminClaim.Notifications> Notifications();
        public IList<AdminClaim.Slideshow> Slideshow();
        public IList<AdminClaim.HistoryAndCache> HistoryAndCache();
        // End:

        // Shafi: Check if Role Has Claim
        public Task<bool> CheckIfRoleHasClaim(string roleId, string claimType, string claim);
        // ENd:

        // Shafi: Assign Claims To Role
        public Task AssignClaimsToRole(string roleId, string claimType, string claimList);
        // ENd:
    }
}
