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

        public UserDto(UserModel user)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.Name = user.Name;
            this.Role = user.Role;
        }
    }
}