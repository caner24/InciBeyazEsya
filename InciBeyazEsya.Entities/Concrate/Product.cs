
using IncıBeyazEsya.DataAcces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InciBeyazEsya.Entities.Concrate
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string ProductCodes { get; set; }
        public double UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string ProductInfo { get; set; }
        public string ProductAbility { get; set; }
        public string ProductColor { get; set; }
        public string Category { get; set; }
        public byte[] ProductImage1 { get; set; }
        public byte[] ProductImage2 { get; set; }
        public byte[] ProductImage3 { get; set; }
        public string Marka { get; set; }
        public int Amount { get; set; }
    }
}
