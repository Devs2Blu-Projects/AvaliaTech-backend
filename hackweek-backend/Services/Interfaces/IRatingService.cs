using Azure.Core;
using hackweek_backend.DTOs;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IRatingService
    {
        Task<List<RatingGetDTO>> GetAllRatingsByAvaliador();
        //Task<RatingModel?> GetRatingById(int id);
        Task CreateRating(RatingDTO rating);
        Task DeleteRatingByAvaliadorById(int id);
        Task<RatingGetDTO> GetRatingByIdByAvaliador(int id);
        int CalculateFinalGradeByAvaliador(int id);
        int CalculateFinalGradeByGroup(int idGrupo);

        Task UpdateRating(int id, RatingModel request);

        //Task<IEnumerable<RatingModel>> GetRatingsByUser(int idUser);
        Task<List<RatingGetDTO>> GetAllRatingsByGroup(int idGroup);
    }
}