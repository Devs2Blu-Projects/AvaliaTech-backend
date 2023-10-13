namespace hackweek_backend.Models
{
    public class RatingModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public double FinalGrade { get; set; }
        public GroupModel? Group { get; set; }
        public UserModel? User { get; set; }
        public List<RatingCriterionModel>? RatingCriteria { get; set; }
    }
}
