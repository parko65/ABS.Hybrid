using Microsoft.AspNetCore.Components;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.DashBoard.Components;
public partial class RecipeDetails
{
    [Parameter]
    public RecipeDto? Recipe { get; set; }

    [Parameter]
    public EventCallback DeleteRecipe { get; set; }

    private void OnDeleteRecipe()
    {
        DeleteRecipe.InvokeAsync();
    }
}
