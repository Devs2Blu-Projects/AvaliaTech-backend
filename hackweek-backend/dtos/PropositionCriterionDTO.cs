namespace hackweek_backend.DTOs
{
    public class PropositionCriterionDTO
    {
        public int Id { get; set; }
        public uint Weight { get; set; }
        public int PropositionId { get; set; }
        public int CriterionId { get; set; }
    }
}
