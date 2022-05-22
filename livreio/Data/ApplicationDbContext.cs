using bookify.API;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace livreio.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    
}