using hackweek_backend.Models;

namespace hackweek_backend.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public UserDto() { }

        public UserDto(UserModel? user)
        {
            if (user != null)
            {
                Id = user.Id;
                Username = user.Username;
                Name = user.Name;
                Role = user.Role;
            }
        }
    }
}