using hackweek_backend.Models;

namespace hackweek_backend.Testes
{
    public class Banco
    {
        public static List<UserModel> users = new List<UserModel>
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
                Name = "Desafio 1"
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
                User = users[1],
                Proposition = propositions[0]
            }
        };

        public static List<CriterionModel> criteria = new List<CriterionModel>
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

       /* public static List<EventCriterionModel> weights = new List<EventCriterionModel>
        {
            new EventCriterionModel()
            {
                Id = 1,
                Weight = 5,
                EventId = 1,
                CriterionId = 1,
                Criterion = criteria[0]
            },
            new EventCriterionModel()
            {
                Id = 2,
                Weight = 3,
                EventId = 1,
                CriterionId = 2,
                Criterion = criteria[1]
            },
            new EventCriterionModel()
            {
                Id = 3,
                Weight = 2,
                EventId = 1,
                CriterionId = 3,
                Criterion = criteria[2]
            }
        };*/

        public static List<RatingModel> ratings = new List<RatingModel>
        {
            new RatingModel()
            {
                Id = 1,
                GroupId = 1,
                UserId = 1,
                Group = groups[0],
                User = users[0]
            }
        };
    }
}
