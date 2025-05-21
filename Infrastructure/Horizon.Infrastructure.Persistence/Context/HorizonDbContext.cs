using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Horizon.Infrastructure.Persistence.Context;
public class HorizonDbContext(DbContextOptions<HorizonDbContext> options)
    : IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HorizonDbContext).Assembly);
    }
}

