using Domain.Models.HotelGalleries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class HotelGalleryConfiguration : IEntityTypeConfiguration<HotelGallery>
    {
        public void Configure(EntityTypeBuilder<HotelGallery> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Path)
                .IsRequired();

            builder.HasOne(x => x.Hotel)
                .WithMany(x => x.HotelGalleries)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.HotelId);
        }
    }
}
