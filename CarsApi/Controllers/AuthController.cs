using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarsApi.DTOs;
using Microsoft.AspNetCore.Identity;
using CarsApi.Repositories.Interfaces;
using CarsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CarsApi.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
        {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenService tokenService;
        //private readonly IUserClaimsPrincipalFactory<IdentityUser> claimsPrincipalFactory;

        public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenService) //, IUserClaimsPrincipalFactory<IdentityUser> claimsPrincipalFactory
            {
            this.userManager = userManager;
            this.tokenService = tokenService;
            //this.claimsPrincipalFactory = claimsPrincipalFactory;
            }

        [HttpPost("Register")]
        //[Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
            {
            var identityUser = new IdentityUser
                {
                UserName = registerRequestDto.UserEmail,
                Email = registerRequestDto.UserEmail
                };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (!identityResult.Succeeded)
                {
                return BadRequest(identityResult.Errors);
                }

            return Ok("User was registered successfully.");
            }



        [HttpPost("Login")]
        //[Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
            {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserEmail);
            if (user == null)
                {
                return BadRequest("Username was not found.");
                }

            var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!checkPasswordResult)
                {
                return BadRequest("Incorrect password.");
                }


            var roles = await userManager.GetRolesAsync(user);
            if (roles.Count == 0)
                {
                return BadRequest("No roles assigned. Contact administrator.");
                }

            var jwtToken = tokenService.CreateToken(user, roles);
            var response = new LoginResponseDto
                {
                JwtToken = jwtToken
                };

            return Ok(response);
            }


        [HttpPost("UpdateRole")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UserRoleModificationDto userRoleModificationDto)
            {
            var user = await userManager.FindByIdAsync(userRoleModificationDto.UserId);
            if (user == null)
                {
                return NotFound("User not found.");
                }

            var currentRoles = await userManager.GetRolesAsync(user);
            var rolesToAdd = userRoleModificationDto.Roles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(userRoleModificationDto.Roles).ToList();

            var addResult = await userManager.AddToRolesAsync(user, rolesToAdd);
            var removeResult = await userManager.RemoveFromRolesAsync(user, rolesToRemove);

            if (addResult.Succeeded && removeResult.Succeeded)
                {
                return Ok("Roles updated successfully.");
                }

            return BadRequest(new
                {
                AddErrors = addResult.Errors,
                RemoveErrors = removeResult.Errors
                });

            }


        [HttpDelete("DeleteUser/{userId}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteUser(string userId)
            {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                {
                return NotFound("User not found.");
                }


            if (user.Id == userManager.GetUserId(User))
                {
                return BadRequest("Users cannot delete their own accounts.");
                }

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
                {
                return Ok("User deleted successfully.");
                }

            return BadRequest(result.Errors);
            }

        [HttpPost("ForgotPassword")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
            {
            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return BadRequest("No user associated with email.");


            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return Ok(token);
            }


        [HttpPost("ResetPassword")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
            {
            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("No user associated with email.");

            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!resetPassResult.Succeeded)
                return BadRequest(resetPassResult.Errors);

            return Ok("Password has been successfully reset.");
            }
        }


    }
