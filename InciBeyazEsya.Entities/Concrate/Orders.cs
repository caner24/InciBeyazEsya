
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
    public class Orders : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public DateTime Date_ { get; set; }
        public double TotalPrice { get; set; }

        public string Status_ { get; set; }
        public int AddresId { get; set; }

        public Guid OrderNo { get; set; }

    }
}
