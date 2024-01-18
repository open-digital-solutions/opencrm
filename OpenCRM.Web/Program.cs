using Microsoft.EntityFrameworkCore;
using OpenCRM.Finance;
using OpenCRM.SwissLPD;
using OpenCRM.Web.Data;
using OpenDHS.Shared;
using OpenDHS.Shared.Data;
using OpenCRM.Core.Web;

var builder = WebApplication.CreateBuilder(args);

var dataConnectionString = builder.Configuration.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("Connection string 'DBConnection' not found.");

// builder.Services.AddDbContext<OpenCRMDataContext>(options => options.UseNpgsql(connectionString));

// builder.Services.AddDefaultIdentity<UserEntity>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<OpenCRMDataContext>();

/// Registering OpenCRM Modules.
builder.Services.AddOpenDHSServices<OpenCRMDataContext>();
builder.Services.AddOpenCRMCoreWeb<OpenCRMDataContext>(dataConnectionString);
builder.Services.AddOpenCRMFinance<OpenCRMDataContext>();
builder.Services.AddOpenCRMSwissLPD<OpenCRMDataContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseAuthorization();

/// Using OpenCRM Modules.
app.UseOpenDHSServices<OpenCRMDataContext>();
app.UseOpenCRMCoreWeb<OpenCRMDataContext>();

app.UseOpenCRMFinanceAsync<OpenCRMDataContext>();
app.UseOpenCRMSwissLPDAsync<OpenCRMDataContext>();

app.MapRazorPages();

app.Run();
