using Order.Application.Contracts.UnitOfWork;

namespace Order.Persistence.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{

    public Task<int> SaveChangeAsync(CancellationToken cancellationToken = default) => context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) => await context.Database.BeginTransactionAsync(cancellationToken);

    public Task CommitTransactionAsync(CancellationToken cancellationToken) => context.Database.CommitTransactionAsync(cancellationToken);
}