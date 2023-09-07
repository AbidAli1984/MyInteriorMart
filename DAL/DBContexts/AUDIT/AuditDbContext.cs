using BOL.AUDITTRAIL;
using Microsoft.EntityFrameworkCore;

namespace DAL.AUDIT
{
    public class AuditDbContext : DbContext
    {
        public AuditDbContext(DbContextOptions<AuditDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserHistory> UserHistory { get; set; }
        public DbSet<ListingLastUpdated> ListingLastUpdated { get; set; }
        public DbSet<ListingLikeDislike> ListingLikeDislike { get; set; }
        public DbSet<Bookmarks> Bookmarks { get; set; }
        public DbSet<Subscribes> Subscribes { get; set; }
        public DbSet<Suggestions> Suggestions { get; set; }
        public DbSet<SearchHistory> SearchHistory { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<UsersOnline> UsersOnline { get; set; }
        public DbSet<ListingNotification> ListingNotification { get; set; }
        public DbSet<ListingClaim> ListingClaim { get; set; }
        public DbSet<UserRegistrationOTPVerification> UserRegistrationOTPVerification { get; set; }
        public DbSet<UserLoginOTPVerification> UserLoginOTPVerification { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
    }
}