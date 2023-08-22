using Microsoft.EntityFrameworkCore;
using BOL.SHARED;

namespace DAL.SHARED
{
    public class SharedDbContext : DbContext
    {
        public SharedDbContext(DbContextOptions<SharedDbContext> options)
            : base(options)
        {
        }

        public DbSet<Designation> Designation { get; set; }
        public DbSet<NatureOfBusiness> NatureOfBusiness { get; set; }
        public DbSet<Turnover> Turnover { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Pincode> Pincode { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Caste> Castes { get; set; }
    }
}