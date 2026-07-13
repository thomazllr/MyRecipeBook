using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Domain.Entities;

public class Recipe : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public ICollection<RecipeIngredient> Ingredients { get; set; } = [];
    public ICollection<RecipeInstruction> Instructions { get; set; } = [];
    public ICollection<RecipeDisheType> DisheTypes{ get; set; } = [];
    public CookTime CookTime { get; set; }
    public Guid UserId { get; set; }

}
