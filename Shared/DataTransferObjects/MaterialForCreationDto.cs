using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public class MaterialForCreationDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Material number must be between 1 and 1000.")]
    public int MaterialNumber { get; set; }

    [Required(ErrorMessage = "Material name is a required field.")]
    [MaxLength(60, ErrorMessage = "Material name cannot exceed 60 characters.")]
    public string Name { get; set; } = string.Empty;
}
