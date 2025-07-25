using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasIndex(r => r.Name).IsUnique();

        builder.HasData(
            new Recipe
            {
                Id = 1,
                Name = "120A104",
                Title = "AC32 HDM Base 40/60 REC (HYBRID)",
                CreatedDate = new DateTime(2023, 06, 15, 10, 10, 56),
            },
            new Recipe
            {
                Id = 2,
                Name = "120A104D",
                Title = "AC32 HDM Base 40/60 DES (HYBRID)",
                CreatedDate = new DateTime(2024, 03, 21, 13, 15, 23),
            },
            new Recipe
            {
                Id = 3,
                Name = "120A104Z",
                Title = "WARM AC32 HDM Base 40/60 REC (HYBRID)",
                CreatedDate = new DateTime(2024, 03, 21, 08, 15, 29),
            }
        );
    }
}
