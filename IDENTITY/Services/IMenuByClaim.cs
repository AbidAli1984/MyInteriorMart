using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY.Services
{
    public interface IMenuByClaim
    {
        public Task<bool> RoleHasClaimType(string userGuid, string ClaimType);
        public Task<bool> RoleHasClaim(string userGuid, string ClaimType, string ClaimValue);
    }
}
