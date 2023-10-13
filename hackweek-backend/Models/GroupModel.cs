namespace hackweek_backend.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Team { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int? PropositionId { get; set; }
        public PropositionModel? Proposition { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public uint FinalGrade { get; set; }
        public uint Position { get; set; }
        public uint DateOffset { get; set; }
        public int UserId { get; set; }
        public UserModel? User { get; set; }
        public List<GroupRatingModel>? GroupRatings { get; set; }
        public int EventId { get; set; }
        public EventModel? Event { get; set; }
    }
}
