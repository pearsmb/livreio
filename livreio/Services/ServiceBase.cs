using AutoMapper;
using livreio.API;
using livreio.Data;
using livreio.Features.User;
using Microsoft.EntityFrameworkCore;

namespace livreio.Services;

public abstract class ServiceBase : IService
{
    internal IConfiguration _configuration;
    internal IMapper _mapper;
    internal ApplicationDbContext _dbContext;
    internal IUserAccessor _userAccessor;

    public ServiceBase(IConfiguration configuration, IMapper mapper, ApplicationDbContext dbContext, IUserAccessor userAccessor)
    {
        _configuration = configuration;
        _mapper = mapper;
        _dbContext = dbContext;
        _userAccessor = userAccessor;
    }
    
    public async Task<AppUser?> GetSignedInUserAsync()
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x =>
            x.UserName == _userAccessor.GetUserName());
    }
}