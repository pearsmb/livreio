using livreio.API;

namespace livreio.Services;

public interface IService
{
    Task<AppUser?> GetSignedInUserAsync();
}