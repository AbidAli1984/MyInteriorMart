using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(IDENTITY.Areas.Identity.IdentityHostingStartup))]
namespace IDENTITY.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}