using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Recipe
{
    [Column("RecipeId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Recipe name is a required field.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the name is 60 characters.")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Title is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the title is 100 characters.")]
    public string? Title { get; set; }

    public int VersionNumber { get; set; }

    public DateTime CreatedDate { get; set; }    

    public bool IsValid { get; set; }

    public int BatchSize { get; set; }

    public bool IsBatchSizeFixed { get; set; }

    public int MixTime { get; set; }

    public int MixTemperature { get; set; }

    public int LowerTemperatureDeviation { get; set; }

    public int UpperTemperatureDeviation { get; set; }

    public ICollection<Job>? Jobs { get; set; }
    public ICollection<RecipeStorageUnit>? RecipeStorageUnits { get; set; }
}
