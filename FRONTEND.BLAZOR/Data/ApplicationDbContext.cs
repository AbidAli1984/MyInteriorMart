using FRONTEND.BLAZOR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRONTEND.BLAZOR.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<RoleCategory> RoleCategory { get; set; }
        public DbSet<RoleCategoryAndRole> RoleCategoryAndRole { get; set; }
        public DbSet<SuspendedUser> SuspendedUser { get; set; }
    }
}
