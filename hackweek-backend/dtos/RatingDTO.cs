namespace hackweek_backend.dtos
{
    public class RatingDTO
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public List<RatingCriterionDTO>? Grades { get; set; }
    }
}
