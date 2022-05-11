
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
    public class Payments:IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentType { get; set; }
        public DateTime Date_ { get; set; }

        public string IsOk { get; set; }

        public double PaymentTotal { get; set; }
    }
}
