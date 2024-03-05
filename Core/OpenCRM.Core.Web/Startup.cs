using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Extensions;
using OpenCRM.Core.QRCode;
using OpenCRM.Core.Web.Services;
using OpenCRM.Core.Web.Services.CardBlockService;
using OpenCRM.Core.Web.Services.EmailService;
using OpenCRM.Core.Web.Services.IdentityService;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Services.RoleService;
using OpenCRM.Core.Web.Services.TranslationService;

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
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILanguageService, LanguageService<TDBContext>>();
            services.AddScoped<ITranslationService, TranslationService<TDBContext>>();
            services.AddScoped<ICookiesSessionService, CookiesSessionService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICardBlockService, CardBlockService>();
            services.AddScoped<IUserSessionService, UserSessionService<TDBContext>>();
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = "OpenCRM.Antiforgery.Token";
            });
            services.AddIdentity<UserEntity, RoleEntity>().AddEntityFrameworkStores<TDBContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Login";
                options.LogoutPath = "/Identity/Logout";
                options.Cookie.Name = "OpenCRM.Identity.Token";
                
            });
            services.AddAuthentication();
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            });
            services.AddAuthorization();
            services.AddHttpContextAccessor();

            services.AddMicrosoftIdentityWebApiAuthentication(configuration)
                   .EnableTokenAcquisitionToCallDownstreamApi()
                       .AddMicrosoftGraph(configuration.GetSection("DownstreamApi"))
                       .AddInMemoryTokenCaches();

            return services;
        }
        public static  IApplicationBuilder UseOpenCRM<TDBContext>(this IApplicationBuilder app, IWebHostEnvironment env) where TDBContext : DataContext
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            
            //TODO: Evaluate this condition
            if (env != null && env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                //TODO: Use scoped app to use any regitered service before starting up

                var dbContext = scope.ServiceProvider
                  .GetRequiredService<TDBContext>();
                
                dbContext.Database.EnsureDeleted();

                dbContext.Database.EnsureCreated();

                var languageService = scope.ServiceProvider.GetRequiredService<ILanguageService>();
                languageService.Seed().Wait();

                var translationsService = scope.ServiceProvider.GetRequiredService<ITranslationService>();
                translationsService.Seed().Wait();

                var emailService = scope.ServiceProvider
                  .GetRequiredService<IEmailService>();
                emailService.SendEmail("yariel.re@gmail.com", "Hola", "https://google.com");

                var roleService = scope.ServiceProvider
                .GetRequiredService<IRoleService>();
                roleService.Seed().Wait();

                var identityService = scope.ServiceProvider
               .GetRequiredService<IIdentityService>();
                identityService.Seed().Wait();

            }
            return app;
        }
       

    }
}
