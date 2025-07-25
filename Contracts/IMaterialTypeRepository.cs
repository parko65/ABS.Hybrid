using Entities.Models;

namespace Contracts;
public interface IMaterialTypeRepository
{
    Task<IEnumerable<MaterialType>> GetMaterialTypesAsync(bool trackChanges);
    Task<MaterialType?> GetMaterialTypeAsync(int id, bool trackChanges);
    Task<MaterialType?> GetMaterialTypeByNameAsync(string name, bool trackChanges);
}