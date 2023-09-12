using Domain.Models;

namespace Domain.Repositories;

public interface IEntityRepository<in T> where T : AbstractEntity
{
    public void Create( T entity );
    public void Update( T entity );
    public void Delete( T entity );
}