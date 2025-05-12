using Microsoft.AspNetCore.Authentication.Cookies;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.DAL;
using TTBusinesLogic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

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
		options.LoginPath = "/Login";  // Path for login page
		options.LogoutPath = "/Logout"; // Path for logout
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Session expiry time
		options.SlidingExpiration = true; // Automatically renew cookie expiration
	});

builder.Services.AddAuthorization();
builder.Services.AddScoped<IUserQuestRepository>(provider =>
	new UserQuestRepository(builder.Configuration.GetConnectionString("1")));

builder.Services.AddScoped<IUserQuestService, UserQuestService>();



var app = builder.Build();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();