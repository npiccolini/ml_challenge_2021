using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Managers
{
    public interface IMutantManager
    {
        public bool IsMutant(string[] dna);
    }
}
