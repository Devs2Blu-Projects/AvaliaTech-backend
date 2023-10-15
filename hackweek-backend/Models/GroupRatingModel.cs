namespace hackweek_backend.Models
{
    public class GroupRatingModel
    {
        public int Id { get; set; }
        public  double Grade { get; set; }
        public int GroupId { get; set; }
        public int CriterionId { get; set; }
        public GroupModel? Group { get; set; }
        public CriterionModel? Criterion { get; set; }
    }
}
