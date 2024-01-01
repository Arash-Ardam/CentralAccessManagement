using CrudDbAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CrudDbAccess.Configurations.Database
{
    public static class DbContextConfig
    {
        public static void AddDatabaseConfigurations(this WebApplicationBuilder app) 
        {
            var dbSection = app.Configuration.GetSection("dbContext");
            var dbOptions = dbSection.Get<DbContextOptions>();

            if (dbOptions.isEnabled && !dbOptions.isInMemory)
            {
                app.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(dbOptions.connectionString),ServiceLifetime.Scoped);
            }

            else if(dbOptions.isEnabled && dbOptions.isInMemory)
            {
                app.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase(nameof(ApplicationDbContext)));
            }
        }

    }
}
