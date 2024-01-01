using CrudDbAccess.Data;
using CrudDbAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CrudDbAccess.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        
        public DbSet<BaseAccessData> AccessInformation { get; set; }
        public DbSet<DatabaseDetails> InfraDatabases { get; set; }

        public DbSet<BaseInfo> BaseInfo { get; set; }
        //Table Relations Handeling
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DatabaseDetails>().HasKey(x => x.Id);
            //modelBuilder.Entity<DatabaseDetails>().HasMany<BaseAccessData>().WithOne().IsRequired().OnDelete(DeleteBehavior.Cascad);

            modelBuilder.Entity<BaseAccessData>()
                .HasOne<DatabaseDetails>(model => model.From)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);


            modelBuilder.Entity<BaseAccessData>()
                .HasOne<DatabaseDetails>(model => model.To)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);




        }
    }
}
