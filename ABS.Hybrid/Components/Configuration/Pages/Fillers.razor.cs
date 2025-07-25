using Service.Contracts;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.Configuration.Pages;

public partial class Fillers
{
    private readonly MaterialPageService _materialPageService;

    public Fillers(IServiceManager service, IDialogService dialogService, IToastService toastService)
    {
        _materialPageService = new MaterialPageService(service, dialogService, toastService);
    }

    protected async override Task OnInitializedAsync()
    {
        await _materialPageService.LoadMaterialsAsync("Filler");
    }

    private async Task LoadMaterialsAsync()
    {
        await _materialPageService.LoadMaterialsAsync("Filler");
    }

    private async Task OpenMaterialForCreationDialog()
    {
        await _materialPageService.OpenMaterialForCreationDialog();
        await LoadMaterialsAsync(); // Reload after creation
    }

    private void HandleMaterialSelected(MaterialDto selectedMaterial)
    {
        _materialPageService.HandleMaterialSelected(selectedMaterial);
    }

    private async Task DeleteMaterialAsync()
    {
        await _materialPageService.DeleteMaterialAsync();
        await LoadMaterialsAsync(); // Reload after deletion
    }    // Properties to expose service state to the Razor view
    public IEnumerable<MaterialDto> Materials => _materialPageService.Materials;
    public MaterialTypeDto? Type => _materialPageService.Type;
    public bool IsDeleteDisabled => _materialPageService.IsDeleteDisabled;
    public MaterialDto? SelectedMaterial => _materialPageService.SelectedMaterial;
}
