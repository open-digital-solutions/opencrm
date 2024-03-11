using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.Web;
using OpenCRM.Core.Web.Services.BlockServices.TextBlockService;
using OpenCRM.Core.Web.Services.CardBlockService;
using OpenCRM.Core.Web.Services.EmailService;
using OpenCRM.Core.Web.Services.IdentityService;
using OpenCRM.Core.Web.Services.ImageBlockService;
using OpenCRM.Core.Web.Services.RoleService;
using OpenCRM.Core.Web.Services.TranslationService;
using OpenCRM.SwissLPD;
using OpenCRM.Web.Data;
using OpenCRM.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenCRM<OpenCRMDataContext>(builder.Configuration);
builder.Services.AddOpenCRMSwissLPD<OpenCRMDataContext>();
builder.Services.AddRazorPages();

//Add MoreServices here
builder.Services.AddScoped<MyTranslationsSeederService>();

var app = builder.Build();

app.UseOpenCRM<OpenCRMDataContext>(app.Environment);
app.UseOpenCRMSwissLPDAsync<OpenCRMDataContext>();
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{    
    var identityService = scope.ServiceProvider
   .GetRequiredService<MyTranslationsSeederService>();
    identityService.SeedTranslationsAsync().Wait();

}

app.Run();
