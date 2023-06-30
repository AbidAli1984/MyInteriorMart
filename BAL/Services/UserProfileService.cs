using BAL.Services.Contracts;
using BOL.IDENTITY;
using DAL.Models;
using DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserProfileService(IUserProfileRepository userProfileRepository, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            this._userProfileRepository = userProfileRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<UserProfile> GetProfileByOwnerGuid(string ownerGuid)
        {
            return await _userProfileRepository.GetProfileByOwnerGuid(ownerGuid);
        }

        public async Task<UserProfile> AddUserProfile(UserProfile userProfile)
        {
            return await _userProfileRepository.AddUserProfile(userProfile);
        }

        public async Task<UserProfile> UpdateUserProfile(UserProfile userProfile)
        {
            return await _userProfileRepository.UpdateUserProfile(userProfile);
        }
    }
}
