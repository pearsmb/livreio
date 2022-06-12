using System.ComponentModel.DataAnnotations;
using livreio.API;
using Microsoft.EntityFrameworkCore;

namespace livreio.Domain;

public class Book
{
    public int BookId { get; set; }
    public string VolumeId { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public double? AverageRating { get; set; }
    public string Description { get; set; }
    public string ImageLink { get; set; }
    
    
    public List<AppUser_FavouriteBooks> FavouriteBooks { get; set; }

    
}