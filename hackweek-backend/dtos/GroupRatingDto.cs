﻿using hackweek_backend.Models;

namespace hackweek_backend.dtos
{
    public class GroupRatingDto
    {
        public int Id { get; set; }
        public double Grade { get; set; }
        public int GroupId { get; set; }
        public int CriterionId { get; set; }
        public CriterionModel? Criterion { get; set; }

        public GroupRatingDto(GroupRatingModel? groupRating)
        {
            if (groupRating != null)
            {
                Id = groupRating.Id;
                Grade = groupRating.Grade;
                GroupId = groupRating.GroupId;
                CriterionId = groupRating.CriterionId;
                Criterion = groupRating.Criterion;
            }
        }
    }
}
