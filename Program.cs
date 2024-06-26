using Microsoft.EntityFrameworkCore;
using DiscBudV1.Data;
using Microsoft.AspNetCore.Identity;
using DiscBudV1.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DiscBudV1Connection");

builder.Services.AddDbContext<DiscBudV1Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<DiscBudV1User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DiscBudV1Context>();
builder.Services.AddRazorPages();
builder.Services.AddLogging();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
});
//builder.Services.AddDbContext <DiscBudV1Context> (options =>
//options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
