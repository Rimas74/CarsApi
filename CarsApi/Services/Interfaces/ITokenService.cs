using Microsoft.AspNetCore.Identity;

namespace CarsApi.Services.Interfaces
    {
    public interface ITokenService
        {
        string CreateToken(IdentityUser user, IList<string> roles);
        }
    }
