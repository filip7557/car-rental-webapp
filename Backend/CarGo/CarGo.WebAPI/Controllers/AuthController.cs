﻿using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> LoginAsync(string email, string password)
        {
            var loginResponse = await _tokenService.LoginAsync(email, password);
            if (!string.IsNullOrWhiteSpace(loginResponse.Token))
                return Ok(loginResponse);

            return Unauthorized("Invalid credentials.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserDTO user)
        {
            var success = await _tokenService.RegisterAsync(user);
            if (success)
                return Ok("Registered.");

            return BadRequest("Email is already in use.");
        }
    }
}