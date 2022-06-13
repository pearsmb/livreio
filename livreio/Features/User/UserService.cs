using AutoMapper;
using livreio.API;
using livreio.Data;
using livreio.Services;
using Microsoft.EntityFrameworkCore;

namespace livreio.Features.User;

public class UserService : ServiceBase
{
    public UserService(IConfiguration configuration, IMapper mapper, ApplicationDbContext dbContext, IUserAccessor userAccessor) : base(configuration, mapper, dbContext, userAccessor)
    {
    }
    
    public async Task<GetUserDto> GetUserByUserName(string userName)
    {
        var user = await _dbContext.Users
            .Include(x => x.Posts)
            .Include(x => x.FavouriteBooks)
                .ThenInclude(fb => fb.Book)
            .FirstOrDefaultAsync(x =>
            x.UserName == userName);

        var userToReturn = _mapper.Map<GetUserDto>(user);

        return userToReturn;

    }
    
    
}