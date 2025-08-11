using System;
using Microsoft.EntityFrameworkCore;
using Equinox.Models.DomainModels;
using System.Reflection;

namespace Equinox.Models.Data
{
    public class EquinoxContext : DbContext
    {
        public EquinoxContext(DbContextOptions<EquinoxContext> options) 
            : base(options) 
        { }

        public DbSet<EquinoxClass> EquinoxClasses => Set<EquinoxClass>();
        public DbSet<Club> Clubs => Set<Club>();
        public DbSet<ClassCategory> ClassCategories => Set<ClassCategory>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
