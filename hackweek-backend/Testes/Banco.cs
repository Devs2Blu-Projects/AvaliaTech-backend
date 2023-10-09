using hackweek_backend.Models;
using System.Collections.Generic;

namespace hackweek_backend.Testes
{
    public class Banco
    {
        public static List<UserModel> user = new List<UserModel>
        {
            new UserModel
            {
                Id = 1,
                Username = "maria",
                Name = "Maria",
                PasswordHash = "123",
                Role = "Avaliator"
            },
            new UserModel
            {
                Id = 2,
                Username = "joao",
                Name = "João",
                PasswordHash = "123",
                Role = "Student"
            }
        };

        public static List<PropositionModel> propositions = new List<PropositionModel>
        {
            new PropositionModel
            {
                Id = 1,
                Name = "Desafio 1",
                PropositionCriteria = weights
            }
        };

        public static List<GroupModel> groups = new List<GroupModel>
        {
            new GroupModel()
            {
                Id = 1,
                Team = "julio, marcia e ricardo",
                FinalGrade = 0,
                ProjectDescription = "github",
                Language = "C#",
                UserId = 2,
                PropositionId = 1,
                User = user[1],
                Proposition = propositions[0]

            }
        };

        public  static List<CriterionModel> criteria = new List<CriterionModel> 
        {
            new CriterionModel()
            {
                Id = 1,
                Name = "Funcionalidade",
                Description = ""
            },
            new CriterionModel()
            {
                Id = 2,
                Name = "Apresentacao",
                Description = ""
            },
            new CriterionModel()
            {
                Id = 3,
                Name = "Prazo",
                Description = ""
            },
        };

        public static List<PropositionCriterionModel> weights = new List<PropositionCriterionModel>
        {
            new PropositionCriterionModel()
            {
                Id = 1,
                Weight = 5,
                PropositionId = 1,
                CriterionId = 1,
                Proposition = propositions[0],
                Criterion = criteria[0]
            },
            new PropositionCriterionModel()
            {
                Id = 2,
                Weight = 3,
                PropositionId = 1,
                CriterionId = 2,
                Proposition = propositions[0],
                Criterion = criteria[1]
            },
            new PropositionCriterionModel()
            {
                Id = 3,
                Weight = 2,
                PropositionId = 1,
                CriterionId = 3,
                Proposition = propositions[0],
                Criterion = criteria[2]
            }
        };
        public static List<RatingModel> ratings = new List<RatingModel>
        {
            new RatingModel()
            {
                Id = 1,
                GroupId = 1,
                UserId = 1,
                Group = groups[0],
                User = user[0]
            }
        };

    }
}
