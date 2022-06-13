using livreio.API;
using livreio.Features.User;
using Microsoft.AspNetCore.Mvc;

namespace livreio.Features;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _userService;

    public UserController(ILogger<UserController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("{userName}" ,Name = "GetUserByUserName")]
    public async Task<AppUser> TestEndpoint(string userName)
    {
        return await _userService.GetUserByUserName(userName);
    }


}

