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
    internal class ReserveDetailConfiguration : IEntityTypeConfiguration<ReserveDetail>
    {
        public void Configure(EntityTypeBuilder<ReserveDetail> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasOne(s => s.Reserve)
                .WithMany(s => s.ReserveDetails)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(s => s.ReserveId);

            builder.HasOne(s => s.Room)
                .WithMany(s => s.ReserveDetails)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(s => s.RoomId);
        }
    }
}
