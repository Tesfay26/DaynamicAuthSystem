using DynamicAuthSystem.Application.Commands;
using DynamicAuthSystem.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicAuthSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // POST api/<AccountController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(new { Message = "User registred successfully." });
            }
            return BadRequest(new { Message = "User registration faild." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            var command = new LoginUserCommand
            {
                UserName = loginDto.UserName,
                Password = loginDto.Password
            };

            var token = await _mediator.Send(command);

            if (token == null)
                return Unauthorized(new { Message = "Invalid username or password" });

            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutUserCommand());
            return Ok(new { Message = "User logged out successfully" });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var result = await _mediator.Send(new ConfirmEmailCommand { UserId = userId, Token = token });
            if (result) return Ok(new { Message = "Email confirmed successfully." });
            return BadRequest("Error confirming email.");
        }

        [HttpPost("enable-2fa")]
        public async Task<IActionResult> EnableTwoFactorAuthentication([FromBody] EnableTwoFactorAuthCommand command)
        {
            var result = await _mediator.Send(command);
            if (result) return Ok(new { Message = "Two-Factor Authentication enabled successfully." });
            return BadRequest("Error enabling Two-Factor Authentication.");
        }
    }
}
