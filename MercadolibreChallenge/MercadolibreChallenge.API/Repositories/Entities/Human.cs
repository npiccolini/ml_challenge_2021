using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Repositories.Entities
{
    [Table("Human")]
    public class Human
    {
        [Key]
        public int Id { get; set; }
        public string Dna { get; set; }
        public bool Mutant { get; set; }
    }
}
