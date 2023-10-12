using Azure.Core;
using hackweek_backend.dtos;
using hackweek_backend.DTOs;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IRatingService
    {
        Task CreateRating(RatingDTO rating);
        Task<List<RatingGetDTO>> GetRatingsByGroup(int idGroup);
        Task DeleteRating(int id);
        Task<RatingGetDTO> GetRatingById(int id);
        double CalculateFinalGradeByRating(int id);
        double CalculateFinalGradeByGroup(int idGrupo);
        Dictionary<int, double> CalculateCriterionGradeByGroup(int idGrupo);
        Task<List<RatingGetDTO>> GetAllRatings();
        Task StartRating(int idGroup);
        Task EndRating(int idGroup);
    }
}