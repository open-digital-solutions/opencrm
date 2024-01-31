using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Extensions;
using OpenCRM.Core.QRCode;
using OpenCRM.Core.Web.Services;
using OpenCRM.Core.Web.Services.LanguageService;

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
            services.AddScoped<ILanguageService, LanguageService<TDBContext>>();
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
