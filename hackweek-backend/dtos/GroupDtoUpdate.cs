namespace hackweek_backend.dtos
{
    public class GroupDtoUpdate
    {
        public int Id { get; set; }
        public string Team { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int? PropositionId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
    }
}
