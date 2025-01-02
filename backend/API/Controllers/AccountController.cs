using Business.DTOs;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userDTO)
        {
            if (await userManager.FindByEmailAsync(userDTO.Email) != null)
            {
                return BadRequest("User exists!");
            }

            var user = new AppUser
            {
                DisplayName = userDTO.DisplayName,
                Email = userDTO.Email,
                UserName = userDTO.Username
            };

            var result = await userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded) return BadRequest("Failed to register!");

            var roleResult = await userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded) return BadRequest("Failed to assign role!");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized("Invalid email or password");
            var result = await signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
            if (!result.Succeeded) return Unauthorized("Invalid email or password");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [Authorize]
        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            return Ok(new { valid = true });
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized("Not logged in");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            var profile = new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Username = user.UserName
            };

            return Ok(profile);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UserUpdateDTO userDTO)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized("Not logged in");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            if (!string.IsNullOrEmpty(userDTO.DisplayName)) user.DisplayName = userDTO.DisplayName;
            if (!string.IsNullOrEmpty(userDTO.Email)) user.Email = userDTO.Email;
            if (!string.IsNullOrEmpty(userDTO.Username)) user.UserName = userDTO.Username;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest("Failed to update profile");
            return Ok();
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(PasswordDTO passwordDTO)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized("Not logged in");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            var result = await userManager.ChangePasswordAsync(user, passwordDTO.CurrentPassword, passwordDTO.NewPassword);
            if (!result.Succeeded) return BadRequest("Failed to update password");
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized("Not logged in");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest("Failed to delete user");
            return Ok();
        }
    }
}
