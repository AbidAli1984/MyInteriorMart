using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IUserRoleService
    {
        Task<int> GetRolesCount();
        Task<bool> RoleHasClaimType(string userGuid, string ClaimType);
        Task<bool> RoleHasClaim(string userGuid, string ClaimType, string ClaimValue);
    }
}
