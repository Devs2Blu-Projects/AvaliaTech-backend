using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetUsers();
        Task<UserModel?> GetUserById(int id);
        Task CreateUser(UserModel user);
        Task DeleteUser(int id);
        Task UpdateUser(int id, UserModel user);

        Task<IEnumerable<UserModel>> GetUsersByRole(int idUser);
        Task<IEnumerable<UserModel>> GetUsersByCourse(int idUser);
    }
}