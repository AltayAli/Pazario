using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pazario.Common.Application.Abstractions;
using Pazario.Common.Domain.Abstractions;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Infrastructure.Outbox;

namespace Pazario.Products.Infrastructure.Data
{
    public class ProductsDbContext : DbContext, IUnitOfWork
    {
        private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        private readonly IDateTimeProvider _dateTimeProvider;
        public ProductsDbContext(IDateTimeProvider dateTimeProvider, DbContextOptions options) : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddDomainEventAsOutboxMessage();

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private void AddDomainEventAsOutboxMessage()
        {
            var outboxMessages = ChangeTracker.Entries<BaseEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var events = entity.GetDomainEvents().ToList();
                    entity.ClearDomainEvents();

                    return events;
                })
                .Select(domainEvent => new OutboxMessage(
                    JsonConvert.SerializeObject(domainEvent, _jsonSerializerSettings),
                    domainEvent.GetType().Name,
                    _dateTimeProvider.UtcNow))
                .ToList();

            AddRange(outboxMessages);
        }
    }
}
