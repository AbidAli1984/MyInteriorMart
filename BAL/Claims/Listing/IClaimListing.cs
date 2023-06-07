using BOL.VIEWMODELS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Claims.Listing
{
    public interface IClaimListing
    {
        Task<bool> CheckIfUserAlreadyClaimedAnyListing(string userGuid);
        Task<int> GenerateMobileOTP(string mobileNumber, string userGuid, int listingId, string message);
        Task<int> GenerateEmailOTP(string email, string userGuid, int listingId, string message);
        Task GenerateDocumentOTP(string userGuid, int listingId, string message);
        Task<ValidationResponseViewModel> VerifyMobileOTP(string mobileNumber, string userGuid, int listingId, int otp);
        Task<ValidationResponseViewModel> VerifyEmailOTP(string email, string userGuid, int listingId, int otp);
        Task<ValidationResponseViewModel> VerifyDocumentOTP(string shortLink, string mobile, string email);
        Task UploadBusinessProof();
        Task TransferListing(string userGuid, int listingId, string mobile, string email);
        Task<bool> CheckIfListingAlreadyClaimed(int listingId);
    }
}
