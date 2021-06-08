using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParcelTracker.Models.Database
{
    public class Parcel
    {
        public Guid Id { get; set; }

        public Address ReturnAddress { get; set; }

        public Address DeliveryAddress { get; set; }

        public IEnumerable<ParcelState> ParcelStates { get; set; }
    }

    public class ParcelEntityTypeConfiguration : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.ToTable("Parcels");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.OwnsOne(p => p.DeliveryAddress, ConfigureAddress);
            builder.OwnsOne(p => p.ReturnAddress, ConfigureAddress);
        }

        private static void ConfigureAddress(OwnedNavigationBuilder<Parcel, Address> addressBuilder)
        {
            addressBuilder.Property(a => a.Name).HasColumnType("varchar(100)").IsRequired();
            addressBuilder.Property(a => a.Street).HasColumnType("varchar(50)").IsRequired();
            addressBuilder.Property(a => a.HouseNumber).HasColumnType("varchar(20)").IsRequired();
            addressBuilder.Property(a => a.PostalCode).HasColumnType("varchar(20)").IsRequired();
            addressBuilder.Property(a => a.City).HasColumnType("varchar(50)").IsRequired();
            addressBuilder.Property(a => a.Country).HasColumnType("varchar(50)").IsRequired();
            addressBuilder.WithOwner();
        }
    }
}