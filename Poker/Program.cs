using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Poker.Data;
using Poker.Data.NoSQL;
using Poker.Models;
using Poker.Services;
using Poker.SignalR;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddLogging();
//Initiation MongoDb
services.AddSingleton(new MongoClient("mongodb://localhost:27017"));

services.AddTransient<CookiesService>();
services.AddTransient<RoomAdapter>();
services.AddTransient<PlayServices>();
services.AddTransient<BankService>();
services.AddScoped<FinishService>();
services.AddTransient<Deck>();
services.AddTransient<CombinationSelection>();


services.AddMvc();
services.AddControllersWithViews();
services.AddSignalR();

services.AddDistributedMemoryCache();
services.AddSession();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

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

// Get MongoClient through DI
app.Map("/GetMongo", async (MongoClient client) =>
{
    var db = client.GetDatabase("test");    //go to DB
    var collection = db.GetCollection<BsonDocument>("users"); //Get collection users
    // for test add start data , if collecton empty
    if(await collection.CountDocumentsAsync("{}") == 0)
    {
        await collection.InsertManyAsync(new List<BsonDocument>
        {
            new BsonDocument{ { "Name", "Tom2" },{"Age", 22}},
            new BsonDocument{ { "Name", "Bob" },{"Age", 42}}
        });
    }
    var users = await collection.Find("{}").ToListAsync();
    return users.ToJson();
});
MongoDbTest.StartServer();
await MongoDbTest.IsTest(); //MongoDb Test

app.Map("/PostMongo",  (MongoClient client) =>
{
    var db = client.GetDatabase("test");
    var collection = db.GetCollection<BsonDocument>("cards"); 
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.UseSession(); //UseSession вызывается после UseRouting,
                  //но перед MapRazorPages и MapDefaultControllerRoute.
//Невозможно получить доступ к HttpContext.Session до вызова UseSession.

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
