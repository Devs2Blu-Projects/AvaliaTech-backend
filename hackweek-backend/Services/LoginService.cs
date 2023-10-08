using hackweek_backend.Data;
using hackweek_backend.DTOs;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hackweek_backend.Services
{
    public class LoginService : ILoginService
    {
        private readonly DataContext _context; // TODO: Inject IUserService instead
        private readonly IConfiguration _config;

        public LoginService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        public async Task<string> Login(LoginDto request)
        {
            var user = await GetUserWithPassword(request);
            if (user == null)
                throw new Exception("Usuário não encontrado!");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Senha incorreta!");

            var token = GenerateToken(user);

            return token;
        }

        private string GenerateToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var claims= new []
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],      //Issuer
                _config["Jwt:Audience"],    //Audience
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserModel?> GetUserWithPassword(LoginDto request) // TODO: Move to IUserService
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            return user;
        }
    }
}
