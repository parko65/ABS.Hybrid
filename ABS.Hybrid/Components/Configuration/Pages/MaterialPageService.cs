using ABS.Hybrid.Components.Configuration.Dialogs;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.Configuration.Pages;

public class MaterialPageService
{
    private readonly IServiceManager _service;
    private readonly IDialogService _dialogService;
    private readonly IToastService _toastService;
    
    public IEnumerable<MaterialDto> Materials { get; set; } = new List<MaterialDto>();
    private IDialogReference? _dialog;
    private bool _isDisabled = true;
    private MaterialDto? _selectedMaterial { get; set; }
    public MaterialTypeDto? Type { get; set; }

    public MaterialPageService(IServiceManager service, IDialogService dialogService, IToastService toastService)
    {
        _service = service;
        _dialogService = dialogService;
        _toastService = toastService;
    }

    public async Task LoadMaterialsAsync(string materialTypeName)
    {
        var materialType = await _service.MaterialTypeService.GetMaterialTypeByNameAsync(materialTypeName, trackChanges: false);

        Type = new MaterialTypeDto(materialType.Id, materialType.Name);

        var materials = await _service.MaterialService.GetMaterialsByTypeAsync(Type.Id, trackChanges: false);

        Materials = materials?.Any() == true ? materials : new List<MaterialDto>();
    }

    public async Task OpenMaterialForCreationDialog()
    {
        var materialForCreation = new MaterialForCreationDto();
        
        _dialog = await _dialogService.ShowPanelAsync<CreateMaterialDialog>(materialForCreation, new DialogParameters<MaterialForCreationDto>()
        {
            Content = materialForCreation,
            Alignment = Microsoft.FluentUI.AspNetCore.Components.HorizontalAlignment.Right,
            Title = $"Create {Type?.Name}",
            PreventDismissOnOverlayClick = true,
            PreventScroll = true
        });

        var result = await _dialog.Result;
        if (!result.Cancelled && result.Data != null)
        {
            var createdMaterial = (MaterialForCreationDto)result.Data;
            await _service.MaterialService.CreateMaterialForTypeAsync(Type!.Id, createdMaterial, trackChanges: false);

            var toastIntent = ToastIntent.Success;
            var toastMessage = $"{Type.Name} created successfully.";
            _toastService.ShowToast(toastIntent, toastMessage);
        }
    }

    public void HandleMaterialSelected(MaterialDto selectedMaterial)
    {
        if (selectedMaterial != null)
        {
            _isDisabled = false;
            _selectedMaterial = selectedMaterial;
        }
        else
        {
            _isDisabled = true;
        }
    }

    public async Task DeleteMaterialAsync()
    {
        if (_selectedMaterial == null)
        {
            _toastService.ShowToast(ToastIntent.Error, "No material selected for deletion.");
            return;
        }

        await _service.MaterialService.DeleteMaterialForMaterialTypeAsync(Type!.Id, _selectedMaterial.Id, trackChanges: false);

        _toastService.ShowToast(ToastIntent.Success, $"{_selectedMaterial.Name} deleted successfully.");

        _selectedMaterial = null;
        _isDisabled = true;
    }

    public bool IsDeleteDisabled => _isDisabled;
    public MaterialDto? SelectedMaterial => _selectedMaterial;
}
