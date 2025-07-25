using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class MaterialType
{
    [Column("MaterialTypeId")]
    public int Id { get; set; }    

    [Required(ErrorMessage = "Material type name is required.")]
    [MaxLength(60, ErrorMessage = "Material type name cannot exceed 60 characters.")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Material>? Materials { get; set; }
    public ICollection<StorageUnit>? StorageUnits { get; set; }
}
