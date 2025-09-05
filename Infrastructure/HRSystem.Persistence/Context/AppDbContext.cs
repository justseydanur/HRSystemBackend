using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HRSystem.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Department entity configuration
            modelBuilder.Entity<Department>(b =>
            {
                b.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                b.HasIndex(x => x.Name)
                    .IsUnique(); // Aynı isimden sadece bir tane olsun
            });
        }
    }

    // Design-time factory (migration için gerekli)
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // SQL Server bağlantı stringi
            optionsBuilder.UseSqlServer("Server=.;Database=HRSystemDB;Trusted_Connection=True;TrustServerCertificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
