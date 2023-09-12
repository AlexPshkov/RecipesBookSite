using Domain.Models.secondary;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly DataBaseContext _dbContext;

    public TagRepository( DataBaseContext dbContext )
    {
        _dbContext = dbContext;
    }

    public Task<TagEntity?> GetById( int id )
    {
        return _dbContext.Tags.SingleOrDefaultAsync( tag => id.Equals( tag.TagId ) );
    }

    public Task<TagEntity?> GetByName( string name )
    {
        return _dbContext.Tags.SingleOrDefaultAsync( tag => name == tag.Name );
    }

    public async void Create( TagEntity entity )
    {
        await _dbContext.Tags.AddAsync( entity );
    }

    public void Update( TagEntity entity )
    {
        _dbContext.Tags.Update( entity );
    }

    public void Delete( TagEntity entity )
    {
        _dbContext.Tags.Remove( entity );
    }

    public async Task<List<TagEntity>> GetBestTags( int amount )
    {
        IQueryable<TagEntity> actions = _dbContext.Tags
            .Include( x => x.Recipes )
            .OrderByDescending( x => x.Recipes.Count )
            .Take( amount );

        return await actions.ToListAsync();
    }
}