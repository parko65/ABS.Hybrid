using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.Recipes.Pages;
public partial class CreateRecipe
{
    private readonly IServiceManager _service;
    private readonly NavigationManager _nav;
    private readonly IToastService _toastService;

    private bool _isDisabled = true;

    public CreateRecipe(IServiceManager service, NavigationManager nav, IToastService toastService)
    {
        _service = service;
        _nav = nav;
        _toastService = toastService;
    }

    private RecipeForCreationDto Recipe { get; set; } = new();
    //static IQueryable<StorageUnitDto>? AvailableStorageUnits;
    static IQueryable<StorageUnitDto>? AvailableHotBins;
    static IQueryable<StorageUnitDto>? AvailableColdfeeders;
    static IQueryable<StorageUnitDto>? AvailableBitumenTanks;
    static IQueryable<StorageUnitDto>? AvailableFillerSilos;
    static IQueryable<StorageUnitDto>? AvailableAdditiveTanks;

    private bool IsSubmitting { get; set; }
    
    private string ErrorMessage { get; set; } = string.Empty;
    private IEnumerable<StorageUnitDto> SelectedHotBins = [];
    private IEnumerable<StorageUnitDto> SelectedColdfeeders = [];
    private IEnumerable<StorageUnitDto> SelectedBitumenTanks = [];
    private IEnumerable<StorageUnitDto> SelectedFillerSilos = [];
    private IEnumerable<StorageUnitDto> SelectedAdditiveTanks = [];    

    protected async override Task OnInitializedAsync()
    {
        InitializeRecipe();
        await LoadStorageUnits();   
    }

    private void InitializeRecipe()
    {
        Recipe = new RecipeForCreationDto
        {
            VersionNumber = 1,
            CreatedDate = DateTime.Now,
            IsValid = true,
            BatchSize = 3000,
            IsBatchSizeFixed = false,
            MixTime = 25,
            MixTemperature = 0,
            LowerTemperatureDeviation = 0,
            UpperTemperatureDeviation = 0,
            RecipeStorageUnits = []
        };
    }

    private async Task LoadStorageUnits()
    {
        try
        {
            var units = await _service.StorageUnitService.GetStorageUnitsWithMaterialAsync(trackChanges: false);
            //AvailableStorageUnits = units.Where(s => s.Material?.Name != null).AsQueryable();
            AvailableHotBins = units.Where(s => s.MaterialType?.Name == "Aggregate" && s.Material?.Name != null).AsQueryable();
            AvailableColdfeeders = units.Where(s => s.MaterialType?.Name == "Coldfeed" && s.Material?.Name != null).AsQueryable();
            AvailableBitumenTanks = units.Where(s => s.MaterialType?.Name == "Bitumen" && s.Material?.Name != null).AsQueryable();
            AvailableFillerSilos = units.Where(s => s.MaterialType?.Name == "Filler" && s.Material?.Name != null).AsQueryable();
            AvailableAdditiveTanks = units.Where(s => s.MaterialType?.Name == "Additive" && s.Material?.Name != null).AsQueryable();

            // Pre-select the first storage unit
            if (AvailableHotBins.Any())
            {
                var firstUnit = AvailableHotBins.First();
                SelectedHotBins = new[] { firstUnit };
            }

            if (AvailableColdfeeders.Any())
            {
                var firstUnit = AvailableColdfeeders.First();
                SelectedColdfeeders = new[] { firstUnit };
            }

            if (AvailableBitumenTanks.Any())
            {
                var firstUnit = AvailableBitumenTanks.First();
                SelectedBitumenTanks = new[] { firstUnit };
            }

            if (AvailableFillerSilos.Any())
            {
                var firstUnit = AvailableFillerSilos.First();
                SelectedFillerSilos = new[] { firstUnit };
            }

            if (AvailableAdditiveTanks.Any())
            {
                var firstUnit = AvailableAdditiveTanks.First();
                SelectedAdditiveTanks = new[] { firstUnit };
            }

            StateHasChanged();
        }
        catch (Exception)
        {
            ErrorMessage = "Failed to load storage units. Please refresh the page and try again.";
            _toastService.ShowError("Error loading storage units");
        }
    }
    
    private async Task HandleValidSubmit()
    {
        IsSubmitting = true;
        ErrorMessage = string.Empty;

        try
        {
            var hotBins = SelectedHotBins              
                .Select(su => new RecipeStorageUnitForCreationDto
                {
                    StorageUnitId = su.Id,                    
                    Take = double.TryParse(su.Take, out var take) ? take : 0
                }).ToList();

            var coldfeeders = SelectedColdfeeders
                .Select(su => new RecipeStorageUnitForCreationDto
                {
                    StorageUnitId = su.Id,
                    Take = double.TryParse(su.Take, out var take) ? take : 0
                }).ToList();

            var bitumenTanks = SelectedBitumenTanks
                .Select(su => new RecipeStorageUnitForCreationDto
                {
                    StorageUnitId = su.Id,
                    Take = double.TryParse(su.Take, out var take) ? take : 0
                }).ToList();

            var fillerSilos = SelectedFillerSilos
                .Select(su => new RecipeStorageUnitForCreationDto
                {
                    StorageUnitId = su.Id,
                    Take = double.TryParse(su.Take, out var take) ? take : 0
                }).ToList();

            var additiveTanks = SelectedAdditiveTanks
                .Select(su => new RecipeStorageUnitForCreationDto
                {
                    StorageUnitId = su.Id,
                    Take = double.TryParse(su.Take, out var take) ? take : 0
                }).ToList();

            Recipe.RecipeStorageUnits = hotBins
                .Concat(coldfeeders)
                .Concat(bitumenTanks)
                .Concat(fillerSilos)
                .Concat(additiveTanks)
                .ToList();

            // if the sum of all takes is not 100 _disable the submit button
            var totalTake = Recipe.RecipeStorageUnits.Sum(su => su.Take);
            if (totalTake <= 98 || totalTake >= 101)
            {
                _isDisabled = false;
                ErrorMessage = "The total of all takes must equal 100%. Please adjust the values.";
                _toastService.ShowWarning("Total take must equal 100%");
                return;
            }

            var createdRecipe = await _service.RecipeService.CreateRecipeAsync(Recipe, trackChanges: false);

            _toastService.ShowSuccess($"Recipe '{createdRecipe.Name}' created successfully!");
            _nav.NavigateTo("/");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to create recipe: {ex.Message}";
            _toastService.ShowError("Failed to create recipe");
        }
        finally
        {
            IsSubmitting = false;
        }
    }

    private void HandleInvalidSubmit()
    {
        ErrorMessage = "Please correct the validation errors and try again.";
        _toastService.ShowWarning("Please correct the form errors");
    }

    private void Cancel()
    {
        _nav.NavigateTo("/");
    }
} 