using hackweek_backend.Models;

namespace hackweek_backend.DTOs
{
    public class RatingGetDTO
    {
        public int Grade { get; set; }
        public GroupModel? Group { get; set; }
        public UserDto? User { get; set; }    
    }
}
