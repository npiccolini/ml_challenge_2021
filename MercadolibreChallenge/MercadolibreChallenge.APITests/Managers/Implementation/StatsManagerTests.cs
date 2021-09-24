using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using MercadolibreChallenge.API.Repositories;
using MercadolibreChallenge.API.Repositories.Entities;
using System.Linq;

namespace MercadolibreChallenge.API.Managers.Implementation.Tests
{
    [TestClass()]
    public class StatsManagerTests
    {
        [TestMethod]
        public void TestContosoStockPrice()
        {
            // Arrange
            var humanRepository = new Mock<IHumanRepository>();
            var statManager = new StatsManager();

            var listHumans = new List<Human>
            {
                new Human { Id = 1, Dna = string.Empty, Mutant = false },
                new Human { Id = 2, Dna = string.Empty, Mutant = true },
                new Human { Id = 3, Dna = string.Empty, Mutant = false }
            };

            humanRepository.Setup(x => x.List()).Returns(listHumans.ToList());

            // Act:
            var result = statManager.GetStats(humanRepository.Object);

            // Assert:
            var obj = new StatsDto { ADN = new DnaDto { count_mutant_dna = 1, count_human_dna = 2, ratio = (decimal)0.5 } };

            Assert.AreEqual(obj.ADN.count_human_dna, result.ADN.count_human_dna);
            Assert.AreEqual(obj.ADN.count_mutant_dna, result.ADN.count_mutant_dna);
            Assert.AreEqual(obj.ADN.ratio, result.ADN.ratio);
        }
    }
}
