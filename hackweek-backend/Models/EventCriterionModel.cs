namespace hackweek_backend.Models
{
    public class EventCriterionModel
    {
        public int Id { get; set; }
        public uint Weight { get; set; }
        public int EventId { get; set; }
        public int CriterionId { get; set; }
        public EventModel? Event { get; set; }
        public CriterionModel? Criterion { get; set; }
    }
}
