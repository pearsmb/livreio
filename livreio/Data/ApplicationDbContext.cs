using livreio.API;
using livreio.Domain;
using livreio.Features.Post;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace livreio.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<AppUser_FavouriteBooks>()
            .HasOne(b => b.Book)
            .WithMany(fb => fb.FavouriteBooks)
            .HasForeignKey(bi => bi.BookId);

        modelBuilder.Entity<AppUser_FavouriteBooks>()
            .HasOne(au => au.AppUser)
            .WithMany(fb => fb.FavouriteBooks)
            .HasForeignKey(aui => aui.AppUserId);
        
        
        base.OnModelCreating(modelBuilder);
        
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<AppUser_FavouriteBooks> FavouriteBooks { get; set; }
}