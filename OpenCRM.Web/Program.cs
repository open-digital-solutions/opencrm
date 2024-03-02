using OpenCRM.Core.Web;
using OpenCRM.SwissLPD;
using OpenCRM.Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenCRM<OpenCRMDataContext>(builder.Configuration);
builder.Services.AddOpenCRMSwissLPD<OpenCRMDataContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseOpenCRM<OpenCRMDataContext>(app.Environment);
app.UseOpenCRMSwissLPDAsync<OpenCRMDataContext>();
app.MapRazorPages();
app.Run();
