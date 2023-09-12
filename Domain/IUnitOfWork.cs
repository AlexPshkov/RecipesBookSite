namespace Domain;

public interface IUnitOfWork
{
    public Task<bool> SaveChanges( CancellationToken cancellationToken = default );
}