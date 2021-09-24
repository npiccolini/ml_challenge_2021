using MercadolibreChallenge.API.Repositories;
using MercadolibreChallenge.API.Repositories.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Managers.Implementation
{
    public class StatsDto
    {
        public DnaDto ADN { get; set; }
    }

    public class DnaDto
    {
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }
        public decimal ratio { get; set; }
    }

    public class StatsManager
    {
        public StatsDto GetStats()
        {
            var list = new HumanRepository().List();

            return CalculateStats(list);
        }

        public StatsDto GetStats(IHumanRepository humanRepository)
        {
            var list = humanRepository.List();

            return CalculateStats(list);
        }

        private static StatsDto CalculateStats(List<Human> list)
        {
            var count_human_dna = list.Count(x => x.Mutant == false);
            var count_mutant_dna = list.Count(x => x.Mutant == true);
            var ratio = count_mutant_dna / (decimal)count_human_dna;

            return new StatsDto { ADN = new DnaDto { count_mutant_dna = count_mutant_dna, count_human_dna = count_human_dna, ratio = ratio } };
        }
    }
}
