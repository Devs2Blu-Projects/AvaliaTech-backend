using hackweek_backend.Models;

namespace hackweek_backend.dtos
{
    public class GradeDTO
    {
        public int Grade { get; set; }
        public RatingModel? Rating { get; set; }
        public CriterionModel? Criterion { get; set; }
    }
}
