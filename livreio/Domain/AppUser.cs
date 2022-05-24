using Microsoft.AspNetCore.Identity;

namespace bookify.API;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
    
}