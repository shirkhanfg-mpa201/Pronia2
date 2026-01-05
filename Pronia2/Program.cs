using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pronia2.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var app = builder.Build();



app.UseStaticFiles();
app.UseRouting();

    app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );



app.MapDefaultControllerRoute();

app.Run();
