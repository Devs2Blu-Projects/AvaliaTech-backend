namespace hackweek_backend.DTOs
{
    public class EventCriterionDTO
    {
        public int Id { get; set; }
        public uint Weight { get; set; }
        public int EventId { get; set; }
        public int CriterionId { get; set; }
    }
}
