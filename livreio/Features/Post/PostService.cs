using AutoMapper;
using livreio.Data;
using livreio.Features.Books;
using livreio.Features.User;
using livreio.Services;
using Microsoft.EntityFrameworkCore;

namespace livreio.Features.Post;

public class PostService : ServiceBase
{
    private readonly BookService _bookService;

    public PostService(IConfiguration configuration, IMapper mapper, ApplicationDbContext dbContext, IUserAccessor userAccessor, BookService bookService) : base(configuration, mapper, dbContext, userAccessor)
    {
        _bookService = bookService;
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
    
    public async Task<List<PostDto>> GetPostsByUserName(string userName)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
            x.UserName == userName);
        
        return _mapper.Map<List<PostDto>>(_dbContext.Posts
            .Include(x=> x.AssociatedBook)
            .Where(x => x.AppUser == user).ToList());
        
    }
    
    public async Task<List<PostDto>> GetRecentPosts()
    {
        return _mapper.Map<List<PostDto>>(await _dbContext.Posts
            .Include(x=> x.AssociatedBook)
            .Take(5).ToListAsync());
    }
    
    public async Task<PostDto> GetPostById(int Id)
    {
        
        return _mapper.Map<PostDto>(await _dbContext.Posts.FirstOrDefaultAsync(x =>
            x.Id == Id));
        
    }

    public async Task<List<PostDto>> DeletePostById(int id)
    {

        var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
            x.UserName == _userAccessor.GetUserName());
        
        var post = await _dbContext.Posts.FindAsync(id);

        if (post.AppUserId != user.Id)
        {
            return await GetRecentPosts();
        }
        
        _dbContext.Remove(post);

        await _dbContext.SaveChangesAsync();
        
        return await GetRecentPosts();

    }

    
}