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
        Task<string> CreateUser(ApplicationUser user);
        Task<bool> UpdateUser(ApplicationUser user);
    }
}
