using hackweek_backend.Models;

namespace hackweek_backend.dtos
{
    public class RatingGetGroupDTO
    {
        public double Grade { get; set; }
        public GroupDto? Group { get; set; }
        public UserDto? User { get; set; }
        public CriterionModel? Criterion { get; set; }
    }
}
