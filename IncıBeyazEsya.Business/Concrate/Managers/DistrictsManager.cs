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
    public class DistrictsManager :IDistrictsService
    {
        private INorthwindContext _northwindContext;
        public DistrictsManager(INorthwindContext context)
        {
            _northwindContext = context;
        }

        SqlCommand _sqlCommand;
        public Districts Add(Districts entity)
        {
            _sqlCommand = new SqlCommand("INSERT INTO Districts (TownsId,District)values(@p1,@p2)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.TownsId);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.District.ToString());
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public List<Districts> GetAll(string filter = null)
        {
            List<Districts> myList = new List<Districts>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Districts", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Districts { TownsId = (int)da["Payments"], District = da["District"].ToString() });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from Districts where District=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Districts { TownsId = (int)da["TownsId"], District = da["District"].ToString() });
                }
            }

            return myList.ToList();
        }

        public Districts GetById(int id)
        {
            Districts entities = new Districts();
            _sqlCommand = new SqlCommand("Select * from Districts where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Districts {Id= (int)da["Id"], TownsId = (int)da["TownsId"], District = da["District"].ToString() };
            }
            return entities;
        }

        public Districts Update(Districts entity)
        {
            _sqlCommand = new SqlCommand("Update Districts set TownsId=@p1,District=@p2 where Id=@p3", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.TownsId);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.District);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public Districts GetByName(string name)
        {
            Districts entities = new Districts();
            _sqlCommand = new SqlCommand("Select * from Districts where District=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", name);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Districts {Id= (int)da["Id"], TownsId = (int)da["TownsId"], District = da["District"].ToString() };
            }
            return entities;
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from Districts where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }
    }
}
