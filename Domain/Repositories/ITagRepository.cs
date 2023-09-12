using Domain.Models.secondary;

namespace Domain.Repositories;

public interface ITagRepository : IEntityRepository<TagEntity>
{
    public Task<TagEntity?> GetById( int id );
    public Task<TagEntity?> GetByName( string name );
    public Task<List<TagEntity>> GetBestTags( int amount );
}