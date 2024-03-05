using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenCRM.SwissLPD.Services.EventService;
using OpenCRM.Core;
using OpenCRM.SwissLPD.Services.SupplierService;

namespace OpenCRM.SwissLPD
{

    public static class StartupModuleExtensions
    {
        public static IServiceCollection AddOpenCRMSwissLPD<TDBContext>(this IServiceCollection services) where TDBContext : DataContext
        {
            //TODO: Register all module services here
            services.AddScoped<IEventService, EventService<TDBContext>>();
            services.AddScoped<ISupplierService, SupplierService>();

            return services;
        }
        public static IApplicationBuilder UseOpenCRMSwissLPDAsync<TDBContext>(this IApplicationBuilder app) where TDBContext : DataContext
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                //TODO: Use scoped app to use any regitered service before starting up
                var eventService = scope.ServiceProvider
               .GetRequiredService<IEventService>();
                
                eventService.Seed().Wait();
            }
            return app;
        }
    }
}
