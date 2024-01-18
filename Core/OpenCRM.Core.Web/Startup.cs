using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Services;
using OpenDHS.Shared;
using OpenDHS.Shared.Data;
using OpenDHS.Shared.Extensions;
using OpenDHS.Shared.QRCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web
{
    public static class StartupModuleExtensions
    {
        public static IServiceCollection AddOpenCRMCoreWeb<TDBContext>(this IServiceCollection services, string connectionString) where TDBContext : DataContext
        {
            OpenCRMEnv.SetWebRoot();
            //TODO: Register all module services here
            services.AddDbContext<TDBContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<QRCodeService>();
            services.AddScoped<IMediaService, MediaService<TDBContext>>();
            services.AddScoped<IDataBlockService, DataBlockService<TDBContext>>();
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            services.AddDefaultIdentity<UserEntity>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TDBContext>();
            return services;
        }
        public static IApplicationBuilder UseOpenCRMCoreWeb<TDBContext>(this IApplicationBuilder app) where TDBContext : DataContext
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseAuthorization();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                //TODO: Use scoped app to use any regitered service before starting up
                var dbContext = scope.ServiceProvider
                  .GetRequiredService<TDBContext>();

                dbContext.Database.EnsureCreated();
            }
            return app;
        }
    }
}
