using hackweek_backend.Models;
using hackweek_backend.Testes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace Teste
{
    public class MetodosTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateFinalGradeByAvaliador_notasAleatorias_76()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = 7,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 7,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = 10,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            var result = um.CalculateFinalGradeByAvaliador(notas, 1);
            var expected = 76;

            //Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateFinalGradeByAvaliador_notasAltas_92()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = 9,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 9,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = 10,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            var result = um.CalculateFinalGradeByAvaliador(notas, 1);
            var expected = 92;

            //Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateFinalGradeByAvaliador_notasBaixa_30()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = 3,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 5,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = 0,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            var result = um.CalculateFinalGradeByAvaliador(notas, 1);
            var expected = 30;

            //Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateFinalGradeByAvaliador_gabarito_100()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = 10,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 10,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = 10,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            var result = um.CalculateFinalGradeByAvaliador(notas, 1);
            var expected = 100;

            //Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateFinalGradeByAvaliador_zerado_0()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = 0,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 0,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = 0,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            var result = um.CalculateFinalGradeByAvaliador(notas, 1);
            var expected = 0;

            //Assert

            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void CalculateFinalGradeByAvaliador_notasIguaisImpares_50()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = 5,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 5,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = 5,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            var result = um.CalculateFinalGradeByAvaliador(notas, 1);
            var expected = 50;

            //Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateFinalGradeByAvaliador_extremas_35()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = 0,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 5,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = 10,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            var result = um.CalculateFinalGradeByAvaliador(notas, 1);
            var expected = 35;

            //Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateFinalGradeByAvaliador_notasExtremasInversas_65()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = 10,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 5,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = 0,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            var result = um.CalculateFinalGradeByAvaliador(notas, 1);
            var expected = 65;

            //Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateFinalGradeByAvaliador_notasIguaisPares_80()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = 8,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 8,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = 8,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            var result = um.CalculateFinalGradeByAvaliador(notas, 1);
            var expected = 80;

            //Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateFinalGradeByAvaliador_notasNegativas()
        {
            //Arrange
            var um = new Metodos();

            List<RatingCriterionModel> notas = new List<RatingCriterionModel>
            {
                new RatingCriterionModel
                {
                    Id = 1,
                    Grade = -1,
                    RatingId = 1,
                    CriterionId = 1,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[0],
                },
                new RatingCriterionModel
                {
                    Id = 2,
                    Grade = 0,
                    RatingId = 1,
                    CriterionId = 2,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[1],
                },
                new RatingCriterionModel
                {
                    Id = 3,
                    Grade = -3,
                    RatingId = 1,
                    CriterionId = 3,
                    Rating = Banco.ratings[0],
                    Criterion = Banco.criteria[2],
                },
            };

            //Act
            Assert.Throws<Exception>(() => um.CalculateFinalGradeByAvaliador(notas, 1));
        }
    }
}