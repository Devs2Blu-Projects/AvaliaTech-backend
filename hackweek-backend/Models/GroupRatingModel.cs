namespace hackweek_backend.Models
{
    public class GroupRatingModel
    {
        public int Id { get; set; }
        public uint Grade { get; set; }
        public int GroupId { get; set; }
        public int PropositionCriterionId { get; set; }
        public GroupModel? Group { get; set; }
        public PropositionCriterionModel? PropositionCriterion { get; set; }
    }
}
