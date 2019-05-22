using System;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class DemoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DemoDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-CP2NEHV\\SQLEXPRESS;Integrated Security=True;Database=DemoDB;Trusted_Connection=True");
           // optionsBuilder.UseSqlServer(@"Server=DESKTOP-CP2NEHV\\SQLEXPRESS;Integrated Security=True;Database=DemoDB");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
