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
    public class ReserveGuestConfiguration : IEntityTypeConfiguration<ReserveGuest>
    {
        public void Configure(EntityTypeBuilder<ReserveGuest> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasOne(s => s.ReserveDetail)
                .WithMany(s => s.ReserveGuests)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(s => s.ReserveDetailId);
        }
    }
}
