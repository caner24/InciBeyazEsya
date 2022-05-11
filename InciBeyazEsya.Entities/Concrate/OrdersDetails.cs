
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
    public class OrdersDetails:IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int Amount { get; set; }
        public double UnitPrice { get; set; }
        public double LineTotal { get; set; }
        public Guid orderDetailUniq { get; set; }
    }
}
