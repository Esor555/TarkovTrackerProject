using Microsoft.AspNetCore.Authentication.Cookies;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;
using TarkovTrackerDAL.test;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages
builder.Services.AddRazorPages();

// Configure Cookie Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

// Add Authorization
builder.Services.AddAuthorization();

// Configure Dependency Injection
builder.Services.AddScoped<IUserQuestRepository>(provider =>
    new UserQuestRepository(builder.Configuration.GetConnectionString("1")));
builder.Services.AddScoped<IUserQuestService, UserQuestService>();
builder.Services.AddScoped<UserQuestPageService>(); 

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();