﻿using hackweek_backend.Data;
using hackweek_backend.dtos;
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

        async public Task CreateRating(RatingDTO rating)
        {
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
           if (rating == null ) throw new ArgumentException("ID não existente");
            _context.RatingCriteria.RemoveRange(ratingCriterion);
           _context.Ratings.Remove(rating);
           await _context.SaveChangesAsync();
        }

        public async Task<RatingGetDTO> GetRatingById(int id)
        {
            var rating = await _context.Ratings
                .Where(r => r.Id == id)
                .Select(r => new
                {
                    User = r.User,
                    Group = r.Group,
                }).FirstOrDefaultAsync();

            if (rating == null)  return null;

            double grade = CalculateFinalGradeByRating(id);

            RatingGetDTO result = new RatingGetDTO
            {
                Grade = grade,
                User = new UserDto(rating.User),
                Group = new GroupDto(rating.Group),
            };

            return result;
        }


        // Calculo da nota final da avaliacao de X avaliador com X grupo
        public double CalculateFinalGradeByRating(int id)
        {
            // Pega a X avaliação
            var rating =  _context.Ratings.FirstOrDefault(r => r.Id ==id);

            // Pega todos os ratingCriterion que tem a X avaliação, para pegar a nota 
            var ratingCriterion =  _context.RatingCriteria.Where(r => r.Rating == rating).ToList();

            //pega as notas de x avaliação por criterio
            List<double> grades =  ratingCriterion.Select(r => r.Grade).ToList();

            //pego o peso de cada criterio
            List<uint?> weights = new List<uint?>();

            foreach(var r in ratingCriterion)
            {
                uint? weight = _context.PropositionsCriteria.FirstOrDefault(p => p.Criterion == r.Criterion)?.Weight;
                if (weight != null) weights.Add(weight);
            }

            if (weights.Count != grades.Count) throw new InvalidOperationException("ERRO! Peso e notas não tão organizadas!");

            double finalGrade = 0;
            // soma ponderada
            for(int i = 0; i< grades.Count; i++)
            {
                if (weights[i] <= 0 || grades[i] < 0) throw new Exception("ERRO! Peso ou nota abaixo de 0");
                if (weights[i] == null) throw new Exception("ERRO! Peso ou nota abaixo de 0");
                finalGrade += grades[i] * (int)weights[i];
            }
            rating.FinalGrade = finalGrade;
            return finalGrade;
        }


        // Retorna a nota final de um grupo
        //!! Formula precisa revisar
        public double CalculateFinalGradeByGroup(int idGrupo)
        {
            // Pega o grupo e todas as avaliações desse grupo
            var group = _context.Groups.FirstOrDefault(g => g.Id == idGrupo);
            var ratingsGroup = _context.Ratings.Where(r => r.Group == group).ToList();

            double finalGrade = 0;

            // Soma a nota de todas as avaliações desse grupo
            foreach(var i in ratingsGroup)  finalGrade += CalculateFinalGradeByRating(i.Id);

            // Divide a nota final pelo o numero de avaliacoes
            finalGrade = finalGrade / ratingsGroup.Count();

            return finalGrade;
        }

        public void UpdateGroup(int idGrupo)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == idGrupo);
            group.FinalGrade = CalculateFinalGradeByGroup(idGrupo);
            CalculateCriterionGradeByGroup(idGrupo);


        }

        public Dictionary<int, double> CalculateCriterionGradeByGroup(int idGrupo)
        {
            List<RatingModel> ratingsByGroup = _context.Ratings.Where(r => r.GroupId == idGrupo).ToList();

            Dictionary<int, double> lista = new Dictionary<int, double>();

            foreach (var i in ratingsByGroup)
            {
                var y = _context.RatingCriteria.FirstOrDefault(rc => rc.RatingId == i.Id);
                if (lista[y.CriterionId] != null)  lista[y.CriterionId] += y.Grade;
                else lista[y.CriterionId] = y.Grade;
            }

            return lista;
        }

        async public Task<List<RatingGetDTO>> GetAllRatings()
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
                double grade = CalculateFinalGradeByRating(ratings[i].Id);
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

        async public Task<List<RatingGetDTO>> GetRatingsByGroup(int idGroup)
        {
            List<RatingModel> ratings = await _context.Ratings.Where(r => r.GroupId == idGroup).ToListAsync();
            if (ratings.Count == 0 || ratings == null) return null;


            List<RatingGetDTO> retorno = new List<RatingGetDTO>();

            foreach(var r in ratings)
            {
                double grade = CalculateFinalGradeByRating(r.Id);
                
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

       /* async public Task CreateGrade(GradeDTO grade)
        {
            if (grade.Grade < 0) throw new Exception("Notas tem que ser positivas!");
            RatingCriterionModel Grade = new RatingCriterionModel();
            Grade.Grade = grade.Grade;
            Grade.Criterion = grade.Criterion;
            Grade.Rating = grade.Rating;
            Grade.RatingId = grade.Rating.Id;
            Grade.CriterionId = grade.Criterion.Id;
            _context.RatingCriteria.Add(Grade);
            await _context.SaveChangesAsync();
        }*/

        //Raphael
        async public Task StartRating(int idGroup)
        {
            var group = await _context.Groups.FindAsync(idGroup) ?? throw new Exception($"Grupo não encontrado! ({idGroup})");

            group.StartTime ??= DateTime.Now;
            group.EndTime = null;
            await _context.SaveChangesAsync();
        }

        //Raphael
        async public Task EndRating(int idGroup)
        {
            var group = await _context.Groups.FindAsync(idGroup) ?? throw new Exception($"Grupo não encontrado! ({idGroup})");
            var criteria = await _context.PropositionsCriteria.Where(pc => pc.PropositionId == group.PropositionId).ToListAsync();
            
            CalculateCriterionGradeByGroup(idGroup);
            group.FinalGrade = (uint) CalculateFinalGradeByGroup(idGroup);
            group.EndTime ??= DateTime.Now;
            await _context.SaveChangesAsync();
        }

        
    }
}
