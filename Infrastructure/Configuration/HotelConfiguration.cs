using Domain.Models.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("nvarchar");



            builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnType("nvarchar");



            builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnType("nvarchar");




            builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("nvarchar");



            builder.Property(x => x.Phone)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("nvarchar");



            builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar");


            builder.HasMany(s => s.Users)
                .WithOne(s => s.Hotel)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(s => s.HotelId);
            

        }
    }
}
