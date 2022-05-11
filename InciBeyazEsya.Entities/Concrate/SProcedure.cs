using IncıBeyazEsya.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InciBeyazEsya.Entities.Concrate
{
    public class SProcedure:IEntity
    {
        public int ProductId { get; set; }
        public int OrdersId { get; set; }
        public int AdressId { get; set; }
        public int CityId { get; set; }
        public int DistrictsId { get; set; }
        public int TownsId { get; set; }
        public int UserId { get; set; }
        public int OrderDetailId { get; set; }
        public int InvoicesId { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime Date_ { get; set; }
        public string NameSurname { get; set; }
        public string ProcName { get; set; }
        public string Amount { get; set; }
    }
}
