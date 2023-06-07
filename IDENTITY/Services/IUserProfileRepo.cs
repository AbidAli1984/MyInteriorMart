using System.Threading.Tasks;
using IDENTITY.Models;

namespace IDENTITY.Services
{
    public interface IUserProfileRepo
    {
        Task<UserProfile> GetUserProfileAsync(string OwnerGuid);
        string UserEmailByGuid(string userGuid);
    }
}
