using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string msg) : base(msg) { }
    }
}
