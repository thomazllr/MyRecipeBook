using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infrastructure.DataAccess;

internal class UnityOfWork : IUnityOfWork
{
    private readonly MyRecipeBookDbContext _dbContext;

    public UnityOfWork(MyRecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
