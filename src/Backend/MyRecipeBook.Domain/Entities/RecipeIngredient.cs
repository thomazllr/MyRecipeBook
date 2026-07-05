namespace MyRecipeBook.Domain.Entities;

public class RecipeIngredient : BaseEntity
{
    public string Item { get; set; } = string.Empty;
    public Guid RecipeId { get; private set; }
}
