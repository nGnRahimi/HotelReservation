

using Domain.Models.Cancelation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class CancelationRuleConfiguration : IEntityTypeConfiguration<CancelationRule>
    {
        public void Configure(EntityTypeBuilder<CancelationRule> builder)
        {
            builder.HasKey(x => x.Id);

           
            builder.HasOne(x => x.CancelationPolicy)
                .WithMany(x => x.CancelationRules)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.CancelationPolicyId);
        }
    }
}
