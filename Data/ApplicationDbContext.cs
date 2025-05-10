using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket_Listener_Worker.src.Models;

namespace WebSocket_Listener_Worker.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Stock> stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>().HasNoKey(); 
            modelBuilder.Entity<Stock>().ToTable("Stocks");
            modelBuilder.Entity<Stock>().Property(s => s.Symbol).HasColumnName("Symbol");
        }
    }
}
