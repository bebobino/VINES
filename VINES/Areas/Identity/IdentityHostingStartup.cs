using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VINES.Areas.Identity.Data;
using VINES.Data;

[assembly: HostingStartup(typeof(VINES.Areas.Identity.IdentityHostingStartup))]
namespace VINES.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDBContextConnection")));

                services.AddDefaultIdentity<VINESUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AuthDBContext>();
            });
        }
    }
}