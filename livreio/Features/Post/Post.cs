using System.Text.Json.Serialization;
using livreio.API;
using livreio.Domain;

namespace livreio.Features.Post;

public class Post
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public Book? AssociatedBook { get; set; }
    public string Message { get; set; }

    public string AppUserId { get; set; }
    [JsonIgnore]
    public AppUser AppUser { get; set; }
    
}