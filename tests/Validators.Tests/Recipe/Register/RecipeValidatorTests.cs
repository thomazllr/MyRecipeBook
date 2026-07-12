using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exception;
using Shouldly;

namespace Validators.Tests.Recipe.Register;

public class RecipeValidatorTests
{
    [Fact]
    public void Success()
    {
        var request = RequestRecipeJsonBuilder.Build();

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("              ")]
    [InlineData(null)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "Intentional because it is a unit test")]
    public void Validate_ShouldHaveError_WhenTitleIsEmpty(string title)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Title = title;

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_TITLE_REQUIRED));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenTitleIsTooLong()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Title = new string('a', 251);

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_TITLE_MAX_LENGHT));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenCookTimeIsInvalid()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.CookTime = (CookTime)999;

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_COOKIE_TIME_INVALID));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenDishTypesIsEmpty()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.DishTypes = [];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_AT_LEAST_ONE_DISH_TYPE));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenDishTypeIsInvalid()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.DishTypes = [(DishType)999];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_DISH_TYPE_INVALID));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenIngredientsIsEmpty()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredients = [];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_AT_LEAST_ONE_INGREDIENT));
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("              ")]
    [InlineData(null)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "Intentional because it is a unit test")]
    public void Validate_ShouldHaveError_WhenIngredientIsEmpty(string ingredient)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredients = [ingredient];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INGREDIENT_EMPTY));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenIngredientIsTooLong()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredients = [new string('a', 251)];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INGREDIENT_MAX_LENGHT));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenInstructionsIsEmpty()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions = [];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_AT_LEAST_ONE_INSTRUCTION));
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_ShouldHaveError_WhenInstructionOrderIsInvalid(int order)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions =
        [
            new RequestRecipeInstructionJson
            {
                Order = order,
                Description = "Instruction description"
            }
        ];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INSTRUCTION_ORDER_INVALID));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenInstructionOrderIsDuplicated()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions =
        [
            new RequestRecipeInstructionJson
            {
                Order = 1,
                Description = "Step 1"
            },
            new RequestRecipeInstructionJson
            {
                Order = 1,
                Description = "Step 2"
            }
        ];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INSTRUCTION_ORDER_DUPLICATED));
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("              ")]
    [InlineData(null)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "Intentional because it is a unit test")]
    public void Validate_ShouldHaveError_WhenInstructionDescriptionIsEmpty(string description)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions =
        [
            new RequestRecipeInstructionJson
            {
                Order = 1,
                Description = description
            }
        ];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INSTRUCTION_DESCRIPTION_REQUIRED));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenInstructionDescriptionIsTooLong()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions =
        [
            new RequestRecipeInstructionJson
            {
                Order = 1,
                Description = new string('a', 2001)
            }
        ];

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INSTRUCTION_DESCRIPTION_MAX_LENGHT));
        });
    }
}
