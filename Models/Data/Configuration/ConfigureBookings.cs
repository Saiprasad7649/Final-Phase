using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Equinox.Models.DomainModels;

namespace Equinox.Models.Data.Configuration
{
    public class ConfigureBookings : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> entity)
        {
            // Configure primary key
            entity.HasKey(b => b.BookingId);

            // Configure relationships
            entity.HasOne(b => b.EquinoxClass)
                  .WithMany(ec => ec.Bookings)
                  .HasForeignKey(b => b.EquinoxClassId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            entity.HasData(
                new Booking { BookingId = 1, EquinoxClassId = 1 },
                new Booking { BookingId = 2, EquinoxClassId = 1 },
                new Booking { BookingId = 3, EquinoxClassId = 2 },
                new Booking { BookingId = 4, EquinoxClassId = 3 }
            );
        }
    }
}
