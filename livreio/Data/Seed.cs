using bookify.API;
using Microsoft.AspNetCore.Identity;

namespace livreio.Data;

public class Seed
{
    public static async Task SeedData(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
    {

        if (!userManager.Users.Any())
        {
            var users = new List<AppUser>
            {
                new AppUser {DisplayName = "Ben", UserName = "Ben", Email = "ben@gmail.com"},
                new AppUser {DisplayName = "Tom", UserName = "Tom", Email = "tom@gmail.com"},
                new AppUser {DisplayName = "Sam", UserName = "Sam", Email = "sam@gmail.com"},
                new AppUser {DisplayName = "Pete", UserName = "Pete", Email = "pete@gmail.com"},
                new AppUser {DisplayName = "Paul", UserName = "Paul", Email = "paul@gmail.com"}
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$word1");
            }
            
        }
        await dbContext.SaveChangesAsync();
    }
}

