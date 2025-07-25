using Microsoft.FluentUI.AspNetCore.Components;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.Configuration.Pages;
public partial class MaterialLoadingReclaim
{
    private readonly IServiceManager _service;

    public MaterialLoadingReclaim(IServiceManager service)
    {
        _service = service;
    }

    // Get the MaterialType we are currently working with
    private MaterialTypeDto? Type { get; set; }

    // Get the list of Materials for the current MaterialType
    private IEnumerable<MaterialDto> Materials { get; set; } = new List<MaterialDto>();

    // Get the list of StorageUnits for the current MaterialType
    private IEnumerable<StorageUnitDto> StorageUnits { get; set; } = new List<StorageUnitDto>();

    // Get the selected Material
    private MaterialDto? SelectedMaterial { get; set; }

    // Get the selected StorageUnit
    private StorageUnitDto? SelectedStorageUnit { get; set; }

    // Flag to disable the UI when no Material is selected
    private bool IsDisabled => SelectedMaterial == null || SelectedStorageUnit == null;

    private IDialogReference? _dialog;

    private bool _isLoading = true;

    protected async override Task OnInitializedAsync()
    {
        // Load the MaterialType and its Materials
        await LoadMaterialsAsync("Reclaimed Asphalt");

        // Load the StorageUnits for the current MaterialType
        await LoadStorageUnitsAsync();

        _isLoading = false;
    }

    private async Task LoadMaterialsAsync(string materialTypeName)
    {
        var materialType = await _service.MaterialTypeService.GetMaterialTypeByNameAsync(materialTypeName, trackChanges: false);

        Type = new MaterialTypeDto(materialType.Id, materialType.Name);

        var materials = await _service.MaterialService.GetMaterialsByTypeAsync(Type.Id, trackChanges: false);

        Materials = materials?.Any() == true ? materials : new List<MaterialDto>();
    }

    private async Task LoadStorageUnitsAsync()
    {
        if (Type == null)
            return;

        var storageUnits = await _service.StorageUnitService.GetStorageUnitsByMaterialTypeWithMaterialAsync(Type.Id, trackChanges: false);

        StorageUnits = storageUnits?.Any() == true ? storageUnits : new List<StorageUnitDto>();
    }

    private void HandleRowClick(FluentDataGridRow<StorageUnitDto> storageUnit)
    {
        SelectedStorageUnit = storageUnit.Item;

        if (SelectedStorageUnit != null)
            SelectedMaterial = SelectedStorageUnit.Material;
    }

    private string? GetRowClass(StorageUnitDto storageUnit)
    {
        return SelectedStorageUnit == storageUnit ? "selected-row" : null;
    }

    private async Task ChangeMaterialStorageUnitAsync()
    {
        var selectedMaterial = SelectedMaterial;

        // Update the Material's StorageUnit
        var materialForUpdate = new MaterialForUpdateDto();

        materialForUpdate.MaterialNumber = selectedMaterial!.MaterialNumber;
        materialForUpdate.Name = selectedMaterial.Name;

        await _service.MaterialService.UpdateMaterialForStorageUnitAsync(SelectedStorageUnit!.Id, Type.Id, selectedMaterial!.Id, materialForUpdate,
            storageUnitTrackChanges: false,
            materialTrackChanges: true);

        // Load the MaterialType and its Materials
        await LoadMaterialsAsync("Reclaime Asphalt");

        // Load the StorageUnits for the current MaterialType
        await LoadStorageUnitsAsync();
    }
}
