namespace hackweek_backend.Models
{
    public class PropositionCriterionModel
    {
        public int Id { get; set; }
        public uint Weight { get; set; }
        public int PropositionId { get; set; }
        public int CriterionId { get; set; }
        public PropositionModel? Proposition { get; set; }
        public CriterionModel? Criterion { get; set; }
    }
}
