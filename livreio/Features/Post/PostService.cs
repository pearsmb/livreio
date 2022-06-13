using AutoMapper;
using livreio.Data;
using livreio.Features.User;
using livreio.Services;
using Microsoft.EntityFrameworkCore;

namespace livreio.Features.Post;

public class PostService : ServiceBase
{
    public PostService(IConfiguration configuration, IMapper mapper, ApplicationDbContext dbContext, IUserAccessor userAccessor) : base(configuration, mapper, dbContext, userAccessor)
    {
    }


    public async Task<PostDto> SubmitPost(PostDto post)
    {

        var user = await GetSignedInUserAsync();

        var postToSave = _mapper.Map<Post>(post);
        
        user.Posts.Add(postToSave);
        _dbContext.Update(user);
        
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<PostDto>(postToSave);

    }

    public async Task<List<PostDto>> GetPosts()
    {
        var user = await GetSignedInUserAsync();

        return _mapper.Map<List<PostDto>>(_dbContext.Posts
            .Where(x => x.AppUser == user).ToList());
        
    }

    public async Task<List<PostDto>> GetPostsByUserName(string userName)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
            x.UserName == userName);
        
        return _mapper.Map<List<PostDto>>(_dbContext.Posts
            .Where(x => x.AppUser == user).ToList());
        
    }
    
}