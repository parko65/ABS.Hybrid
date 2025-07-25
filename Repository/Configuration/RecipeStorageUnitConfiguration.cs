using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class RecipeStorageUnitConfiguration : IEntityTypeConfiguration<RecipeStorageUnit>
{
    public void Configure(EntityTypeBuilder<RecipeStorageUnit> builder) =>
        builder.HasKey(rs => new { rs.RecipeId, rs.StorageUnitId });
}
