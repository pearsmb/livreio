using livreio.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace livreio.Features.Post;


[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly PostService _postService;


    public PostController(ILogger<PostController> logger, UserManager<AppUser> userManager, PostService postService)
    {
        _logger = logger;
        _userManager = userManager;
        _postService = postService;
    }


    [HttpPost("SubmitPost")]
    public async Task<ActionResult<PostDto>> SubmitPost(PostDto post)
    {

        return await _postService.SubmitPost(post);

    }

    [HttpGet("GetPosts")]
    public async Task<ActionResult<List<PostDto>>> GetPosts()
    {
        return await _postService.GetPosts();
    }
    
}