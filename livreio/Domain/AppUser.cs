using livreio.Domain;
using Microsoft.AspNetCore.Identity;

namespace livreio.API;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }

    public List<AppUser_FavouriteBooks> FavouriteBooks { get; set; }

}