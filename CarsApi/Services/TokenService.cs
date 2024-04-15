using CarsApi.Repositories;
using CarsApi.Repositories.Interfaces;
using CarsApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CarsApi.Services
    {
    public class TokenService : ITokenService
        {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
            {
            _tokenRepository = tokenRepository;
            }

        public string CreateToken(IdentityUser user, IList<string> roles)
            {
            return _tokenRepository.CreateJWTToken(user, roles);
            }
        }
    }
