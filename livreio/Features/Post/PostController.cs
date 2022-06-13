using livreio.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace livreio.Features.Post;


[ApiController]
[Route("api/User/")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly PostService _postService;

    public PostController(ILogger<PostController> logger, PostService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    [HttpPost("{username}/posts")]
    public async Task<ActionResult<PostDto>> SubmitPost(string userName, PostDto post)
    {
        return await _postService.SubmitPost(post);
    }

    [HttpGet("{username}/posts")]
    public async Task<ActionResult<List<PostDto>>> GetPosts(string userName)
    {
        return await _postService.GetPostsByUserName(userName);
    }
    
}