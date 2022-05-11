using InciBeyazEsya.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncıBeyazEsya.Business.ValidationRules.FluentValidation
{
    public class NotEmpty:Exception
    {
        public NotEmpty(string message):base(message)
        {
          
        }
    }
}
