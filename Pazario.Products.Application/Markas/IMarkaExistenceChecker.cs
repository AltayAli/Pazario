namespace Pazario.Products.Application.Markas
{
    /// <summary>
    /// Application-level abstraction for checking marka existence.
    /// Uses cache first, then database — handlers depend on this instead of repository + cache directly.
    /// </summary>
    public interface IMarkaExistenceChecker
    {
        Task<bool> ExistsAsync(Guid markaId, CancellationToken cancellationToken = default);
    }
}
