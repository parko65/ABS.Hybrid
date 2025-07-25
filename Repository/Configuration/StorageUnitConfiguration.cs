using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class StorageUnitConfiguration : IEntityTypeConfiguration<StorageUnit>
{
    public void Configure(EntityTypeBuilder<StorageUnit> builder)
    {
        builder.HasData(
            new StorageUnit
            {
                Id = 1,
                Name = "Bin 1",
                MaterialTypeId = 1
            },
            new StorageUnit
            {
                Id = 2,
                Name = "Bin 2",
                MaterialTypeId = 1
            },
            new StorageUnit
            {
                Id = 3,
                Name = "Bin 3",
                MaterialTypeId = 1
            },
            new StorageUnit
            {
                Id = 4,
                Name = "Bin 4",
                MaterialTypeId = 1
            },
            new StorageUnit
            {
                Id = 5,
                Name = "Bin 5",
                MaterialTypeId = 1
            },
            new StorageUnit
            {
                Id = 6,
                Name = "Bin 6",
                MaterialTypeId = 1
            },
            new StorageUnit
            {
                Id = 7,
                Name = "Tank 1",
                MaterialTypeId = 2
            },
            new StorageUnit
            {
                Id = 8,
                Name = "Tank 2",
                MaterialTypeId = 2
            },
            new StorageUnit
            {
                Id = 9,
                Name = "Silo 1",
                MaterialTypeId = 3
            },
            new StorageUnit
            {
                Id = 10,
                Name = "Silo 2",
                MaterialTypeId = 3
            },
            new StorageUnit
            {
                Id = 11,
                Name = "Silo 3",
                MaterialTypeId = 3
            }
        );
    }
}
