using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SalesWebMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMVCContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMVCContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SeedingService>(); 
builder.Services.AddScoped<SellerService>(); 
builder.Services.AddScoped<DepartmentService>(); 
builder.Services.AddScoped<SalesRecordService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var enUS = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo>{enUS},
    SupportedUICultures = new List<CultureInfo>{enUS}
};
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();

app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

app.UseStaticFiles(); 


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

