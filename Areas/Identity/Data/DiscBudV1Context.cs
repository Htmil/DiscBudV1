using DiscBudV1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DiscBudV1.Models;

namespace DiscBudV1.Data;

public class DiscBudV1Context : IdentityDbContext<DiscBudV1User>
{
    public DiscBudV1Context(DbContextOptions<DiscBudV1Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<DiscBudV1.Models.Disc>? Discs { get; set; }
}
