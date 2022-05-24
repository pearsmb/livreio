using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using bookify.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace livreio.Services;

public class TokenService
{

    /// <summary>
    /// creates JWT token to authenticate API requests
    /// </summary>
    /// <param name="appuser"></param>
    /// <returns></returns>
    public string CreateToken(AppUser appuser)
    {

        // claims to be sent with every single token JWT token request
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, appuser.UserName),
            new Claim(ClaimTypes.NameIdentifier, appuser.Id),
            new Claim(ClaimTypes.Email, appuser.Email)
        };

        
        // in dev this is fine, but need to autogenerate a long random key here for prod
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(("super secret key")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(31),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token); 



    }
    
}