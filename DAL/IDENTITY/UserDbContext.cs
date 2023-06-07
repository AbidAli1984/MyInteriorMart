using System;
using System.Collections.Generic;
using System.Text;
using BOL.IDENTITY;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL.IDENTITY
{
    public class UserDbContext : IdentityDbContext
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
