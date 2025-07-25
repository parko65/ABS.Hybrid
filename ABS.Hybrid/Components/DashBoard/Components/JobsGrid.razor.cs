using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.DashBoard.Components;
public partial class JobsGrid
{
    [Parameter]
    public List<JobDto>? Jobs { get; set; }

    private JobDto? SelectedJob { get; set; }

    PaginationState pagination = new() { ItemsPerPage = 10 };

    [Parameter]
    public EventCallback<JobDto?> OnJobSelected { get; set; }

    private void HandleRowClick(FluentDataGridRow<JobDto> job)
    {
        SelectedJob = job.Item;
        OnJobSelected.InvokeAsync(SelectedJob);
    }

    private string? GetRowClass(JobDto job)
    {
        return SelectedJob == job ? "selected-row" : null;
    }
}
