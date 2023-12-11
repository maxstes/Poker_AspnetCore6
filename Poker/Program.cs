using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Poker.Data;
using Poker.Models;
using Poker.SignalR;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddMvc();
services.AddControllersWithViews();
services.AddSignalR();
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");

builder.Services.AddAuthorization();

services.AddIdentity<ApplicationUser,IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.RequireUniqueEmail = true;
})
.AddUserManager<UserManager<ApplicationUser>>()
.AddSignInManager<SignInManager<ApplicationUser>>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddRoleManager<RoleManager<IdentityRole>>()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}"
    );
app.Run();
