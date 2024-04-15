using Microsoft.AspNetCore.Identity;

namespace CarsApi.Repositories.Interfaces
    {
    public interface ITokenRepository
        {
        string CreateJWTToken(IdentityUser user, IList<string> roles);
        }
    }
