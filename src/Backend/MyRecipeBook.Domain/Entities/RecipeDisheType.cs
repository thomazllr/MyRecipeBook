using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Domain.Entities;

public class RecipeDisheType : BaseEntity
{
    public DishType Type { get; set; }
    public Guid RecipeId { get; private set; }
}