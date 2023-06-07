using BOL.AUDITTRAIL;
using DAL.AUDIT;
using DAL.SHARED;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Identity
{
    public class UserRoleClaim : IUserRoleClaim
    {

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SharedDbContext SharedContext;
        private readonly AuditDbContext AuditContext;
        public UserRoleClaim(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SharedDbContext sharedContext, AuditDbContext auditContext)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            SharedContext = sharedContext;
            AuditContext = auditContext;
        }

        public async Task<string> GetRoleName(string roleId)
        {
            var roleName = await roleManager.Roles.Where(i => i.Id == roleId).Select(i => i.Name).FirstOrDefaultAsync(); ;
            return roleName;
        }

        // Shafi: Count all permissions assigned to role
        public async Task<int> CountClaimsAssignedToRole(string roleId)
        {
            // Shafi: Get role
            var role = await roleManager.FindByIdAsync(roleId);
            // End:

            // Shafi: Get all claims for role
            var getAllClaimsForRole = await roleManager.GetClaimsAsync(role);
            // End:

            // Shafi: Count claims in role
            return getAllClaimsForRole.Count();
            // End:
        }

        // Shafi: Get userId by userName
        public async Task<string> GetUserId(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user.Id.ToString();
        }

        public async Task<int> GenerateUserRegistrationOtp(string mobile)
        {
            UserRegistrationOTPVerification userOtp = new UserRegistrationOTPVerification();

            Random random = new Random();
            int otp = random.Next(100000, 900000);

            userOtp.DateTime = DateTime.Now;
            userOtp.Mobile = mobile;
            userOtp.Status = "Pending";
            userOtp.OTPVerified = false;
            userOtp.OTP = otp;

            await AuditContext.AddAsync(userOtp);
            await AuditContext.SaveChangesAsync();

            return userOtp.OtpID;
        }
        // End:

        public async Task<int> GenerateUserLoginOtp(string mobile)
        {
            UserLoginOTPVerification userOtp = new UserLoginOTPVerification();

            Random random = new Random();
            int otp = random.Next(100000, 900000);

            userOtp.DateTime = DateTime.Now;
            userOtp.Mobile = mobile;
            userOtp.Status = "Pending";
            userOtp.OTPVerified = false;
            userOtp.OTP = otp;

            await AuditContext.AddAsync(userOtp);
            await AuditContext.SaveChangesAsync();

            return userOtp.OtpID;
        }
    }
}
