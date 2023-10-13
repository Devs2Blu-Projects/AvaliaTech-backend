namespace hackweek_backend.Models
{
    public class GroupRatingModel
    {
        public int Id { get; set; }
        public double Grade { get; set; }
        public int GroupId { get; set; }
        public int EventCriterionId { get; set; }
        public GroupModel? Group { get; set; }
        public EventCriterionModel? EventCriterion { get; set; }
    }
}
