namespace hackweek_backend.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Team { get; set; } = string.Empty;
        public uint FinalGrade { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public uint Position { get; set; }
        public string Language { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int UserId { get; set; }
        public int PropositionId { get; set; }
        public UserModel? User { get; set; }
        public PropositionModel? Proposition { get; set; }
        public List<GroupRatingModel>? GroupRatings { get; set; }
    }
}
