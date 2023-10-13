using hackweek_backend.DTOs;

namespace hackweek_backend.Services.Interfaces
{
    public interface IRatingService
    {
        Task CreateRating(RatingDTO rating);
        Task<List<RatingGetDTO>> GetRatingsByGroup(int idGroup);
        Task DeleteRating(int id);
        Task<RatingGetDTO> GetRatingById(int id);
        Task<List<RatingGetDTO>> GetAllRatings();
    }
}