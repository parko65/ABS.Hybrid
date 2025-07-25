using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace ABS.Hybrid;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Recipe Dtos
        CreateMap<Recipe, RecipeDto>();
        CreateMap<RecipeForCreationDto, Recipe>();

        // MaterialType Dtos
        CreateMap<MaterialType, MaterialTypeDto>();

        // Material Dtos
        CreateMap<Material, MaterialDto>();
        CreateMap<MaterialForCreationDto, Material>();
        CreateMap<MaterialForUpdateDto, Material>();

        // StorageUnit Dtos
        CreateMap<StorageUnit, StorageUnitDto>();

        // RecipeStorageUnit Dtos
        CreateMap<RecipeStorageUnit, RecipeStorageUnitDto>();
        CreateMap<RecipeStorageUnitForCreationDto, RecipeStorageUnit>();

        // Job Dtos
        CreateMap<Job, JobDto>();
        CreateMap<JobForCreationDto, Job>();

        // Destination Dtos
        CreateMap<Destination, DestinationDto>();
    }
}
