using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IMaterialTypeService
{
    Task<IEnumerable<MaterialTypeDto>> GetMaterialTypesAsync(bool trackChanges);
    Task<MaterialTypeDto> GetMaterialTypeAsync(int id, bool trackChanges);
    Task<MaterialTypeDto> GetMaterialTypeByNameAsync(string name, bool trackChanges);
}
