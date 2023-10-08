using hackweek_backend.dtos;
using hackweek_backend.Models;

namespace hackweek_backend.DTOs
{
    public class GroupDto
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
        public int? PropositionId { get; set; }
        public UserDto? User { get; set; }
        public PropositionModel? Proposition { get; set; }
        public List<GroupRatingDto>? GroupRatings { get; set; }

        public GroupDto(GroupModel? group)
        {
            if (group != null)
            {
                Id = group.Id;
                Team = group.Team;
                FinalGrade = group.FinalGrade;
                ProjectDescription = group.ProjectDescription;
                Position = group.Position;
                Language = group.Language;
                StartTime = group.StartTime;
                EndTime = group.EndTime;
                UserId = group.UserId;
                PropositionId = group.PropositionId;
                User = new UserDto(group.User);
                Proposition = group.Proposition;
                GroupRatings = (group.GroupRatings == null) ? null : group.GroupRatings.Select(gr => new GroupRatingDto(gr)).ToList();
            }
        }
    }
}
