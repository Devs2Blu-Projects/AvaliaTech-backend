using hackweek_backend.Data;
using hackweek_backend.dtos;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using System.Data;

namespace hackweek_backend.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IGlobalService _globalService;

        private readonly string[] _allowCreateRoleList = {
            UserRoles.Group,
            UserRoles.User,
        };
        private readonly string[] _allowGetByRoleList = {
            UserRoles.Group,
            UserRoles.User,
        };

        public UserService(DataContext context, IGlobalService globalService)
        {
            _context = context;
            _globalService = globalService;
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

            var eventId = (await _globalService.GetGlobal()).CurrentEventId ?? throw new Exception($"Evento atual não selecionado!");

            var userModel = new UserModel
            {
                Username = request.Username,
                Name = request.Name,
                Role = request.Role,
                EventId = eventId,
            };

            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();

            if (request.Role == UserRoles.Group) await InternalCreateGroup(userModel.Id, userModel.Username, eventId);
        }

        private async Task InternalCreateGroup(int userId, string projectName, int eventId)
        {
            await _context.Groups.AddAsync(new GroupModel
            {
                ProjectName = projectName,
                UserId = userId,
                Position = (await _context.Groups.AnyAsync()) ? await _context.Groups.MaxAsync(g => g.Position) + 1 : 1,
                EventId = eventId,
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

            var password = Guid.NewGuid().ToString().Replace("-", "");

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

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetUsersEventByRole(string role)
        {
            if (_allowGetByRoleList.FirstOrDefault(r => r == role) == null) return new List<UserDto>();

            var global = await _globalService.GetGlobal();

            return await _context.Users.Where(u => (u.Role == role) && (u.EventId == global.CurrentEventId))
                .Select(u => new UserDto(u))
                .ToListAsync();
        }
    }
}