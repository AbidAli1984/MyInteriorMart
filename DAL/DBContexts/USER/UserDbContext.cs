using System;
using System.Collections.Generic;
using System.Text;
using BOL.IDENTITY;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL.USER
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<RoleCategory> RoleCategory { get; set; }
        public DbSet<RoleCategoryAndRole> RoleCategoryAndRole { get; set; }
        public DbSet<SuspendedUser> SuspendedUser { get; set; }
    }
}
