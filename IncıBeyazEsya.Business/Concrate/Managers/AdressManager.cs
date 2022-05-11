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
    public class AdressManager : IAdressService
    {
        private INorthwindContext _northwindContext;

        public AdressManager(INorthwindContext context)
        {
            _northwindContext = context;
        }

        SqlCommand _sqlCommand;
        public Adress Add(Adress entity)
        {
            _sqlCommand = new SqlCommand("INSERT INTO Adress (UserId,CityId,TownsId,DistrictsId,PostalCode,AdressText)values(@p1,@p2,@p3,@p4,@p5,@p6)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.UserId);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.CıtyId);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.TownsId);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.DistrictsId);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.PostalCode);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.AdressText.ToString());
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public List<Adress> GetAll(string filter = null)
        {
            List<Adress> myList = new List<Adress>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Adress", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Adress { UserId = (int)da["UserId"], CıtyId = (int)da["PaymentType"], TownsId = (int)da["PaymentTotal"], DistrictsId = (int)da["DistrictsId"], PostalCode = (int)da["PostalCode"], AdressText = da["AdressText"].ToString() });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from Adress where Date_=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Adress { UserId = (int)da["UserId"], CıtyId = (int)da["PaymentType"], TownsId = (int)da["PaymentTotal"], DistrictsId = (int)da["DistrictsId"], PostalCode =Convert.ToInt32(da["PostalCode"]), AdressText = da["AdressText"].ToString() });
                }
            }

            return myList.ToList();
        }

        public Adress GetById(int id)
        {
            Adress entities = new Adress();
            _sqlCommand = new SqlCommand("Select * from Adress where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Adress { Id = (int)da["Id"], UserId = (int)da["UserId"], CıtyId = (int)da["CityId"], TownsId = (int)da["TownsId"], DistrictsId = (int)da["DistrictsId"], PostalCode = Convert.ToInt32(da["PostalCode"]), AdressText = da["AdressText"].ToString() };
            }
            return entities;
        }
         
        public Adress Update(Adress entity)
        {
            _sqlCommand = new SqlCommand("Update Adress set UserId=@p1,CityId=@p3,TownsId=@p4,DistrictsId=@p5,PostalCode=@p6,AdressText=@p7 where Id=@p8", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.UserId);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.CıtyId);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.TownsId);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.DistrictsId);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.PostalCode);
            _sqlCommand.Parameters.AddWithValue("@p7", entity.AdressText);
            _sqlCommand.Parameters.AddWithValue("@p8", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public Adress GetByName(string name)
        {
            Adress entities = new Adress();
            _sqlCommand = new SqlCommand("Select * from Adress where PostalCode=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", name);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Adress { Id = (int)da["Id"], UserId = (int)da["UserId"], CıtyId = (int)da["CityId"], TownsId = (int)da["TownsId"], DistrictsId = (int)da["DistrictsId"], PostalCode = Convert.ToInt32(da["PostalCode"]), AdressText = da["AdressText"].ToString() };
            }
            return entities;
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from Adress where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }
    }
}
