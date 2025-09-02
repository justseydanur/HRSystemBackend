using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace HRSystem.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Buraya kendi SQL Server bağlantı stringini yaz
            optionsBuilder.UseSqlServer("Server=.;Database=HRSystemDB;Trusted_Connection=True;TrustServerCertificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
    //Açıklamalar:
    //DbSet < User > → Users tablosunu temsil eder
    //Constructor → Program.cs veya Startup.cs’de database bağlantısı için kullanılır
    //Bu DbContext artık repository ve servislerde kullanılacak.