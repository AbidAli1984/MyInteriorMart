using DAL.CustomModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UserRepository
{
    public interface IUserRepository
    {
        //Task<ApplicationUser> CreateUser(ApplicationUser user);
        Task<ApplicationUser> AdOrUpdateUser(ApplicationUser user);
        Task<ApplicationUser> GetUserByMobileNo(string mobileNo);
    }
}
