using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IUserRoleRepository
    {
        Task<List<string>> GetRoleClaimTypesByRoleId(string roleId);
        Task<string> GetRoleClaimNameByRoleIdClaimTypeAndClaimValue(string roleId,
            string claimType, string claimValue);
    }
}
