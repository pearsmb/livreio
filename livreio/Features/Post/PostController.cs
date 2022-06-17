using livreio.API;
using livreio.Features.Books;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace livreio.Features.Post;


[ApiController]
[Route("api/")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly PostService _postService;

    public PostController(ILogger<PostController> logger, PostService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    [HttpPost("User/{username}/posts")]
    public async Task<ActionResult<PostDto>> SubmitPost(string userName, PostDto post)
    {
        return await _postService.SubmitPost(post);
    }

    [HttpGet("User/{username}/posts")]
    public async Task<ActionResult<List<PostDto>>> GetPosts(string userName)
    {
        return await _postService.GetPostsByUserName(userName);
    }
    
    [HttpGet("posts")]
    public async Task<ActionResult<List<PostDto>>> GetPosts()
    {
        return await _postService.GetRecentPosts();
    }
    
    [HttpGet("posts-timeline")]
    public async Task<ActionResult<List<PostDto>>> GetPostsTimeline()
    {
        return await _postService.GetTimeLine();
    }
    
    

    [HttpGet("posts/{postId}")]
    public async Task<ActionResult<PostDto>> GetPostByPostId(int postId)
    {
        return await _postService.GetPostById(postId);
    }
    
    [HttpDelete("posts/{postId}")]
    public async Task<ActionResult<List<PostDto>>> DeletePostByPostId(int postId)
    {
        return await _postService.DeletePostById(postId);
    }
    
    
    

}