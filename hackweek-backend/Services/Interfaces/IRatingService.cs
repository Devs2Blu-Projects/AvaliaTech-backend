using Azure.Core;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingModel>> GetRatings();
        Task<RatingModel?> GetRatingById(int id);
        Task CreateRating(RatingModel request);
        Task DeleteRating(int id);
        Task UpdateRating(int id, RatingModel request);

        Task<IEnumerable<RatingModel>> GetRatingsByUser(int idUser);
        Task<IEnumerable<RatingModel>> GetRatingsByGroup(int idUser);
    }
}