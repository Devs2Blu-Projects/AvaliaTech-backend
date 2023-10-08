using hackweek_backend.Models;

namespace hackweek_backend.DTOs
{
    public class RatingGetGroupDTO
    {
        public int Grade { get; set; }
        public GroupModel? Group { get; set; }
        public UserDto? User { get; set; }
        public CriterionModel? Criterion { get; set; }
    }
}
