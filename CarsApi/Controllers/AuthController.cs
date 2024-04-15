using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarsApi.DTOs;
using Microsoft.AspNetCore.Identity;
using CarsApi.Repositories.Interfaces;
using CarsApi.Services.Interfaces;

namespace CarsApi.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
        {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenService tokenService;

        public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenService)
            {
            this.userManager = userManager;
            this.tokenService = tokenService;
            }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
            {
            var identityUser = new IdentityUser
                {
                UserName = registerRequestDto.UserEmail,
                Email = registerRequestDto.UserEmail
                };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
                {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Length > 0)
                    {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                        {
                        return Ok("User was registered! Please login.");
                        }
                    }
                return Ok("User was registered without roles.");
                }
            return BadRequest(identityResult.Errors);
            }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
            {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserEmail);
            if (user != null)
                {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                    {
                    //Ger Roled for this user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                        {
                        // Create token logic here
                        var jwtToken = tokenService.CreateToken(user, roles);
                        var response = new LoginResponseDto
                            {
                            JwtToken = jwtToken
                            };
                        return Ok(jwtToken);

                        }

                    }
                }
            return BadRequest("Username was not found or password is incorrect.");
            }
        }
    }
