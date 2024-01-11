using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenDHS.Shared.Extensions;
using OpenDHS.Shared.QRCode;
using OpenDHS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCRM.Finance.Services;

namespace OpenCRM.Finance
{

    public static class StartupModuleExtensions
    {
        public static IServiceCollection AddOpenCRMFinance<TDBContext>(this IServiceCollection services) where TDBContext : DataContext
        {
            //TODO: Register all module services here
            services.AddScoped<IAccountingService, AccountingService<TDBContext>>();

            return services;
        }
        public static async Task<IApplicationBuilder> UseOpenCRMFinanceAsync<TDBContext>(this IApplicationBuilder app) where TDBContext : DataContext
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            using (var scope = app.ApplicationServices.CreateScope())
            {
                //TODO: Use scoped app to use any regitered service before starting up
                var accountingDataService = scope.ServiceProvider
                .GetRequiredService<IAccountingService>();               
                await accountingDataService.Seed();
            }
            return app;
        }
    }
}