using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Exceptions
{
    public class InvalidDnaException : Exception
    {
        public InvalidDnaException(string msg) : base(msg)
        { }
    }
}
