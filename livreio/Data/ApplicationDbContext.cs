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

        modelBuilder.Entity<UserFollowing>(b =>
        {
            b.HasKey(k => new { k.ObserverId, k.TargetId });
            b.HasOne(o => o.Observer)
                .WithMany(f => f.Followings)
                .HasForeignKey(o => o.ObserverId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(o => o.Target)
                .WithMany(f => f.Followers)
                .HasForeignKey(o => o.TargetId)
                .OnDelete(DeleteBehavior.Cascade);

        });
        
        
        base.OnModelCreating(modelBuilder);
        
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<AppUser_FavouriteBooks> FavouriteBooks { get; set; }
    
    public DbSet<UserFollowing> UserFollowings { get; set; }
    
}