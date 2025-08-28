using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
    //Açıklamalar:
    //DbSet < User > → Users tablosunu temsil eder
    //Constructor → Program.cs veya Startup.cs’de database bağlantısı için kullanılır
    //Bu DbContext artık repository ve servislerde kullanılacak.