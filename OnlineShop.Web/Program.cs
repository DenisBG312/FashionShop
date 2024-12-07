using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Services.Data;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.Data;
using OnlineShop.Web.Infrastructure.Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/Error/403";
});

builder.Services.AddScoped(typeof(BaseRepository<,>));

builder.Services.AddScoped<IRepository<ClothingType, int>, BaseRepository<ClothingType, int>>();

builder.Services.RegisterUserDefinedServices(typeof(IProductService).Assembly);

builder.Services.AddControllersWithViews();



var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("bg-BG")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    if (context.Request.Cookies.TryGetValue("Language", out var cookie))
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie);
    }
    else
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
    }

    await next.Invoke();
});


app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

await SeedAdminUserAndRole(app);

app.Run();

async Task SeedAdminUserAndRole(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }

    var adminUser = await userManager.FindByEmailAsync("admin@onlineshop.com");
    if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    var regularUser = await userManager.FindByEmailAsync("bgdenibg@gmail.com");
    if (regularUser != null && !await userManager.IsInRoleAsync(regularUser, "User"))
    {
        await userManager.AddToRoleAsync(regularUser, "User");
    }
}
