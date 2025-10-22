using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orders.Shared.Entites;

namespace Orders.Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex(x => new { x.Id, x.Name }).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x => new {x.Id, x.Name}).IsUnique();
            DisableCascading(modelBuilder);
        }

        private void DisableCascading(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys());

            foreach (var item in relationships)
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
