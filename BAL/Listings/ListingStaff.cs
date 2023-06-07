using BAL.Dashboard.Listing;
using BOL.VIEWMODELS;
using DAL.LISTING;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Listings
{
    public class ListingStaff : IListingStaff
    {
        private readonly IDashboardListing dashboardListing;
        private readonly ListingDbContext listingContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ListingStaff(IDashboardListing dashboardListing, ListingDbContext listingContext, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.dashboardListing = dashboardListing;
            this.listingContext = listingContext;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public Task<int> ClaimedCountAsync(string staffGuid)
        {
            throw new NotImplementedException();
        }

        public Task<int> RequestForClaimCountAsync(string staffGuid)
        {
            throw new NotImplementedException();
        }

        public Task<int> ApprovedCountAsync(string staffGuid)
        {
            throw new NotImplementedException();
        }

        public Task<int> PendingApprovalCountAsync(string staffGuid)
        {
            throw new NotImplementedException();
        }
        public int IncompleteCount(string staffGuid)
        {
            var listingResult = listingContext.Listing.Where(i => i.OwnerGuid == staffGuid).Select(i => i.OwnerGuid).Count();

            var result = from list in listingContext.Listing
                         .Where(i => i.OwnerGuid == staffGuid)
                         join comm in listingContext.Communication
                         on list.ListingID equals comm.ListingID

                         join add in listingContext.Address
                         on list.ListingID equals add.ListingID

                         join cat in listingContext.Categories
                               on list.ListingID equals cat.ListingID

                         join spec in listingContext.Specialisation
                               on list.ListingID equals spec.ListingID

                         join work in listingContext.WorkingHours
                               on list.ListingID equals work.ListingID

                         join pay in listingContext.PaymentMode
                               on list.ListingID equals pay.ListingID

                         select new FreeListingViewModel
                         {
                             Listing = list,
                             Communication = comm,
                             Specialisation = spec,
                             PaymentMode = pay
                         };

            var newResult = result.Select(i => i.Listing.OwnerGuid).Count();

            var count = newResult - listingResult;

            return count;
        }
        
        public async Task<int> WithoutOwnerPhotoCountAsync(string staffGuid)
        {
            var listingResult = await listingContext.Listing.Where(i => i.OwnerGuid == staffGuid).Select(i => i.ListingID).ToListAsync();

            var count = 0;
            foreach(var item in listingResult)
            {
                var file = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "/FileManager/ListingOwnerPhoto/", item + ".jpg");
                if(!System.IO.File.Exists(file))
                {
                    count++;
                }
            }

            return count;
        }

        public async Task<int> WithoutLogoCountAsync(string staffGuid)
        {
            var listingResult = await listingContext.Listing.Where(i => i.OwnerGuid == staffGuid).Select(i => i.ListingID).ToListAsync();

            var count = 0;
            foreach (var item in listingResult)
            {
                var file = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "/FileManager/ListingLogo/", item + ".jpg");
                if (!System.IO.File.Exists(file))
                {
                    count++;
                }
            }

            return count;
        }

        public async Task<int> WithoutThumbnailCountAsync(string staffGuid)
        {
            var listingResult = await listingContext.Listing.Where(i => i.OwnerGuid == staffGuid).Select(i => i.ListingID).ToListAsync();

            var count = 0;
            foreach (var item in listingResult)
            {
                var file = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "/FileManager/ListingThumbnail/", item + ".jpg");
                if (!System.IO.File.Exists(file))
                {
                    count++;
                }
            }

            return count;
        }

        public Task<int> RequestForChangesCountAsync(string staffGuid)
        {
            throw new NotImplementedException();
        }

        public Task<int> ChangesDoneCountAsync(string staffGuid)
        {
            throw new NotImplementedException();
        }

        public Task<int> RequestForRemovalCountAsync(string staffGuid)
        {
            throw new NotImplementedException();
        }
    }
}
