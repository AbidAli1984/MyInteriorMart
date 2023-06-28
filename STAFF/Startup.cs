using BAL.Addresses;
using BAL.Audit;
using BAL.Billing;
using BAL.Category;
using BAL.Claims;
using BAL.Dashboard.History;
using BAL.Dashboard.Listing;
using BAL.Identity;
using BAL.Keyword;
using BAL.Listings;
using BAL.Messaging.Notify;
using DAL.AUDIT;
using DAL.BANNER;
using DAL.BILLING;
using DAL.CATEGORIES;
using DAL.LISTING;
using DAL.SHARED;
using IDENTITY.Data;
using IDENTITY.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace STAFF
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddControllersWithViews();
#if DEBUG
            if (HostEnvironment.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation(options =>
                {
                    var identity = Path.GetFullPath(Path.Combine(HostEnvironment.ContentRootPath, "..\\", "IDENTITY"));
                    options.FileProviders.Add(new PhysicalFileProvider(identity));
                });
            }
#endif

            // Begin: ApplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimApplication")));
            // End:

            // Begin: SharedDbContext
            services.AddDbContext<SharedDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimShared")));
            // End:

            // Begin: ListingDbContext
            services.AddDbContext<ListingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimListing")));
            // End:

            // Begin: ListingDbContext
            services.AddDbContext<CategoriesDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimCategories")));
            // End:

            // Begin: AuditDbContext
            services.AddDbContext<AuditDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuditTrail")));
            // End:

            // Begin: BillingDbContext
            services.AddDbContext<BillingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimBilling")));
            // End:

            // Begin: BannerDbContext
            services.AddDbContext<BannerDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimBanner")));
            // End:

            // Begin: 
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                // Shafi: Add role to get all roles in manage role controller
                .AddRoles<IdentityRole>()
                // End:
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Shafi: Cookie based authentication and login
            // ConfigureApplicationCookie must be called after calling AddIdentity or AddDefaultIdentity.
            // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-3.1
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "UmarzoneLoginCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.LoginPath = "/Identity/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            // End:

            // Shafi: Customize Password And Other Policies
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.MaxFailedAccessAttempts = 3;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

            });
            // End:

            // Shafi: Repositories
            services.AddTransient<ICategory, Category>();
            services.AddTransient<IAddresses, Addresses>();
            services.AddTransient<IListingManager, ListingManager>();
            services.AddTransient<IBilling, Billing>();
            services.AddTransient<INotification, Notification>();
            services.AddTransient<IHistoryAudit, HistoryAudit>();
            services.AddTransient<IDashboardListing, DashboardListing>();
            services.AddTransient<IDashboardUserHistory, DashboardUserHistory>();
            services.AddTransient<IClaimsAdmin, ClaimsAdmin>();
            services.AddTransient<IUserRoleClaim, UserRoleClaim>();
            services.AddTransient<IMenuByClaim, MenuByClaim>();
            services.AddTransient<IUsersOnlineRepository, UsersOnlineRepository>();
            services.AddTransient<IKeywords, Keywords>();
            // End:

            // Shafi: Response Caching
            services.AddMemoryCache();
            // End:

            services.AddControllersWithViews();

            // Shafi: SignalR
            services.AddSignalR();
            // End:
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Shafi: Configure route for Areas
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                // End:

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
