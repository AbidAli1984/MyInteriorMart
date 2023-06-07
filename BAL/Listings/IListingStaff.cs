using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Listings
{
    public interface IListingStaff
    {
        Task<int> ClaimedCountAsync(string staffGuid);
        Task<int> RequestForClaimCountAsync(string staffGuid);
        Task<int> ApprovedCountAsync(string staffGuid);
        Task<int> PendingApprovalCountAsync(string staffGuid);
        int IncompleteCount(string staffGuid);
        Task<int> WithoutOwnerPhotoCountAsync(string staffGuid);
        Task<int> WithoutLogoCountAsync(string staffGuid);
        Task<int> WithoutThumbnailCountAsync(string staffGuid);
        Task<int> RequestForChangesCountAsync(string staffGuid);
        Task<int> ChangesDoneCountAsync(string staffGuid);
        Task<int> RequestForRemovalCountAsync(string staffGuid);


    }
}
