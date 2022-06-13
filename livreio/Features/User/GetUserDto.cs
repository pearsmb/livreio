using livreio.Domain;

namespace livreio.Features.User;
public class GetUserDto
{
    public string DisplayName { get; set; }
    public string Username { get; set; }
    public List<AppUser_FavouriteBooks> FavouriteBooks { get; set; } = new List<AppUser_FavouriteBooks>();
    public List<Post.Post> Posts { get; set; }= new List<Post.Post>();
}