using hackweek_backend.dtos;

namespace hackweek_backend.DTOs
{
    public class RatingDTO
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public List<RatingCriterionDTO> Grades { get; set; }
    }
}
