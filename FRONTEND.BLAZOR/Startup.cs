using DAL.AUDIT;
using DAL.BANNER;
using DAL.BILLING;
using DAL.CATEGORIES;
using DAL.Repositories.Contracts;
using DAL.Models;
using DAL.LISTING;
using DAL.SHARED;
using DAL.USER;
using DAL.Repositories;
using FRONTEND.BLAZOR.Areas.Identity;
using FRONTEND.BLAZOR.Data;
using FRONTEND.BLAZOR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using REPO;
using System.IO;
using BAL.Services.Contracts;
using BAL.Services;

namespace FRONTEND.BLAZOR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Shafi: ApplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimApplication")), ServiceLifetime.Transient);
            // End:

            // Abid: UserDbContext
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimUser")), ServiceLifetime.Transient);
            // End:

            // Shafi: SharedDbContext
            services.AddDbContext<SharedDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimShared")), ServiceLifetime.Transient);
            // End:

            // Shafi: ListingDbContext
            services.AddDbContext<ListingDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MimListing")), ServiceLifetime.Transient);
            // End:

            // Begin: ListingDbContext
            services.AddDbContext<CategoriesDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimCategories")), ServiceLifetime.Transient);
            // End:

            // Shafi: AuditDbContext
            services.AddDbContext<AuditDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuditTrail")), ServiceLifetime.Transient);
            // End:

            // Begin: BillingDbContext
            services.AddDbContext<BillingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimBilling")), ServiceLifetime.Transient);
            // End:

            // Begin: BannerDbContext
            services.AddDbContext<BannerDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MimBanner")), ServiceLifetime.Transient);
            // End:

            // Begin: AntDesign Blazor UI
            services.AddAntDesign();
            // End:


            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                // Shafi: Add role to get all roles in manage role controller
                .AddRoles<IdentityRole>()
                // End:
                .AddEntityFrameworkStores<UserDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddTransient<CategoryRepo>();
            services.AddTransient<IHistoryAudit, HistoryAudit>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISuspendedUserService, SuspendedUserService>();
            services.AddTransient<ISuspendedUserRepository, SuspendedUserRepository>();
            services.AddTransient<IListingService, ListingService>();
            services.AddTransient<IListingRepository, ListingRepository>();
            services.AddTransient<IAuditService, AuditService>();
            services.AddTransient<IAuditRepository, AuditRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Begin: Create another public/static folder
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "CloudBox")),
                RequestPath = new PathString("/CloudBox")
            });
            // End: Create another public/static folder

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
