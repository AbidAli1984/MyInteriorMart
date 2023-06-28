using BAL.Services.Contracts;
using DAL.Models;
using DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class SuspendedUserService : ISuspendedUserService
    {
        private readonly ISuspendedUserRepository _suspendedUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SuspendedUserService(ISuspendedUserRepository suspendedUserRepositor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._suspendedUserRepository = suspendedUserRepositor;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<bool> IsUserSuspended(string UserGuid)
        {
            if (string.IsNullOrEmpty(UserGuid))
                return false;
            var suspendedUser = await _suspendedUserRepository.GetSuspendedUserById(UserGuid);
            return suspendedUser != null;
        }

        public async Task<IEnumerable<BOL.IDENTITY.SuspendedUser>> GetSuspendedUsers()
        {
            return await _suspendedUserRepository.GetSuspendedUsers();
        }

        public async Task<IEnumerable<BOL.IDENTITY.SuspendedUser>> GetUnSuspendedUsers()
        {
            return await _suspendedUserRepository.GetUnSuspendedUsers();
        }

        public async Task SuspendUser(string SuspendedTo, string SuspendedBy, DateTime SuspendedDate, string ReasonForSuspending)
        {
            await _suspendedUserRepository.SuspendUser(SuspendedTo, SuspendedBy, SuspendedDate, ReasonForSuspending);
        }

        public async Task UnSuspendUser(string UnsuspendedTo, string UnsuspendedBy, DateTime UnsuspendedDate, string ReasonForUnsuspending)
        {
            await _suspendedUserRepository.UnSuspendUser(UnsuspendedTo, UnsuspendedBy, UnsuspendedDate, ReasonForUnsuspending);
        }
    }
}
