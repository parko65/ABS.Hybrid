using Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PLCEntities;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.DashBoard.Pages;
public partial class DashBoard : IAsyncDisposable
{
    private readonly IServiceManager _service;
    private readonly NavigationManager _nav;

    private List<string> messages = [];

    private bool jobLoaded;

    private TagModel _tagModel = new();

    private bool _isListening = false;

    private int i;

    private bool _isPlcConnected { get; set; } = false;

    private HubConnection? _hubConnection;

    public bool IsConnected =>
        _hubConnection?.State == HubConnectionState.Connected;

    public DashBoard(IServiceManager service, NavigationManager nav)
    {
        _service = service;
        _nav = nav;
    }

    private List<RecipeDto>? _recipes = [];
    private List<DestinationDto>? _destinations = [];
    private List<JobDto>? _jobs = [];

    private RecipeDto? _selectedRecipe;
    private JobDto? _selectedJob;

    protected async override Task OnInitializedAsync()
    {
        var recipes = await _service.RecipeService.GetRecipesAsync(trackChanges: false);
        var destinations = await _service.DestinationService.GetDestinationsAsync(trackChanges: false);        

        _recipes = [.. recipes];
        _destinations = [.. destinations];

        await LoadJobsAsync();

        if (_recipes.Count > 0)
        {
            _selectedRecipe = _recipes[0];
        }

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_nav.ToAbsoluteUri("https://abs.io:8080/chathub"))
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<TagModel>("ReceivePLCData", (tag) =>
        {
            if (tag is null)
            {
                _isPlcConnected = false;
                InvokeAsync(StateHasChanged);
                return;
            }           

            _isPlcConnected = true;

            // Only process and update UI data when accordion is expanded
            if (!_isListening)
            {
                InvokeAsync(StateHasChanged); // Still need to update for connection status
                return;
            }

            if (tag.JobId == 0) return; // Ignore messages with JobId 0

            _tagModel = tag;
            
            InvokeAsync(StateHasChanged);
            i++;
        });

        await _hubConnection.StartAsync();
    }

    private async Task LoadJobsAsync()
    {
        var jobs = await _service.JobService.GetJobsAsync(DateTime.Now.Date, JobStatus.Created, trackChanges: false);

        _jobs = [.. jobs];
    }

    private void RecipeSelected(RecipeDto recipe)
    {
        _selectedRecipe = recipe;
    }

    private async Task CreateJobAsync(JobForCreationDto jobForCreation)
    {
        if (_selectedRecipe is not null)
            await _service.JobService.CreateJobForRecipeAsync(_selectedRecipe.Id, jobForCreation, trackChanges: false);

        await LoadJobsAsync();
    }

    private void SetSelectedJob(JobDto? job)
    {
        _selectedJob = job;
    }

    private async Task StartMixingAsync()
    {
        var mixerStatus = await _service.PlcReadService.CheckIfMixerReadyAsync();

        if (mixerStatus != 0)
        {
            // Mixer is not ready, show an error message or handle accordingly
            return;
        }

        // The Recipe.Name of the Selected Job needs to be split into 2 ints, one for the upper 16 bits and one for the lower 16 bits.
        var recipeIdUpper = (_selectedJob.Recipe.Id >> 16) & 0xFFFF;
        var recipeIdLower = _selectedJob.Recipe.Id & 0xFFFF;

        var jobNumberUpper = (_selectedJob.JobNumber >> 16) & 0xFFFF;
        var jobNumberLower = _selectedJob.JobNumber & 0xFFFF;

        var destination = _selectedJob.Destination.LocationNumber;

        var tonnage = (int)(_selectedJob.Tonnage * 100); // Convert tonnage to an integer representation

        var basicInfoSent = await _service.PlcWriteService.WriteBasicInfoAsync(
            new int[] {
                _selectedJob.Id,
                recipeIdUpper,
                recipeIdLower,
                jobNumberUpper,
                jobNumberLower,
                destination,
                tonnage
            });

        var bin1Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Bin 1")?.Take ?? 0) * 100;

        var bin2Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Bin 2")?.Take ?? 0) * 100;

        var bin3Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Bin 3")?.Take ?? 0) * 100;

        var bin4Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Bin 4")?.Take ?? 0) * 100;

        var bin5Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Bin 5")?.Take ?? 0) * 100;

        var bin6Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Bin 6")?.Take ?? 0) * 100;

        var hotbinsSent = await _service.PlcWriteService.WriteHotBinsAsync(
            new int[] {
                bin1Take,
                1,
                bin2Take,
                1,
                bin3Take,
                1,
                bin4Take,
                1,
                bin5Take,
                1,
                bin6Take,
                1
            });

        var tank1Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Tank 1")?.Take ?? 0) * 100;

        var tank2Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Tank 2")?.Take ?? 0) * 100;

        var tank3Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Tank 3")?.Take ?? 0) * 100;

        var bitumenTanksSent = await _service.PlcWriteService.WriteBitumenTanksAsync(
            new int[] {
                tank1Take,
                tank2Take,
                tank3Take
            });

        var fill1Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Silo 1")?.Take ?? 0) * 100;

        var fill2Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Silo 2")?.Take ?? 0) * 100;

        var fill3Take = (int)(_selectedJob.Recipe.RecipeStorageUnits!
            .FirstOrDefault(rs => rs.StorageUnit!.Name == "Silo 3")?.Take ?? 0) * 100;

        var fillerSilosSent = await _service.PlcWriteService.WriteFillerSilosAsync(
            new int[] {
                fill1Take,
                fill2Take,
                fill3Take
            });

        var recipeName = _selectedJob.Recipe.Name;
        var recipeNameSent = await _service.PlcWriteService.WriteRecipeNameAsync(recipeName);        

        if (basicInfoSent && hotbinsSent && bitumenTanksSent && fillerSilosSent && recipeNameSent)
        {
            var jobLoaded = await _service.PlcWriteService.StartAsync(1);
        }            
    }

    private async Task CompleteMixingAsync()
    {
        await _service.PlcWriteService.StartAsync(0);
    }

    private void LoggingAccordianChanged(bool isExpanded)
    {
        _isListening = isExpanded;
    }

    private async Task DeleteSelectedJob()
    {
        await _service.JobService.DeleteJobAsync(_selectedJob!.Id, trackChanges: false);

        await LoadJobsAsync();
    }

    private async Task DeleteRecipeAsync()
    {
        if (_selectedRecipe is not null)
        {
            await _service.RecipeService.DeleteRecipeAsync(_selectedRecipe.Id, trackChanges: false);
            _recipes!.Remove(_selectedRecipe);
            _selectedRecipe = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }        
    }
}