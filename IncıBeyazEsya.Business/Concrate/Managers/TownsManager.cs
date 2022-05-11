
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
    public class TownsManager :ITownsService
    {
        private INorthwindContext _northwindContext;
        public TownsManager(INorthwindContext context)
        {
            _northwindContext = context;
        }
        SqlCommand _sqlCommand;
        public Towns Add(Towns towns)
        {
            _sqlCommand = new SqlCommand("INSERT INTO Towns (Town,CityId)values(@p1,@p2)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", towns.Town);
            _sqlCommand.Parameters.AddWithValue("@p2", towns.CityId);
            _sqlCommand.ExecuteNonQuery();
            return towns;
        }

        public List<Towns> GetAll(string filter = null)
        {
            List<Towns> myList = new List<Towns>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Towns", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Towns { Id = (int)da["Id"], Town = da["Town"].ToString(), CityId = da["CityId"].ToString() });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from Towns where CityId=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Towns { Id = (int)da["Id"], Town = da["Town"].ToString(), CityId = da["CityId"].ToString() });
                }
            }

            return myList.ToList();


        }

        public Towns GetById(int id)
        {
            Towns entities = new Towns();
            _sqlCommand = new SqlCommand("Select * from Towns where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Towns { Id = (int)da["Id"], Town = da["Town"].ToString(), CityId = da["CityId"].ToString() };
            }
            return entities;
        }

        public Towns Update(Towns towns)
        {
            _sqlCommand = new SqlCommand("Update Towns set Town=@p1,CityId=@p2 where Id=@p3", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", towns.Town);
            _sqlCommand.Parameters.AddWithValue("@p2", towns.CityId);
            _sqlCommand.Parameters.AddWithValue("@p3", towns.Id);
            _sqlCommand.ExecuteNonQuery();
            return towns;
        }

        public Towns GetByName(string name)
        {
            Towns entities = new Towns();
            _sqlCommand = new SqlCommand("Select * from Towns where Town=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", name);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Towns { Id = (int)da["Id"], Town = da["Town"].ToString(), CityId = da["CityId"].ToString() };
            }
            return entities;
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from Towns where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }
    }
}
