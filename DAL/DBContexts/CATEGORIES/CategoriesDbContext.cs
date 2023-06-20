using BOL.BRANDS;
using BOL.CATEGORIES;
using Microsoft.EntityFrameworkCore;

namespace DAL.CATEGORIES
{
    public class CategoriesDbContext : DbContext
    {
        public CategoriesDbContext(DbContextOptions<CategoriesDbContext> options)
            : base(options)
        {
        }

        public DbSet<FirstCategory> FirstCategory { get; set; }
        public DbSet<SecondCategory> SecondCategory { get; set; }
        public DbSet<ThirdCategory> ThirdCategory { get; set; }
        public DbSet<FourthCategory> FourthCategory { get; set; }
        public DbSet<FifthCategory> FifthCategory { get; set; }
        public DbSet<SixthCategory> SixthCategory { get; set; }
        public DbSet<KeywordFirstCategory> KeywordFirstCategory { get; set; }
        public DbSet<KeywordSecondCategory> KeywordSecondCategory { get; set; }
        public DbSet<KeywordThirdCategory> KeywordThirdCategory { get; set; }
        public DbSet<KeywordFourthCategory> KeywordFourthCategory { get; set; }
        public DbSet<KeywordFifthCategory> KeywordFifthCategory { get; set; }
        public DbSet<KeywordSixthCategory> KeywordSixthCategory { get; set; }
        public DbSet<NewSearchTerm> NewSearchTerm { get; set; }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<BrandCategory> BrandsCategory { get; set; }
        public DbSet<KeywordBrand> KeywordBrand { get; set; }
        public DbSet<ListingTitle> ListingTitle { get; set; }

        // Shafi: Begin CMS
        public DbSet<Pages> Pages { get; set; }
        // End:
    }
}