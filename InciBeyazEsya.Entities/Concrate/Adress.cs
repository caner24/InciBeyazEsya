
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
    public class Adress : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CıtyId { get; set; }
        public int TownsId { get; set; }
        public int DistrictsId { get; set; }
        public int PostalCode { get; set; }
        public string AdressText { get; set; }
    }
}
