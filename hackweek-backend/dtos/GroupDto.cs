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
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public uint FinalGrade { get; set; }
        public uint Position { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public List<GroupRatingDto>? GroupRatings { get; set; }

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
                StartTime = group.StartTime;
                EndTime = group.EndTime;
                UserId = group.UserId;
                User = new UserDto(group.User);
                GroupRatings = (group.GroupRatings == null) ? null : group.GroupRatings.Select(gr => new GroupRatingDto(gr)).ToList();
            }
        }
    }
}
