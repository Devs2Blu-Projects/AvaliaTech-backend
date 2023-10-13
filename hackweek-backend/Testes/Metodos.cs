using hackweek_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace hackweek_backend.Testes
{
    public class Metodos
    {
        public double CalculateFinalGradeByAvaliador(List<RatingCriterionModel> ratingCriteria, int id)
        {
            // Pega a X avaliação
            var rating = Banco.ratings.FirstOrDefault(r => r.Id == id);

            // Pega todos os ratingCriterion que tem a X avaliação, para pegar a nota 
            var ratingCriterion = ratingCriteria.Where(r => r.Rating == rating).ToList();

            //pega as notas de x avaliação por criterio
            List<double> grades = ratingCriterion.Select(r => r.Grade).ToList();

            //pego o peso de cada criterio
            List<uint?> weights = new List<uint?>();

            foreach (var r in ratingCriterion)
            {
                uint? weight = Banco.weights.FirstOrDefault(p => p.Criterion == r.Criterion)?.Weight;
                if (weight != null) weights.Add(weight);
            }

            if (weights.Count != grades.Count) throw new InvalidOperationException("ERRO! Peso e notas não tão organizadas!");

            double finalGrade = 0;


            // soma ponderada
            for (int i = 0; i < grades.Count; i++)
            {
                if (weights[i] <= 0 || grades[i] < 0) throw new Exception("ERRO! Peso ou nota abaixo de 0");
                if (weights[i] == null) throw new Exception("ERRO! Peso ou nota abaixo de 0");
                finalGrade += grades[i] * (int)weights[i];
            }

            return finalGrade;
        }


        // Retorna a nota final de um grupo
        //!! Formula precisa revisar
        public int CalculateFinalGradeByGroup(int idGrupo, List<RatingCriterionModel> ratingCriteria)
        {
            // Pega o grupo e todas as avaliações desse grupo
            var group = Banco.groups.FirstOrDefault(g => g.Id == idGrupo);
            var ratingsGroup = Banco.ratings.Where(r => r.Group == group).ToList();

            int finalGrade = 0;

            // Soma a nota de todas as avaliações desse grupo
            foreach (var i in ratingsGroup)
            {
               // finalGrade += CalculateFinalGradeByAvaliador(i.Id);
            }

            // Divide a nota final pelo o numero de avaliacoes
            finalGrade = finalGrade / ratingsGroup.Count();

            return finalGrade;
        }
    }
}
