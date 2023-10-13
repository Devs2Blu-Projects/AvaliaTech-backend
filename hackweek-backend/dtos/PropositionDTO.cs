namespace hackweek_backend.DTOs
{
    public class PropositionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int EventId { get; set; }
        public List<EventCriterionDTO>? PropositionCriteria { get; set; }
    }
}
