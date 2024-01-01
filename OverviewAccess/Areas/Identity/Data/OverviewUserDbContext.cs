using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OverviewAccess.Areas.Identity.Data;

namespace OverviewAccess.Data;

public class OverviewUserDbContext : IdentityDbContext<OverviewIdentityUser>
{
    public OverviewUserDbContext(DbContextOptions<OverviewUserDbContext> options)
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
}
