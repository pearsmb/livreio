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
    
    public async Task<AppUser?> GetUserByUserName(string userName)
    {
        return await _dbContext.Users
            .Include(x => x.Posts)
            .FirstOrDefaultAsync(x =>
            x.UserName == userName);
    }
}