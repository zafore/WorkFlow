using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkFlow.Data;
using WorkFlow.Models;
using WorkFlow.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new ActiveDirectoryService("your-domain.com"));

// Add Authentication Service
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });


// ����� ����� ������� �������� ��������� (IV)
var key = "xBk7xN7S2b-xJDyGIqiWzHtihEsm0eWZILnmTl4I4qY";
var iv = "abed9bd622361278a9e9f652876d9bc5";

//// ����� ���� �������
//builder.Services.AddSingleton(new EncryptionService(key, iv));

//// ����� �������
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<DecryptRequestFilter>();
//    options.Filters.Add<EncryptResponseFilter>();
//});
builder.Services.AddDbContext<Workflow2Context>(options => options.UseSqlServer(connectionString, serverOptions =>
{
    serverOptions.EnableRetryOnFailure(
        maxRetryCount: 3,       // Maximum number of retry attempts
        maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
        errorNumbersToAdd: null); // Specific error numbers to retry on (null retries on all transient errors)
}));
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<WorkFlow2Context>(options =>
//    options.UseSqlServer(connectionString));

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//  .AddNegotiate();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Removed ASP.NET Core Identity to use custom authentication
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


