
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
    public class InvoiceDetail : IEntity
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public int OrderDetailId { get; set; }
        public int ProductsId { get; set; }
        public int Amount { get; set; }

        public double UnitPrice { get; set; }
        public double LineTotal { get; set; }

    }
}
