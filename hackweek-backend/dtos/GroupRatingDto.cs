using hackweek_backend.Models;

namespace hackweek_backend.DTOs
{
    public class GroupRatingDto
    {
        public int Id { get; set; }
        public uint Grade { get; set; }
        public int GroupId { get; set; }
        public int PropositionCriterionId { get; set; }

        public GroupRatingDto(GroupRatingModel? groupRating)
        {
            if (groupRating != null)
            {
                Id = groupRating.Id;
                Grade = groupRating.Grade;
                GroupId = groupRating.GroupId;
                PropositionCriterionId = groupRating.PropositionCriterionId;
            }
        }
    }
}
