using Microsoft.AspNetCore.Authentication.Cookies;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;
using TarkovTrackerDAL.test;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();


var connStr = builder.Configuration.GetConnectionString("1");



// Repositories with connection string
builder.Services.AddScoped<IUserQuestRepository>(provider => new UserQuestRepository(connStr));
builder.Services.AddScoped<IquestRepository>(provider => new QuestRepository(connStr));
builder.Services.AddScoped<IUserHideoutRepository>(provider => new UserHideoutRepository(connStr));
builder.Services.AddScoped<IhideoutstationRepository>(provider => new HideoutStationRepository(connStr));

// Services with dependencies properly injected:
builder.Services.AddScoped<UserService>(provider => new UserService(connStr));

builder.Services.AddScoped<UserQuestService>(provider =>
{
    var userQuestRepo = provider.GetRequiredService<IUserQuestRepository>();
    return new UserQuestService(userQuestRepo);
});

builder.Services.AddScoped<QuestService>(provider =>
{
    return new QuestService(connStr);
});

builder.Services.AddScoped<UserQuestPageService>(provider =>
{
    var userQuestService = provider.GetRequiredService<UserQuestService>();
    // If UserQuestPageService also needs QuestService, inject it here:
    // var questService = provider.GetRequiredService<QuestService>();
    return new UserQuestPageService(userQuestService);
});

builder.Services.AddScoped<UserHideoutService>(provider =>
{
    var userHideoutRepo = provider.GetRequiredService<IUserHideoutRepository>();
    return new UserHideoutService(userHideoutRepo);
});

builder.Services.AddScoped<HideoutStationService>(provider =>
{
    return new HideoutStationService(connStr);
});

builder.Services.AddScoped<UserHideoutPageService>(provider =>
{
    var userHideoutService = provider.GetRequiredService<UserHideoutService>();
    var hideoutStationService = provider.GetRequiredService<HideoutStationService>();
    return new UserHideoutPageService(userHideoutService, hideoutStationService);
});


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

// Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();