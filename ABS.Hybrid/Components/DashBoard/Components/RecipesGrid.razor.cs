using Microsoft.AspNetCore.Components;
using Shared.DataTransferObjects;

namespace ABS.Hybrid.Components.DashBoard.Components;
public partial class RecipesGrid
{
    [Parameter]
    public List<RecipeDto>? Recipes { get; set; }

    [Parameter]
    public EventCallback<RecipeDto> OnRecipeSelected { get; set; }    

    private void HandleRecipeChanged(RecipeDto recipe)
    {
        // Invoke the callback to notify the parent component about the selected recipe
        OnRecipeSelected.InvokeAsync(recipe);
    }
}
