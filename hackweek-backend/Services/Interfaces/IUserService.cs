using hackweek_backend.dtos;

namespace hackweek_backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserById(int id);
        Task CreateUser(UserDtoInsert request);
        Task DeleteUser(int id);
        Task UpdateUser(int id, UserDtoUpdate request);

        Task<string> RedefinePassword(int id);
        Task<IEnumerable<UserDto>> GetUsersEventByRole(string role);
    }
}