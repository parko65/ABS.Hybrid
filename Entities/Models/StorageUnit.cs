using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class StorageUnit
{
    [Column("StorageUnitId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Storage unit name is a required field.")]
    [MaxLength(60, ErrorMessage = "Storage unit name cannot exceed 60 characters.")]
    public string? Name { get; set; }

    [ForeignKey(nameof(MaterialType))]
    public int MaterialTypeId { get; set; }
    public MaterialType? MaterialType { get; set; }
    
    public Material? Material { get; set; }

    public ICollection<RecipeStorageUnit>? RecipeStorageUnits { get; set; }
}
