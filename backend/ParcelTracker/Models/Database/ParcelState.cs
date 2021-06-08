using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParcelTracker.Models.Database
{
    public class ParcelState

    {
        public int Id { get; set; }

        public Guid ParcelId { get; set; }

        public Parcel Parcel { get; set; }

        public int StateId { get; set; }

        public State State { get; set; }

        public DateTimeOffset Created { get; set; }
    }

    public class ParcelStateEntityTypeConfiguration : IEntityTypeConfiguration<ParcelState>
    {
        public void Configure(EntityTypeBuilder<ParcelState> builder)
        {
            builder.ToTable("ParcelStates");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Created).IsRequired();
            builder.HasOne(s => s.State).WithMany(s => s.ParcelStates).HasForeignKey(s => s.StateId).IsRequired();
            builder.HasOne(s => s.Parcel).WithMany(p => p.ParcelStates).HasForeignKey(s => s.ParcelId).IsRequired();
        }
    }
}