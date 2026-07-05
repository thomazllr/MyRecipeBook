using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Domain.Entities;

public class RecipeDisheType : BaseEntity
{
    public DishType Type;
    public Guid RecipeId { get; private set; }
}