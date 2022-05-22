namespace livreio.Features.Books;

public class BookDto
{
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public double? AverageRating { get; set; }
    public string Description { get; set; }
    
}