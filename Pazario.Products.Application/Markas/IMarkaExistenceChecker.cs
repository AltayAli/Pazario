namespace Pazario.Products.Application.Markas
{
    public interface IMarkaExistenceChecker
    {
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
    }
}
