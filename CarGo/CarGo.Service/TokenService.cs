using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarGo.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public TokenService(IConfiguration config, IRoleService roleService, IUserService userService)
        {
            _config = config;
            _roleService = roleService;
            _userService = userService;
        }

        private async Task<string> GenerateTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, await _roleService.GetRoleNameByIdAsync(user.RoleId!)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterAsync(User user)
        {
            if (await _userService.GetUserByEmailAsync(user.Email) != null)
                return false;

            string hashedPassword = HashPassword(user.Password!);

            user.Password = hashedPassword;
            if (user.RoleId == null)
                user.RoleId = await _roleService.GetDefaultRoleIdAsync();

            await _userService.RegisterUserAsync(user);

            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailAndPasswordAsync(email, HashPassword(password));
            if (user != null)
            {
                var token = await GenerateTokenAsync(user);
                return token;
            }
            return "";
        }
    }
}