using hackweek_backend.Data;
using hackweek_backend.dtos;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;

namespace hackweek_backend.Services
{
    public class RatingService : IRatingService
    {
        private readonly DataContext _context;
        private readonly IGlobalService _globalService;
        public RatingService(DataContext context, IGlobalService globalService)
        {
            _context = context;
            _globalService = globalService;
        }

        async public Task CreateRating(RatingDTO rating)
        {
            if (rating.Grades == null) throw new Exception($"Critérios de avaliação não enviados!");

            var currentEvent = await _globalService.GetCurrentEvent() ?? throw new Exception($"Evento atual não selecionado!");

            if (currentEvent.IsClosed) throw new Exception("Evento encerrado!");

            RatingModel model = new RatingModel();
            model.UserId = rating.UserId;
            model.GroupId = rating.GroupId;
            _context.Ratings.Add(model);

            await _context.SaveChangesAsync();

            foreach (var item in rating.Grades)
            {
                RatingCriterionModel rc = new RatingCriterionModel
                {
                    Grade = item.Grade,
                    RatingId = model.Id,
                    CriterionId = item.CriterionId,
                };
                _context.RatingCriteria.Add(rc);
            }

            await _context.SaveChangesAsync();

            UpdateGroup(model.GroupId);
        }

        async public Task DeleteRating(int id)
        {
            var rating = _context.Ratings.FirstOrDefault(r => r.Id == id);
            var ratingCriterion = await _context.RatingCriteria.Where(r => r.RatingId == id).ToListAsync();
            if (rating == null) throw new ArgumentException("ID não existente");
            _context.RatingCriteria.RemoveRange(ratingCriterion);
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            UpdateGroup(rating.GroupId);
        }

        public async Task<RatingGetDTO?> GetRatingById(int id)
        {
            var rating = await _context.Ratings
                .Where(r => r.Id == id)
                .Select(r => new
                {
                    User = r.User,
                    Group = r.Group,
                }).FirstOrDefaultAsync();

            if (rating == null) return null;

            double grade = CalculateFinalGradeByRating(id);

            RatingGetDTO result = new RatingGetDTO
            {
                Grade = grade,
                User = new UserDto(rating.User),
                Group = new GroupDto(rating.Group),
            };

            return result;
        }

        public double CalculateFinalGradeByRating(int id)
        {
            var rating = _context.Ratings.FirstOrDefault(r => r.Id == id) ?? throw new InvalidOperationException("Avaliação não encontrada!");

            var ratingCriterion = _context.RatingCriteria.Where(r => r.Rating == rating).ToList();

            List<double> grades = ratingCriterion.Select(r => r.Grade).ToList();

            List<uint?> weights = new List<uint?>();

            foreach (var r in ratingCriterion)
            {
                uint? weight = (uint?)(_context.Criteria.FirstOrDefault(c => c.Id == (int?)r.CriterionId)?.Weight);
                if (weight != null) weights.Add(weight);
            }

            if (weights.Count != grades.Count) throw new InvalidOperationException("ERRO! Peso e notas não tão organizadas!");

            double finalGrade = 0;

            // soma ponderada
            for (int i = 0; i < grades.Count; i++)
            {
                if (grades[i] < 0.00000) grades[i] = 0.00000;
                if (grades[i] > 5.00000) grades[i] = 5.00000;
                if (weights[i] <= 0 || grades[i] < 0) throw new Exception("ERRO! Peso ou nota abaixo de 0");
                if (weights[i] == null) throw new Exception("ERRO! Peso ou nota abaixo de 0");
                finalGrade += grades[i] * (int)(weights[i] ?? 0);
            }
            rating.FinalGrade = finalGrade;
            return finalGrade;
        }


        // Retorna a nota final de um grupo
        //!! Formula precisa revisar
        public double CalculateFinalGradeByGroup(int idGrupo)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == idGrupo);
            var ratingsGroup = _context.Ratings.Where(r => r.Group == group).ToList();

            double finalGrade = 0;

            foreach (var i in ratingsGroup) finalGrade += CalculateFinalGradeByRating(i.Id);

            finalGrade = finalGrade / ratingsGroup.Count();

            return finalGrade;
        }

        public void UpdateGroup(int idGrupo)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == idGrupo);
            if (group == null) return;

            group.FinalGrade = CalculateFinalGradeByGroup(idGrupo);
            CalculateCriterionGradeByGroup(group);

            _context.SaveChanges();

        }

        public void CalculateCriterionGradeByGroup(GroupModel group)
        {
            List<RatingModel> ratingsByGroup = _context.Ratings.Where(r => r.GroupId == group.Id).ToList();
            var propCriterion = _context.Criteria.Where(pc => pc.EventId == group.EventId).ToList(); 

            _context.GroupRatings.Where(g => g.GroupId == group.Id).ExecuteDelete();

            Dictionary<int, double> lista = new Dictionary<int, double>();

            // Avaliacao
            foreach (var i in ratingsByGroup)
            {
                var y = _context.RatingCriteria.Where(rc => rc.RatingId == i.Id).ToList();

                // criterios dql avaliacao
                foreach (var j in y)
                {
                    if(j.Grade < 0) j.Grade = 0;
                    if (j.Grade > 5.00000) j.Grade = 5.00000;

                    double myGrade = j.Grade / ratingsByGroup.Count();
                    var criterion = _context.Criteria.FirstOrDefault(c => c.Id == j.CriterionId);

                    if (criterion != null)
                    {
                        var gr = _context.GroupRatings.FirstOrDefault(gp => gp.CriterionId == criterion.Id);

                        if (gr == null)
                        {
                            gr = new GroupRatingModel()
                            {
                                Grade = myGrade,
                                CriterionId = criterion.Id,
                                GroupId = group.Id
                            };
                        }
                        else gr.Grade += myGrade;
                    }
                }
            }
            _context.SaveChanges();
        }


        async public Task<List<RatingGetDTO>?> GetAllRatings()
        {
            var ratings = await _context.Ratings.Select(r => new
            {
                Id = r.Id,
                User = r.User,
                Group = r.Group,
            }).ToListAsync();

            if (ratings.Count == 0 || ratings == null) return null;

            List<RatingGetDTO> retorno = new List<RatingGetDTO>();

            for (int i = 0; i < ratings.Count; i++)
            {
                double grade = CalculateFinalGradeByRating(ratings[i].Id);

                RatingGetDTO j = new RatingGetDTO()
                {
                    User = new UserDto(ratings[i].User),
                    Group = new GroupDto(ratings[i].Group),
                    Grade = grade
                };
                retorno.Add(j);
            }

            return retorno;
        }

        async public Task<List<RatingGroupGetDTO>?> GetRatingsByGroup(int idGroup)
        {
            List<RatingModel> ratings = await _context.Ratings.Where(r => r.GroupId == idGroup).ToListAsync();

            if (ratings.Count == 0 || ratings == null) return null;

            List<RatingGroupGetDTO> retorno = new List<RatingGroupGetDTO>();

            foreach (var r in ratings)
            {
                double grade = CalculateFinalGradeByRating(r.Id);

                RatingGroupGetDTO j = new RatingGroupGetDTO
                {
                    Id = r.Id,
                    Grade = grade,
                    User = new UserDto(r.User),
                    Group = new GroupDto(r.Group),
                };
                retorno.Add(j);
            }
            return retorno;
        }
    }
}
