using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public class RecipeForCreationDto
{
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
    public IEnumerable<RecipeStorageUnitForCreationDto>? RecipeStorageUnits { get; set; }
}
