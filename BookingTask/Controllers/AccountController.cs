using BookingTask.Models.DTOs;
using BookingTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingTask.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService service)
        {
            _accountService = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto login)
        {
            string token = await _accountService.GenerateJwt(login);
            return Ok(token);
        }
    }
}
