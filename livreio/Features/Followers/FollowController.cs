using livreio.Domain;
using Microsoft.AspNetCore.Mvc;

namespace livreio.Features.Followers;

[ApiController]
[Route("api/[controller]")]
public class FollowController : ControllerBase
{
    private readonly ILogger<FollowController> _logger;
    private readonly FollowersService _followService;

    public FollowController(ILogger<FollowController> logger, FollowersService followService)
    {
        _logger = logger;
        _followService = followService;
    }
    
    [HttpPost("{targetUserName}")]
    public async Task<ActionResult<UserFollowing>> Follow(string targetUserName)
    {
        return  Ok(await _followService.ToggleFollow(targetUserName));
    }
        
    
}