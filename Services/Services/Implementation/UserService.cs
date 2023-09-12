using Domain;
using Domain.Exceptions.Implementation;
using Domain.Models;
using Domain.Repositories;
using Services.Extensions.Entity;

namespace Services.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public Task<UserEntity?> GetUserById( Guid id )
    {
        return _userRepository.GetById( id );
    }

    public Task<UserEntity?> GetUserByLogin( string login )
    {
        return _userRepository.GetByLogin( login );
    }
    
    public Task<List<RecipeEntity>> GetFavorites( Guid userId, int start, int end )
    {
        return _userRepository.GetFavorites( userId, start, end );
    }
    
    public Task<List<RecipeEntity>> GetCreatedRecipes( Guid userId, int start, int end )
    {
        return _userRepository.GetCreatedRecipes( userId, start, end );
    }
    
    public async Task<UserStatisticEntity> GetUserStatistic( Guid userId )
    {
        return await _userRepository.GetUserStatistic( userId );
    }
    
    public async Task<UserEntity> Save( UserEntity userEntity )
    {
        var domainUser = await _userRepository.GetById( userEntity.UserId );
        
        if ( domainUser == null)
        { 
            _userRepository.Create( userEntity );
            await _unitOfWork.SaveChanges();
            return userEntity;
        }

        if ( domainUser.UserId != userEntity.UserId )
        {
            throw new NoPermException( "Another UserId" );
        }

        if ( domainUser.Login != userEntity.Login )
        {
            if ( await _userRepository.GetByLogin( userEntity.Login ) != null )
            {
                throw new UserAlreadyExistsException( userEntity.Login );
            }
        }
        
        _userRepository.Update( domainUser.Combine( userEntity ) );
        await _unitOfWork.SaveChanges();
        return domainUser;
    }
}