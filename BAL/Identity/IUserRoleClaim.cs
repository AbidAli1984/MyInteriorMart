using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Identity
{
    public interface IUserRoleClaim
    {
        // Shafi: Get role name by roleId
        public Task<string> GetRoleName(string roleId);
        // End:

        // Shafi: Count all permissions assigned to role
        public Task<int> CountClaimsAssignedToRole(string roleId);
        // End:
        // Shafi: Get userId by userName
        public Task<string> GetUserId(string userName);
        // End:
        public Task<int> GenerateUserRegistrationOtp(string mobile);
        public Task<int> GenerateUserLoginOtp(string mobile);
    }
}
