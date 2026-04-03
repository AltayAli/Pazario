namespace Pazario.Products.Application.Models
{
    public interface IModelExistenceChecker
    {
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
    }
}
