using MercadolibreChallenge.API.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Repositories
{
    public class ConnStrings
    {
        public string MeliContext { get; set; }
    }

    public class HumanRepository : IHumanRepository
    {
        private MeliContext _context;
        public static ConnStrings connStrings { get; set; } = new ConnStrings();

        public HumanRepository()
        {
            _context = new MeliContext(connStrings.MeliContext);
        }

        public void Save(Human entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public List<Human> List()
        {
            return _context.Humans.ToList();
        }
    }
}
