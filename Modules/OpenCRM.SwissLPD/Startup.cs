﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenCRM.SwissLPD.Services;
using OpenDHS.Shared;

namespace OpenCRM.SwissLPD
{

    public static class StartupModuleExtensions
    {
        public static IServiceCollection AddOpenCRMSwissLPD<TDBContext>(this IServiceCollection services) where TDBContext : DataContext
        {
            //TODO: Register all module services here
            services.AddScoped<IEventService, EventService<TDBContext>>();
            return services;
        }
        public static async Task<IApplicationBuilder> UseOpenCRMSwissLPDAsync<TDBContext>(this IApplicationBuilder app) where TDBContext : DataContext
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
                await eventService.Seed();
            }
            return app;
        }
    }
}
