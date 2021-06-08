using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParcelTracker.Models.Database
{
    public class State
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ParcelState> ParcelStates { get; set; }
    }

    public class StateEntityTypeConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.HasIndex(s => s.Name).IsUnique();
            builder.Property(s => s.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(s => s.Description).HasColumnType("varchar(200)").IsRequired();
            builder.HasData(new()
            {
                Id = 1,
                Name = "Created",
                Description = "The parcel has been created."
            }, new()
            {
                Id = 2,
                Name = "InTransit",
                Description = "The parcel is currently in transit."
            }, new()
            {
                Id = 3,
                Name = "Sorting",
                Description = "The parcel is currently in being sorted."
            }, new()
            {
                Id = 4,
                Name = "Sorted",
                Description = "The parcel has been sorted and is awaiting delivery."
            }, new()
            {
                Id = 5,
                Name = "OutForDelivery",
                Description = "The parcel is out for delivery.",
            }, new()
            {
                Id = 6,
                Name = "Delivered",
                Description = "The parcel has been delivered."
            });
        }
    }
}