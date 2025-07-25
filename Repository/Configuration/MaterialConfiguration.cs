using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasData
            (
            new Material
            {
                Id = 1,
                MaterialNumber = 1,
                Name = "Dust/Sand",
                MaterialTypeId = 1
            },
            new Material
            {
                Id = 2,
                MaterialNumber = 2,
                Name = "6mm",
                MaterialTypeId = 1
            },
            new Material
            {
                Id = 3,
                MaterialNumber = 3,
                Name = "10mm",
                MaterialTypeId = 1
            },
            new Material
            {
                Id = 4,
                MaterialNumber = 4,
                Name = "14mm",
                MaterialTypeId = 1
            },
            new Material
            {
                Id = 5,
                MaterialNumber = 5,
                Name = "20mm",
                MaterialTypeId = 1
            },
            new Material
            {
                Id = 6,
                MaterialNumber = 6,
                Name = "24mm+",
                MaterialTypeId = 1
            },
            new Material
            {
                Id = 7,
                MaterialNumber = 50,
                Name = "40-60 Pen Bitumen",
                MaterialTypeId = 2
            },
            new Material
            {
                Id = 8,
                MaterialNumber = 1001,
                Name = "Masterflex Binder",
                MaterialTypeId = 2
            }
            );
    }
}
