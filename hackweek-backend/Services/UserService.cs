using hackweek_backend.Data;
using hackweek_backend.DTOs;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using System.Data;

namespace hackweek_backend.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly string[] _allowCreateRoleList = {
            UserRoles.Admin,
            UserRoles.Group,
            UserRoles.User,
        };
        private readonly string[] _allowGetByRoleList = {
            UserRoles.Group,
            UserRoles.User,
        };

        public UserService(DataContext context) { _context = context; }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _context.Users
                .Select(u => new UserDto(u))
                .ToListAsync();
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return null;

            return new UserDto(user);
        }

        public async Task CreateUser(UserDtoInsert request)
        {
            if (_context.Users.FirstOrDefault(u => u.Username == request.Username) != null) throw new Exception($"Usuário já cadastrado! ({request.Username})");

            if (_allowCreateRoleList.FirstOrDefault(r => r == request.Role) == null) throw new Exception($"Cargo inválido! ({request.Role})");

            var model = new UserModel
            {
                Username = request.Username,
                Name = request.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
            };

            await _context.Users.AddAsync(model);
            if (request.Role == UserRoles.Group) await _context.Groups.AddAsync(new GroupModel
            {
                UserId = model.Id,
                Position = _context.Groups.Max(g => g.Position) + 1,
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new Exception("Usuário não encontrado!");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> RedefinePassword(int id)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new Exception($"Usuário não cadastrado! ({id})");

            var password = Guid.NewGuid().ToString().Replace("-","");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            await _context.SaveChangesAsync();

            return password;
        }

        public async Task UpdateUser(int id, UserDtoUpdate request)
        {
            if (request.Id != id) throw new Exception($"Id diferente do usuário informado! ({id} - {request.Id})");

            var user = await _context.Users.FindAsync(id) ?? throw new Exception($"Usuário não encontrado! ({request.Id})");

            user.Username = request.Username;
            user.Name = request.Name;
            if (request.Password != string.Empty) user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetUsersByRole(string role)
        {
            if (_allowGetByRoleList.FirstOrDefault(r => r == role) == null) return new List<UserDto>();

            return await _context.Users.Where(u => u.Role == role)
                .Select(u => new UserDto(u))
                .ToListAsync();
        }
    }
}