
using System.Security.Claims;
using livreio.API;
using livreio.Domain;
using livreio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace livreio.Features.User;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _configuration = configuration;
        
    }

    /// <summary>
    /// POST Login method - returns user object
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns></returns>
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
            return CreateUserObject(user);

        }

        // otherwise, return unauthorized
        return Unauthorized();
    }


    /// <summary>
    /// POST - Registers a new user
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {

        // checks for existing user email
        if (await _userManager.Users.AnyAsync(user => user.Email == registerDto.Email))
        {
            return BadRequest("Email already exists.");
        }
        
        // checks for existing username
        if (await _userManager.Users.AnyAsync(user => user.UserName == registerDto.Username))
        {
            return BadRequest("Email already exists.");
        }
        
        // if all is well, create a new user

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Username,
            FavouriteBooks = new List<AppUser_FavouriteBooks>()
        };

        // this in itself handles password validation using Identity, if a password is bad result will be
        // unsuccessful, and the method will return BadRequest instead
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            return CreateUserObject(user);
        }

        return BadRequest("Problem registering user");
        
    }

    /// <summary>
    /// Gets the currently logged in user's details
    /// </summary>
    /// <returns>UserDto</returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        
        // as this method requires a bearer token, 
        // a signed-in user will always be sending a bearer token with the request
        // so the method below checks the token and gets the email from the token 
        // and then finds the signed in user
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

        return CreateUserObject(user);

    }


    /// <summary>
    /// internal helper method used to create a userDto from an AppUser
    /// </summary>
    /// <param name="appUser"></param>
    /// <returns></returns>
    private UserDto CreateUserObject(AppUser appUser)
    {
        return new UserDto
        {
            DisplayName = appUser.DisplayName,
            Token = _tokenService.CreateToken(appUser),
            Username = appUser.UserName
            
        };
    }
    
    
    
}