using Business.DTOs;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : ControllerBase
    {

        //I've implemented AccountService but I could not use SignInManager in the service.

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

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized("Invalied email or password");
            var result = await signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
            if (!result.Succeeded) return Unauthorized("Invalied email or password");

            var token = "";
            return Ok(new { token });
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
