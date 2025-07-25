namespace Shared.DataTransferObjects;
public record StorageUnitDto(int Id, string Name, MaterialDto? Material, MaterialTypeDto? MaterialType)
{
    public bool Selected { get; set; }
    public string? Take { get; set; }
}