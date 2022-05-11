using IncıBeyazEsya.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IncıBeyazEsya.Business.Abstract
{
    public interface ISqlService<T> where T : class, IEntity
    {
        T Add(T entity);
        List<T> GetAll(string filter = null);
        T GetById(int id);
        T GetByName(string name);
        T Update(T entity);
        void Delete(int entity);
    }
}
