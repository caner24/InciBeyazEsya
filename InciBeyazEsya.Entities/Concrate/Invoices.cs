
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
    public class Invoices : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime Date_ { get; set; }
        public int AdressId { get; set; }
        public string CargoFıchneNo { get; set; }

        public double TotalPrice { get; set; }
    }
}
