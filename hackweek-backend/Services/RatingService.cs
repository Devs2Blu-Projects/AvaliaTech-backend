using hackweek_backend.Data;
using hackweek_backend.DTOs;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;

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


        // Deleta a avaliação da tabela de Rating e todas as N->N da tabela de RatingCriteria
        // Quero deletar x avaliação (de X grupo e X avaliador)
        async public Task DeleteRatingByAvaliadorById(int id)
        {
           var rating = _context.Ratings.FirstOrDefault(r => r.Id == id);
           var ratingCriterion = await _context.RatingCriteria.Where(r => r.RatingId == id).ToListAsync();
           if (rating == null ) throw new ArgumentException("ID não existente");
            _context.RatingCriteria.RemoveRange(ratingCriterion);
           _context.Ratings.Remove(rating);
           await _context.SaveChangesAsync();
        }


        // Retorna a avaliação inteira (por ID): Avaliador, Grupo e Nota Final do avaliador
        // Exemplo: Quero pegar avaliação de X grupo do Y avaliador
        public async Task<RatingGetDTO> GetRatingByIdByAvaliador(int id)
        {
            var rating = await _context.Ratings
                .Where(r => r.Id == id)
                .Select(r => new
                {
                    User = r.User,
                    Group = r.Group,
                }).FirstOrDefaultAsync();

            if (rating == null)  return null;

            int grade = CalculateFinalGradeByAvaliador(id);

            RatingGetDTO result = new RatingGetDTO
            {
                Grade = grade,
                User = new UserDto(rating.User),
                Group = new GroupDto(rating.Group),
            };

            return result;
        }


        // Calculo da nota final da avaliacao de X avaliador com X grupo
        public int CalculateFinalGradeByAvaliador(int id)
        {
            // Pega a X avaliação
            var rating =  _context.Ratings.FirstOrDefault(r => r.Id ==id);

            // Pega todos os ratingCriterion que tem a X avaliação, para pegar a nota 
            var ratingCriterion =  _context.RatingCriteria.Where(r => r.Rating == rating).ToList();

            //pega as notas de x avaliação por criterio
            var grades =  ratingCriterion.Select(r => r.Grade).ToList();

            //pego o peso de cada criterio
            List<uint?> weights = new List<uint?>();

            foreach(var r in ratingCriterion)
            {
                uint? weight = _context.PropositionsCriteria.FirstOrDefault(p => p.Criterion == r.Criterion)?.Weight;
                if (weight != null) weights.Add(weight);
            }

            if (weights.Count != grades.Count) throw new InvalidOperationException("ERRO! Peso e notas não tão organizadas!");

            var finalGrade = 0;


            // soma ponderada
            for(int i = 0; i< grades.Count; i++)
            {
                if (weights[i] <= 0 || grades[i] < 0) throw new Exception("ERRO! Peso ou nota abaixo de 0");
                if (weights[i] == null) throw new Exception("ERRO! Peso ou nota abaixo de 0");
                finalGrade += grades[i] * (int)weights[i];
            }

            return finalGrade;
        }


        // Retorna a nota final de um grupo
        //!! Formula precisa revisar
        public int CalculateFinalGradeByGroup(int idGrupo)
        {
            // Pega o grupo e todas as avaliações desse grupo
            var group = _context.Groups.FirstOrDefault(g => g.Id == idGrupo);
            var ratingsGroup = _context.Ratings.Where(r => r.Group == group).ToList();

            int finalGrade = 0;

            // Soma a nota de todas as avaliações desse grupo
            foreach(var i in ratingsGroup)
            {
                finalGrade += CalculateFinalGradeByAvaliador(i.Id);
            }

            // Divide a nota final pelo o numero de avaliacoes
            finalGrade = finalGrade / ratingsGroup.Count();

            return finalGrade;
        }

        // Retorna TODAS as avaliações com: User, Group e notaFinal do avaliador
        // Quero uma lista com cada grupo e cada notaFinal
        async public Task<List<RatingGetDTO>> GetAllRatingsByAvaliador()
        {
            var ratings = await _context.Ratings.Select(r => new
            {
                Id = r.Id,
                User = r.User,
                Group = r.Group,
            }).ToListAsync();

            if (ratings.Count == 0 || ratings == null) return null;

            List<RatingGetDTO> retorno = new List<RatingGetDTO>();
            
            for(int i = 0; i < ratings.Count; i++)
            {
                int grade = CalculateFinalGradeByAvaliador(ratings[i].Id);
                if (grade <= 0) i++;
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

        async public Task<List<RatingGetDTO>> GetAllRatingsByGroup(int idGroup)
        {
            var ratings = await _context.Ratings.Where(r => r.GroupId == idGroup).ToListAsync();
            if (ratings.Count == 0 || ratings == null) return null;

            List<RatingGetDTO> retorno = new List<RatingGetDTO>();

            foreach(var r in ratings)
            {
                int grade = CalculateFinalGradeByAvaliador(r.Id);
                
                RatingGetDTO j = new RatingGetDTO 
                {
                    Grade = grade,
                    User = new UserDto(r.User),
                    Group = new GroupDto(r.Group),
                };

                retorno.Add(j);
            }

            return retorno;


        }



        async public Task UpdateRating(int id, RatingModel request)
        {
            throw new NotImplementedException();
        }
    }
}
