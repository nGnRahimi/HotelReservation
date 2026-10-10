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
    internal class ReserveDailyDetailConfiguration : IEntityTypeConfiguration<ReserveDailyDetail>
    {
        public void Configure(EntityTypeBuilder<ReserveDailyDetail> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasOne(s => s.ReserveDetail)
                .WithMany(s => s.ReserveDailyDetails)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(s => s.ReserveDetailId);
        }
    }
}
