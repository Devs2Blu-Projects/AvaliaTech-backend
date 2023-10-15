using hackweek_backend.Models;

namespace hackweek_backend.dtos
{
    public class GroupDtoWithoutGrade
    {
        public int Id { get; set; }
        public string Team { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int? PropositionId { get; set; }
        public PropositionModel? Proposition { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public uint Position { get; set; }
        public uint DateOffset { get; set; }

        public GroupDtoWithoutGrade() { }

        public GroupDtoWithoutGrade(GroupModel? group)
        {
            if (group != null)
            {
                Id = group.Id;
                Team = group.Team;
                Language = group.Language;
                PropositionId = group.PropositionId;
                Proposition = group.Proposition;
                ProjectName = group.ProjectName;
                ProjectDescription = group.ProjectDescription;
                Position = group.Position;
                DateOffset = group.DateOffset;
            }
        }
    }
}
