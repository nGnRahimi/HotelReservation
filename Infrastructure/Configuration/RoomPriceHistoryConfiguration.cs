
using Domain.Models.Prices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class RoomPriceHistoryConfiguration : IEntityTypeConfiguration<RoomPriceHistory>
    {
        public void Configure(EntityTypeBuilder<RoomPriceHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.Room)
                .WithMany(s => s.RoomPriceHistories)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(s => s.RoomId);
        }
    }
}
