using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Abstractions
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<IQueryable<TEntity>> SelectAsync(FilteringOptions<TEntity> listingOptions = null, CancellationToken cancellationToken = default);
        Task<TEntity?> SelectSimpleOrDefault(FilteringOptions<TEntity> listingOptions, CancellationToken cancellationToken = default);
        Task<TEntity?> Insert(TEntity model, bool saveImmediately = false, CancellationToken cancellationToken = default);
        Task<TEntity?> Update(TEntity model, bool saveImmediately = false, CancellationToken cancellationToken = default);
        Task<TEntity?> Delete(Guid id, bool isItDeepClean = false, bool saveImmediately = false, CancellationToken cancellationToken = default);
    }
}
