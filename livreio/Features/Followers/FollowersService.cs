using AutoMapper;
using AutoMapper.QueryableExtensions;
using livreio.Data;
using livreio.Domain;
using livreio.Features.User;
using livreio.Services;
using Microsoft.EntityFrameworkCore;

namespace livreio.Features.Followers;

public class FollowersService : ServiceBase
{
    public FollowersService(IConfiguration configuration, IMapper mapper, ApplicationDbContext dbContext, IUserAccessor userAccessor) : base(configuration, mapper, dbContext, userAccessor)
    {
    }

    public async Task<UserFollowing> ToggleFollow(string targetUsername)
    {
        var observer = await _dbContext.Users.FirstOrDefaultAsync(x => 
            x.UserName == _userAccessor.GetUserName());
        
        var target = await _dbContext.Users.FirstOrDefaultAsync(x => 
            x.UserName == targetUsername);
        
        //if(target == null) return false;

        var following = await _dbContext.UserFollowings.FindAsync(observer.Id, target.Id);

        if (following == null)
        {
            following = new UserFollowing
            {
                TargetId = target.Id,
                ObserverId = observer.Id,
                Observer = observer,
                Target = target
            };

            _dbContext.UserFollowings.Add(following);
        }
        else
        {
            _dbContext.UserFollowings.Remove(following);
        }

        await _dbContext.SaveChangesAsync();

        return await _dbContext.UserFollowings.FirstOrDefaultAsync();

    }
    
    public async Task<List<FollowDto>> GetFollowers(string userName)
    {
        return _mapper.Map<List<FollowDto>>(await _dbContext.UserFollowings
            .Where(x => x.Target.UserName == userName)
            .Select(o => o.Observer).ToListAsync());
    }
    
    
    public async Task<List<FollowDto>> GetFollowing(string userName)
    {

        return _mapper.Map<List<FollowDto>>(await _dbContext.UserFollowings
            .Where(x => x.Observer.UserName == userName)
            .Select(o => o.Target).ToListAsync());
        
    }

    public async Task<List<string>> GetFollowingIds(string userName)
    {

        return _mapper.Map<List<string>>(await _dbContext.UserFollowings
            .Where(x => x.Observer.UserName == userName)
            .Select(o => o.Target.Id).ToListAsync());
        
    }
}