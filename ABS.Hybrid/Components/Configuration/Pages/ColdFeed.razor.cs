using Microsoft.FluentUI.AspNetCore.Components;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.Configuration.Pages;
public partial class ColdFeed
{
    private readonly MaterialPageService _materialPageService;    

    public ColdFeed(IServiceManager service, IDialogService dialogService, IToastService toastService)
    {
        _materialPageService = new MaterialPageService(service, dialogService, toastService);
    }

    protected async override Task OnInitializedAsync()
    {
        await _materialPageService.LoadMaterialsAsync("Coldfeed");
    }

    private async Task LoadMaterialsAsync()
    {
        await _materialPageService.LoadMaterialsAsync("Small Dose Additive");
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
    }

    // Properties to expose service state to the Razor view
    public IEnumerable<MaterialDto> Materials => _materialPageService.Materials;
    public MaterialTypeDto? Type => _materialPageService.Type;
    public bool IsDeleteDisabled => _materialPageService.IsDeleteDisabled;
    public MaterialDto? SelectedMaterial => _materialPageService.SelectedMaterial;
}
