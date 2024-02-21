using CashboxInterfaceTestTask.Data;
using CashboxInterfaceTestTask.Models;
using CashboxInterfaceTestTask.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))) ;

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 4;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredUniqueChars = 0;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
    opt.Lockout.MaxFailedAccessAttempts = 4;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
});

builder.Services.AddScoped<IBankOperationsService, BankOperationsService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=EnterCardNumber}");

app.Run();
