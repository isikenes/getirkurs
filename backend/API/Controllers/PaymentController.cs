using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentMethodService paymentMethodService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddPaymentMethod(PaymentMethodDTO paymentMethod)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            await paymentMethodService.AddPaymentMethod(paymentMethod, userId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentMethod()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var paymentMethod = await paymentMethodService.GetPaymentMethodByUserId(userId);
            return Ok(paymentMethod);
        }
    }
}
