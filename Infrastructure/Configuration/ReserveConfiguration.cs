using Domain.Models.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    internal class ReserveConfiguration : IEntityTypeConfiguration<Reserve>
    {
        public void Configure(EntityTypeBuilder<Reserve> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasOne(s => s.Hotel)
                .WithMany(s => s.Reserves)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(s => s.HotelId);

            builder.HasOne(s => s.User)
                .WithMany(s => s.Reserves)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(s => s.UserId);
        }
    }
}
