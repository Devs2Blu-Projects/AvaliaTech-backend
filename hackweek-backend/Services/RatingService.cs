using hackweek_backend.Data;
using hackweek_backend.DTOs;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hackweek_backend.Services
{
    public class RatingService : IRatingService
    {
        private readonly DataContext _context;
        public RatingService(DataContext context) { _context = context; }


        //criando a avaliação
        async public Task CreateRating(RatingDTO rating)
        {
            RatingModel model = new RatingModel();
            model.UserId = rating.UserId;
            model.GroupId = rating.GroupId;
            _context.Ratings.Add(model);
            await _context.SaveChangesAsync();
        }

        async public Task DeleteRating(int id)
        {
           var rating = _context.Ratings.FirstOrDefault(r => r.Id == id);
           //var ratingCriterion = _context.RatingCriteria.FirstOrDefault(r => r.RatingId == id);
           if (rating == null) throw new ArgumentException("ID não existente");
           _context.Ratings.Remove(rating);
           //_context.RatingCriteria.Remove(ratingCriterion);
           await _context.SaveChangesAsync();
        }


        // avaliacao inteira, com a nota e etc // ERRADO!
        public async Task<IEnumerable<RatingGetDTO>> GetRatingById(int id)
        {
            var rating = await _context.RatingCriteria.FirstOrDefaultAsync(rating => rating.RatingId == id);

            if (rating == null)  return null; 
            
            var rating2 = await _context.RatingCriteria
                .Where(r => r.RatingId == id)
                .Select(r => new RatingGetDTO
                {
                    Grade = r.Grade,
                    User = r.Rating.User,
                    Group = r.Rating.Group,
                })
                .ToListAsync();

            return rating2;
        }

        async public Task<IEnumerable<RatingModel>> GetRatings()
        {
            throw new NotImplementedException();
        }

        async public Task<IEnumerable<RatingModel>> GetRatingsByGroup(int idUser)
        {
            throw new NotImplementedException();
        }

        async public Task<IEnumerable<RatingModel>> GetRatingsByUser(int idUser)
        {
            throw new NotImplementedException();
        }

        async public Task UpdateRating(int id, RatingModel user)
        {
            throw new NotImplementedException();
        }
    }
}
