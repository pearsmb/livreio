namespace livreio.Features.Books;

public class BookDto
{
    public string VolumeId { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public double? AverageRating { get; set; }
    public string Description { get; set; }
    public string ImageLink { get; set; }
}