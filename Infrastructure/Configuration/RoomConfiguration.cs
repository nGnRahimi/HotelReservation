using Domain.Models.Rooms;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Path)
                .IsRequired();

            builder.HasOne(x => x.Hotel)
                .WithMany(x => x.Rooms)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.HotelId);
        }
    }
}
