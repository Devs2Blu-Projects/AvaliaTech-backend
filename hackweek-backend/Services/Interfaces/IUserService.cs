using hackweek_backend.DTOs;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetUsers();
        Task<UserModel?> GetUserById(int id);
        Task CreateUser(UserModel request);
        Task DeleteUser(int id);
        Task UpdateUser(int id, UserDto request);

        Task<IEnumerable<UserModel>> GetUsersByRole(string role);
        Task<IEnumerable<UserModel>> GetUsersByCourse(string course);
    }
}