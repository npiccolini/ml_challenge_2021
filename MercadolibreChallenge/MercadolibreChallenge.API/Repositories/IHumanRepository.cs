using MercadolibreChallenge.API.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Repositories
{
    public interface IHumanRepository
    {
        public void Save(Human entity);
        public List<Human> List();
    }
}
