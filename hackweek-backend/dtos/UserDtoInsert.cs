using hackweek_backend.Models;

namespace hackweek_backend.DTOs
{
    public class UserDtoInsert
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}