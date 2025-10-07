using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Data;
using ProjectApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews(); // Add this line if not present

// Add sign-in tracking services
builder.Services.AddScoped<SignInTrackingService>();
builder.Services.AddScoped<SignInEventHandler>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Add middleware to track sign-ins
app.Use(async (context, next) =>
{
    await next();
    
    // Check if user just signed in
    if (context.User.Identity?.IsAuthenticated == true && 
        context.Request.Path.StartsWithSegments("/Identity/Account/Login") && 
        context.Request.Method == "POST")
    {
        var signInEventHandler = context.RequestServices.GetRequiredService<SignInEventHandler>();
        var email = context.User.Identity.Name;
        if (!string.IsNullOrEmpty(email))
        {
            await signInEventHandler.HandleSignInAsync(email);
        }
    }
});

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
