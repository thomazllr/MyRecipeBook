using FluentMigrator;
using System.Data;

namespace MyRecipeBook.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_RECIPES, "Creating Recipes Table")]
public class Version0000002 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Recipes")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("Title").AsString(250).NotNullable()
            .WithColumn("CookTime").AsInt32().NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable();

        Create.ForeignKey("FK_Recipes_Users_UserId")
            .FromTable("Recipes").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");

        Create.Table("RecipeIngredients")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("Item").AsString(250).NotNullable()
            .WithColumn("RecipeId").AsGuid().NotNullable();

        Create.ForeignKey("FK_RecipeIngredients_Recipes_RecipeId")
            .FromTable("RecipeIngredients").ForeignColumn("RecipeId")
            .ToTable("Recipes").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        Create.Table("RecipeInstructions")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("Order").AsInt32().NotNullable()
            .WithColumn("Description").AsString(2000).NotNullable()
            .WithColumn("RecipeId").AsGuid().NotNullable();

        Create.ForeignKey("FK_RecipeInstructions_Recipes_RecipeId")
            .FromTable("RecipeInstructions").ForeignColumn("RecipeId")
            .ToTable("Recipes").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        Create.Table("RecipeDisheTypes")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("Type").AsInt32().NotNullable()
            .WithColumn("RecipeId").AsGuid().NotNullable();

        Create.ForeignKey("FK_RecipeDisheTypes_Recipes_RecipeId")
            .FromTable("RecipeDisheTypes").ForeignColumn("RecipeId")
            .ToTable("Recipes").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);
    }


}
