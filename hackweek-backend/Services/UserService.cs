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

        public UserService(DataContext context) { _context = context; }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        
        public async Task<UserModel?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        
        public async Task CreateUser(UserModel request)
        {
            if (_context.Users.FirstOrDefault(u => u.Username == request.Username) != null) throw new Exception("Nome de usuário já cadastrado!");

            await _context.Users.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("Usuário não encontrado!");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(int id, UserDto request)
        {
            if (request.Id != id) throw new Exception("Usuário possui Id diferente do informado!");

            var user = await _context.Users.FindAsync(id) ?? throw new Exception("Usuário não encontrado!");

            user.Username = request.Username;
            user.Name = request.Name;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserModel>> GetUsersByRole(string role)
        {
            return await _context.Users.Where(u => u.Role == role).ToListAsync();
        }
    }
}