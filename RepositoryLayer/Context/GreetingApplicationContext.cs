using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;

namespace RepositoryLayer.Context
{
    public class GreetingApplicationContext : DbContext
    {
        public GreetingApplicationContext(DbContextOptions<GreetingApplicationContext> options) : base(options) { }
            public virtual DbSet<GreetingEntity> Greetings { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        // ADD THIS FOR MIGRATION PURPOSES
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-9VG166Q4\\SQLEXPRESS; Database=GreetingApplicationAPI; Trusted_Connection=True; MultipleActiveResultSets=true;");
            }
        }
    }
}