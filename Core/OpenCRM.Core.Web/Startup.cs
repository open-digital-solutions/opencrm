using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.ExternalConnectors;
using Microsoft.Identity.Web;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Extensions;
using OpenCRM.Core.QRCode;
using OpenCRM.Core.Web.Services.EmailNotificationService;
using OpenCRM.Core.Web.Services.IdentityService;
using OpenCRM.Core.Web.Services.LanguageService;

namespace OpenCRM.Core.Web
{
    public static class StartupModuleExtensions
    {
        public static IServiceCollection AddOpenCRM<TDBContext>(this IServiceCollection services, IConfiguration configuration) where TDBContext : DataContext
        {
            OpenCRMEnv.SetWebRoot();

            //TODO: Register all module services here

            string connectionString = configuration.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("OpenCRM DB Connection string 'DBConnection' not found.");
            services.AddDbContext<TDBContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<QRCodeService>();
            services.AddScoped<IMediaService, MediaService<TDBContext>>();
            services.AddScoped<IDataBlockService, DataBlockService<TDBContext>>();
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            services.AddScoped<ILanguageService, LanguageService<TDBContext>>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddDefaultIdentity<UserEntity>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TDBContext>();

            services.AddMicrosoftIdentityWebApiAuthentication(configuration)
                   .EnableTokenAcquisitionToCallDownstreamApi()
                       .AddMicrosoftGraph(configuration.GetSection("DownstreamApi"))
                       .AddInMemoryTokenCaches();

            return services;
        }
        public static IApplicationBuilder UseOpenCRM<TDBContext>(this IApplicationBuilder app) where TDBContext : DataContext
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

                var emailService = scope.ServiceProvider
                  .GetRequiredService<IEmailNotificationService>();

                emailService.SendMSGraphEmail().Wait();

            }
            return app;
        }
    }
}
