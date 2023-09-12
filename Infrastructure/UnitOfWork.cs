using Domain;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataBaseContext _ctx;

    public UnitOfWork( DataBaseContext ctx )
    {
        _ctx = ctx;
    }

    public async Task<bool> SaveChanges( CancellationToken cancellationToken = default )
    {
        int result = await _ctx.SaveChangesAsync( cancellationToken );
        return result > 0;
    }
}