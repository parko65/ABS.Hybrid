namespace Shared.DataTransferObjects;

public record RecipeDto(
    int Id,
    string Name,
    string Title,
    int VersionNumber,
    DateTime CreatedDate,    
    bool IsValid,
    int BatchSize,
    bool IsBatchSizeFixed,
    int MixTime,
    int MixTemperature,
    int LowerTemperatureDeviation,
    int UpperTemperatureDeviation,
    IEnumerable<RecipeStorageUnitDto>? RecipeStorageUnits
    );