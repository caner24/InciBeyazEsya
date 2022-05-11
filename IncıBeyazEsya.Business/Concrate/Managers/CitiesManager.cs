using IncıBeyazEsya.Business.Abstract;
using IncıBeyazEsya.DataAcces.Abstract;
using InciBeyazEsya.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IncıBeyazEsya.Business.Concrate.Managers
{
    public class CitiesManager : ICitiesService
    {
        private INorthwindContext _northwindContext;

        public CitiesManager(INorthwindContext context)
        {
            _northwindContext = context;
        }

        SqlCommand _sqlCommand;
        public Cities Add(Cities entity)
        {
            _sqlCommand = new SqlCommand("INSERT INTO Cities (City)values(@p1,@p2)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p2", entity.City);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public List<Cities> GetAll(string filter = null)
        {
            List<Cities> myList = new List<Cities>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Cities", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Cities {Id= (int)da["Id"],  City = da["City"].ToString() });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from Cities where CountryId=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.Add("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Cities { Id = (int)da["Id"], City = da["City"].ToString() });
                }
            }

            return myList.ToList();
        }

        public Cities GetById(int id)
        {
            Cities entities = new Cities();
            _sqlCommand = new SqlCommand("Select * from Cities where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Cities {Id= (int)da["Id"], City = da["City"].ToString() };
            }
            return entities;
        }

        public Cities Update(Cities entity)
        {
            _sqlCommand = new SqlCommand("Update Cities set City=@p2 where Id=@p3", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p2", entity.City);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public Cities GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from Cities where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }
    }
}
