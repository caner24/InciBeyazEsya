using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncıBeyazEsya.DataAcces.Abstract
{
    public interface IProcedureService<T> where T : class, IEntity
    {
        T sqlCommand(string parameter);
    }
}
