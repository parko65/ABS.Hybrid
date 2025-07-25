using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class MaterialTypeConfiguration : IEntityTypeConfiguration<MaterialType>
{
    public void Configure(EntityTypeBuilder<MaterialType> builder)
    {
        builder.HasData
            (
            new MaterialType
            {
                Id = 1,
                Name = "Aggregate"
            },
            new MaterialType
            {
                Id = 2,
                Name = "Bitumen"
            },
            new MaterialType
            {
                Id = 3,
                Name = "Filler"
            },
            new MaterialType
            {
                Id = 4,
                Name = "Fixed Additive"
            },
            new MaterialType
            {
                Id = 5,
                Name = "Coldfeed"
            },
            new MaterialType
            {
                Id = 6,
                Name = "Additive"
            },
            new MaterialType
            {
                Id = 7,
                Name = "Reclaimed Asphalt"
            },
            new MaterialType
            {
                Id = 8,
                Name = "Small Dose Additive"
            },
            new MaterialType
            {
                Id = 9,
                Name = "Return Dust"
            }
            );
    }
}
