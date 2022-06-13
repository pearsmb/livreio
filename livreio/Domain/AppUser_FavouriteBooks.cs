using livreio.API;
using System.Text.Json.Serialization;

namespace livreio.Domain;

public class AppUser_FavouriteBooks
{

    public int Id { get; set; }
    
    public int BookId { get; set; }
    
    public Book Book { get; set; }

    public string AppUserId { get; set; }
    
    [JsonIgnore]
    public AppUser AppUser { get; set; }
    
}