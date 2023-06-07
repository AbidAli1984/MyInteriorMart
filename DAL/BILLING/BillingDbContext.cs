using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BOL.PLAN;
using BOL.BILLING;

namespace DAL.BILLING
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Plan> Plan { get; set; }
        public DbSet<Period> Period { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<AdvertisementPlan> AdvertisementPlan { get; set; }
        public DbSet<BannerPlan> BannerPlan { get; set; }
        public DbSet<DataPlan> DataPlan { get; set; }
        public DbSet<EmailPlans> EmailPlans { get; set; }
        public DbSet<SMSPlans> SMSPlans { get; set; }
        public DbSet<MagazinePlan> MagazinePlan { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
