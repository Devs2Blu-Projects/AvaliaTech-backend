namespace hackweek_backend.Models
{
    public class PropositionModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int EventId { get; set; }
        public EventModel? Event { get; set; }

        public List<PropositionCriterionModel>? PropositionCriteria { get; set; }
    }
}
