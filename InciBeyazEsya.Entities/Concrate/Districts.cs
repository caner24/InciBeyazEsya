
using IncıBeyazEsya.DataAcces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InciBeyazEsya.Entities.Concrate
{
    public class Districts : IEntity
    {
        public int Id { get; set; }
        public int TownsId { get; set; }
        public string District { get; set; }
    }
}
