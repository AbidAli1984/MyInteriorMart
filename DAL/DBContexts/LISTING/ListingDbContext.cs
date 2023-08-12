using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BOL.LISTING;
using BOL.PLAN;
using BOL.LABOURNAKA;
using BOL.BANNERADS;
using BOL.LISTING.UploadImage;

namespace DAL.LISTING
{
    public class ListingDbContext : DbContext
    {
        public ListingDbContext(DbContextOptions<ListingDbContext> options)
            : base(options)
        {
        }

        // Shafi: Listing
        public DbSet<Listing> Listing { get; set; }
        public DbSet<Communication> Communication { get; set; }
        public DbSet<Certification> Certification { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<PaymentMode> PaymentMode { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Specialisation> Specialisation { get; set; }
        public DbSet<SocialNetwork> SocialNetwork { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<AssembliesAndCities> AssembliesAndCities { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public IEnumerable<object> NearestSmartCity { get; set; }
        // End:

        // Shafi: Listing analytics
        public DbSet<ListingViewCount> ListingViewCount { get; set; }
        public DbSet<ListingViews> ListingViews { get; set; }
        // End:

        // Shafi: Labour Naka
        public DbSet<LaborCategory> LabourCategory { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Profession> Profession { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Reference> Reference { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Classification> Classification { get; set; }
        // End: Labour Naka

        // Begin: Banners
        public DbSet<HomeBanner> HomeBanner { get; set; }
        public DbSet<CategoryBanner> CategoryBanner { get; set; }
        public DbSet<ListingBanner> ListingBanner { get; set; }
        // End: Banners

        #region Upload Images
        public DbSet<LogoImage> LogoImages { get; set; }
        public DbSet<OwnerImage> OwnerImages { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<BannerDetail> BannerDetails { get; set; }
        public DbSet<CertificationDetail> CertificationDetails { get; set; }
        public DbSet<ClientDetail> ClientDetails { get; set; }
        #endregion
    }
}
