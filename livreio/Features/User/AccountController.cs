
using bookify.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace livreio.Features.User;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        
        // tries to find an existing user by email address
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        // returns if user doesnt exist
        if (user == null) return Unauthorized();

        // compares password of loginDto and the userpassword from the db
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        // if successful, return a new UserDto
        if (result.Succeeded)
        {
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Image = null,
                Token = "This will be a token",
                Username = user.UserName
            };
            
        }

        // otherwise, return unauthorized
        return Unauthorized();
    }
    
}