namespace MyRecipeBook.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public bool IsActive { get; set; } = true;
}
