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
        private readonly IGlobalService _globalService;

        public LoginService(DataContext context, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _config = configuration;
            _globalService = globalService;
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

        private UserDto? GetCurrentUser(HttpContext httpContext)
        {
            var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
                return null;

            return new UserDto
            {
                Id = int.Parse(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!),
                Username = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!,
                Name = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value!,
                Role = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value!
            };
        }

        public bool HasAccessToUser(HttpContext httpContext, int idUser)
        {
            var user = GetCurrentUser(httpContext);
            if (user == null) return false;

            return (user.Role == UserRoles.Admin) || (user.Id == idUser);
        }

        public string GetUserRole(HttpContext httpContext)
        {
            var user = GetCurrentUser(httpContext);
            if (user == null) return "";

            return user.Role;
        }

        private string GenerateToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.Name),
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
            var currentId = (await _globalService.GetGlobal()).CurrentEventId;

            var user = await _context.Users.FirstOrDefaultAsync(u => (u.Username == request.Username) && ((u.EventId ?? currentId) == currentId));

            return user;
        }
    }
}
