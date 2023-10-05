using Azure.Core;
using hackweek_backend.DTOs;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingModel>> GetRatings();
        Task<RatingModel?> GetRatingById(int id);
        Task CreateRating(RatingDTO rating);
        Task DeleteRating(int id);
        Task UpdateRating(int id, RatingModel user);

        Task<IEnumerable<RatingModel>> GetRatingsByUser(int idUser);
        Task<IEnumerable<RatingModel>> GetRatingsByGroup(int idUser);
    }
}