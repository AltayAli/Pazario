# Pazario — Products Module

## Architecture

Clean Architecture with CQRS (MediatR) and Domain-Driven Design.

```
Pazario.Products.Domain          → Entities, value objects, domain events, repository interfaces
Pazario.Products.Application     → Command/Query handlers, pipeline behaviours, abstractions
Pazario.Products.Infrastructure  → EF Core DbContext, Redis cache, repositories, outbox
Pazario.Products.Presentation    → API controllers (minimal surface so far)
```

## Key Patterns

**CQRS via MediatR**
- Commands implement `ICommand` / `ICommand<TResponse>` (both extend `IBaseCommand`)
- Queries implement `IQuery<TResponse>`; cacheable queries also implement `ICacheQuery<TResponse>`
- Handlers implement `ICommandHandler<TCommand>` / `IQueryHandler<TQuery, TResponse>`

**Pipeline Behaviours** (registered in this order in `Infrastructure/DependencyInjection.cs`)
1. `ValidationBehavior` — FluentValidation, runs on every request
2. `LoggingBehavior` — Serilog structured logging with `Result` awareness
3. `QueryCachingBehavior` — Redis cache for `ICacheQuery` requests

**Domain**
- All entities extend `BaseEntity` (Guid Id, audit fields, domain event list)
- Repository interface: `IBaseRepository<TEntity>` with `FilteringOptions<TEntity>` for predicate-based filtering
- `IUnitOfWork` is implemented by `ProductsDbContext`
- Domain events are harvested on `SaveChangesAsync` and persisted as `OutboxMessage` rows (Newtonsoft JSON, `TypeNameHandling.All`)

**Result Pattern**
- `Result` / `Result<T>` used throughout; handlers return `Result`
- `LoggingBehavior` logs warnings on failure, info on success

**Infrastructure**
- SQL Server via EF Core; configurations loaded from assembly (`ApplyConfigurationsFromAssembly`)
- Redis for distributed caching (`IStackExchangeRedisCache` → `ICacheService`)
- Repositories auto-registered via Scrutor: any class whose name ends with `Repository` in the Infrastructure assembly is scanned and registered as its implemented interface (scoped)

## Tech Stack

- .NET 10
- MediatR 13, FluentValidation 12
- EF Core (SQL Server)
- Redis (StackExchange)
- Serilog, Newtonsoft.Json

## Build & Run

```bash
dotnet build Pazario.slnx
dotnet run --project Pazario.Products.Presentation
```

Connection strings required in `appsettings`:
- `DefaultConnection` — SQL Server
- `CacheConnection` — Redis

## Conventions

- One handler file per command/query, co-located with the command/query record
- Validator classes go in the same folder as their command
- Repository names must end with `Repository` to be auto-registered
- Folder structure mirrors the domain aggregate (Categories, Markas, Models, Products, …)
- `FilteringOptions<TEntity>` carries a list of `Expression<Func<TEntity, bool>>` predicates
