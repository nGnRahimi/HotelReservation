

using Domain.Models.Cancelation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class CancelationPolicyConfiguration : IEntityTypeConfiguration<CancelationPolicy>
    {
        public void Configure(EntityTypeBuilder<CancelationPolicy> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar");


            builder.HasOne(x => x.Hotel)
                .WithMany(x => x.CancelationPolicies)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.HotelId);
        }
    }
}
