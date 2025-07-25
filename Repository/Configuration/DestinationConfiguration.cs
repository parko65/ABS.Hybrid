using Entities.Models;
using Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        builder.HasData(
            new Destination
            {
                Id = 1,
                Name = "NK54 USG",
                DestinationType = DestinationType.Truck
            },
            new Destination
            {
                Id = 2,
                Name = "Bin 1",
                DestinationType = DestinationType.Bin
            },
            new Destination
            {
                Id = 3,
                Name = "Bin 2",
                DestinationType = DestinationType.Bin
            },
            new Destination
            {
                Id = 4,
                Name = "NK71 YMH",
                DestinationType = DestinationType.Truck
            }
            );
    }
}