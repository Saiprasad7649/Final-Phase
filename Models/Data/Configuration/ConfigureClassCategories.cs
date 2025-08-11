using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Equinox.Models.DomainModels;

namespace Equinox.Models.Data.Configuration
{
    public class ConfigureClassCategories : IEntityTypeConfiguration<ClassCategory>
    {
        public void Configure(EntityTypeBuilder<ClassCategory> entity)
        {
            // Configure primary key
            entity.HasKey(c => c.ClassCategoryId);

            // Configure relationships
            entity.HasMany(c => c.EquinoxClasses)
                  .WithOne(ec => ec.ClassCategory)
                  .HasForeignKey(ec => ec.ClassCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            entity.HasData(
                new ClassCategory { ClassCategoryId = 1, Name = "Yoga" },
                new ClassCategory { ClassCategoryId = 2, Name = "HIIT" },
                new ClassCategory { ClassCategoryId = 3, Name = "Cardio" },
                new ClassCategory { ClassCategoryId = 4, Name = "Strength" }
            );
        }
    }
}
