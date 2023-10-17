using hackweek_backend.dtos;

namespace hackweek_backend.Services.Interfaces
{
    public interface IRatingService
    {
        Task CreateRating(RatingDTO rating);
        Task<List<RatingGroupGetDTO>?> GetRatingsByGroup(int idGroup);
        Task DeleteRating(int id);
        Task<RatingGetDTO?> GetRatingById(int id);
        Task<List<RatingGetDTO>?> GetAllRatings();
    }
}