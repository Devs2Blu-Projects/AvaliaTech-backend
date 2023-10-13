using hackweek_backend.dtos;
using hackweek_backend.Models;

namespace hackweek_backend.DTOs
{
    public class GroupDto
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
        public List<GroupRatingDto>? GroupRatings { get; set; }

        public GroupDto() { }

        public GroupDto(GroupModel? group)
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
                FinalGrade = group.FinalGrade;
                Position = group.Position;
                DateOffset = group.DateOffset;
                UserId = group.UserId;
                GroupRatings = group.GroupRatings?.Select(gr => new GroupRatingDto(gr)).ToList();
            }
        }
    }
}
