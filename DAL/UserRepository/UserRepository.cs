using DAL.CustomModel;
using DAL.USER;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly UserDbContext userDbContext;
        public UserRepository(UserManager<ApplicationUser> userManager, UserDbContext userDbContext)
        {
            this._userManager = userManager;
            this.userDbContext = userDbContext;
        }
        public async Task<string> CreateUser(ApplicationUser user)
        {
            try
            {
                var result1 = await userDbContext.Users.AddAsync(user);
                userDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
            var result = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);// await _userMaager.CreateAsync(user);
            return result;
        }
        public async Task<bool> UpdateUser(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
