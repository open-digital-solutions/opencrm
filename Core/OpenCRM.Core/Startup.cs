using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Extensions;
using OpenCRM.Core.QRCode;

namespace OpenCRM.Core
{
    public static class StartupModuleExtensions
    {
        public static IServiceCollection AddOpenDHSServices<TDBContext>(this IServiceCollection services) where TDBContext : DataContext
        {
            OpenCRMEnv.SetWebRoot();
    
            services.AddScoped<QRCodeService>();
            services.AddScoped<IMediaService, MediaService<TDBContext>>();
            services.AddScoped<IDataBlockService, DataBlockService<TDBContext>>();

            return services;
        }

            public static IApplicationBuilder UseOpenDHSServices<TDBContext>(this IApplicationBuilder app) where TDBContext : DataContext
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<TDBContext>();
                dbContext.Database.EnsureCreated();
            }         
            return app;
        }
    }
}
