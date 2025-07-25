using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.DashBoard.Components;

public partial class JobCreationForm
{
    [Parameter]
    public List<DestinationDto>? Destinations { get; set; }

    [Parameter]
    public EventCallback<JobForCreationDto> OnJobCreated { get; set; }

    private JobForCreationDto _jobForCreation { get; set; } = new();
    private DestinationDto? _selectedDestination;    

    private EditContext _editContext = default!;

    protected override void OnInitialized() =>
        _editContext = new EditContext(_jobForCreation);    

    private async Task CreateJobAsync()
    {
        var result = _editContext.Validate();
        if (result)
        {
            if (_selectedDestination is null)
            {
                Debug.WriteLine("Selected destination is null");
                return;
            }

            _jobForCreation.DestinationId = _selectedDestination.Id;
            await OnJobCreated.InvokeAsync(_jobForCreation);
        }
        else
        {
            Console.WriteLine("Validation failed. Please check the input.");
        }
    }
}
