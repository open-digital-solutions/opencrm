using OpenCRM.Core.Web;
using OpenCRM.SwissLPD;
using OpenCRM.Web.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenCRM<OpenCRMDataContext>(builder.Configuration);
builder.Services.AddOpenCRMSwissLPD<OpenCRMDataContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseStaticFiles();

app.UseRouting();

app.UseOpenCRM<OpenCRMDataContext>();
app.UseOpenCRMSwissLPDAsync<OpenCRMDataContext>();

app.MapRazorPages();

app.Run();
