using Equinox.Models.Data;
using Equinox.Models.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Register EquinoxContext with SQLite DB
builder.Services.AddDbContext<EquinoxContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EquinoxConnection")));

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IClassCategoryRepository, ClassCategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add required services
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

var app = builder.Build();

// Seed data and assign UserIds to bookings (run once, then remove)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EquinoxContext>();
    context.Database.EnsureCreated();

    // Assign UserIds to bookings (run once, then remove)
    Equinox.Utilities.BookingUserIdAssigner.AssignSequentialUserIds();

    // Seed demo classes with images (run once, then remove)
    Equinox.Utilities.DemoClassSeeder.SeedDemoClasses();
}

// Middleware
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Routing for Admin Area
app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
);

// Default Route for Main Site
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
