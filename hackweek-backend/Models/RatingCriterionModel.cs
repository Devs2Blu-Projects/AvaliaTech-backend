namespace hackweek_backend.Models
{
    public class RatingCriterionModel
    {
        public int Id { get; set; }
        public double Grade { get; set; }
        public int RatingId { get; set; }
        public int CriterionId { get; set;}
        public RatingModel? Rating { get; set; }
        public CriterionModel? Criterion { get; set; }
    }
}
