using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BOL.BANNER;

namespace DAL.BANNER
{
    public class BannerDbContext : DbContext
    {
        public BannerDbContext(DbContextOptions<BannerDbContext> options)
            : base(options)
        {
        }

        //public DbSet<BannerPlan> BannerPlan { get; set; }
        //public DbSet<BannerSize> BannerSize { get; set; }
        public DbSet<BannerType> BannerType { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<BannerPage> BannerPage { get; set; }
        public DbSet<BannerSpace> BannerSpace { get; set; }
        public DbSet<Slideshow> Slideshow { get; set; }
    }
}
