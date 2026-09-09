using Domain.Models.Prices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class RoomPricesConfiguration : IEntityTypeConfiguration<RoomPrice>
    {
        public void Configure(EntityTypeBuilder<RoomPrice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Room)
                .WithMany(x => x.RoomPrices)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.RoomId);
        }
    }
}
