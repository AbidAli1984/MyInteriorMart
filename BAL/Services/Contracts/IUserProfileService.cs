using BOL.IDENTITY;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IUserProfileService
    {
        Task<UserProfile> GetProfileByOwnerGuid(string ownerGuid);
        Task<UserProfile> AddUserProfile(UserProfile userProfile);
        Task<UserProfile> UpdateUserProfile(UserProfile userProfile);
    }
}
