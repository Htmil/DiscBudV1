using Microsoft.EntityFrameworkCore;
using DiscBudV1.Data;
using Microsoft.AspNetCore.Identity;
using DiscBudV1.Areas.Identity.Data;

namespace DiscBudV1 { 
    public class program {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DiscBudV1Connection");

            builder.Services.AddDbContext<DiscBudV1Context>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<DiscBudV1User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DiscBudV1Context>();

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

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            using (var scope = app.Services.CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<DiscBudV1User>>();
                string email = "emil@admin.com";
                string password = "123456";
                if (await _userManager.FindByEmailAsync(email) == null)
                {
                    var user = new DiscBudV1User();
                    user.UserName = email;
                    user.Email = email; 
                    await _userManager.CreateAsync(user, password);
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
            app.Run();
        }
    }
}