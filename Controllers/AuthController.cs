using Microsoft.AspNetCore.Mvc;

namespace BillsPaymentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.Username == "admin" && request.Password == "admin123")
                return Ok(new { message = $"Login successful! Welcome, {request.Username}!" });

            return Unauthorized(new { message = "Invalid username or password." });
        }
    }
}