using livreio.Domain;
using livreio.Features.Books;

namespace livreio.Features.Post;

public class PostDto
{
    
    public string AppUserId { get; set; }
    public string Message { get; set; }
    public BookDto AssociatedBook { get; set; }
    
}