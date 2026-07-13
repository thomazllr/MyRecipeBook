using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Mappings;

internal static class MapsterConfiguration
{
    internal static void Configure()
    {
        TypeAdapterConfig<RequestRecipeJson, Recipe>
            .NewConfig()
            .Map(dest => dest.Ingredients, source => source.Ingredients.Select(ingredient => new RecipeIngredient
            {
                Item = ingredient
            }))
            .Map(dest => dest.DisheTypes, source => source.DishTypes.Select(dishType => new RecipeDisheType
            {
               Type = (Domain.Enums.DishType) dishType
            }));
    }
}
