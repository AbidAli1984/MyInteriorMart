using BOL.IDENTITY;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetProfileByOwnerGuid(string ownerGuid);
        Task<UserProfile> AddUserProfile(UserProfile userProfile);
        Task<UserProfile> UpdateUserProfile(UserProfile userProfile);
    }
}
