using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class Material
{
    [Column("MaterialId")]
    public int Id { get; set; }

    public int MaterialNumber { get; set; }

    [Required(ErrorMessage = "Material name is required.")]
    [MaxLength(60, ErrorMessage = "Material name cannot exceed 60 characters.")]
    public string Name { get; set; } = string.Empty;

    [ForeignKey(nameof(MaterialType))]
    public int MaterialTypeId { get; set; }
    public MaterialType? MaterialType { get; set; }
    
    [ForeignKey(nameof(StorageUnit))]
    public int? StorageUnitId { get; set; }
    public StorageUnit? StorageUnit { get; set; }    
}
