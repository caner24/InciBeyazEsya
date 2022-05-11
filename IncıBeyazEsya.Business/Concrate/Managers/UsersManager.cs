using IncıBeyazEsya.DataAcces.Abstract;
using IncıBeyazEsya.Business.Abstract;
using InciBeyazEsya.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data;

namespace IncıBeyazEsya.Business.Concrate.Managers
{
    public class UsersManager : IUsersService
    {
        private INorthwindContext _northwindContext;

        public UsersManager(INorthwindContext context)
        {
            _northwindContext = context;
        }
        SqlCommand _sqlCommand;
        public User Add(User entity)
        {
            _sqlCommand = new SqlCommand("INSERT INTO Users (NameSurname,Email,Gender,BirthDate,CreateDate,TelNumber1,TelNumber2)values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.NameSurname.ToString());
            _sqlCommand.Parameters.AddWithValue("@p2", entity.Email.ToString());
            _sqlCommand.Parameters.AddWithValue("@p3", entity.Gender.ToString());
            _sqlCommand.Parameters.Add("@p4", SqlDbType.Date).Value = entity.BırthDate;
            _sqlCommand.Parameters.Add("@p5", SqlDbType.Date).Value = entity.CreateDate;
            _sqlCommand.Parameters.AddWithValue("@p6", entity.TelNumber1);
            _sqlCommand.Parameters.AddWithValue("@p7", entity.TelNumber2);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public List<User> GetAll(string filter = null)
        {
            List<User> myList = new List<User>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Users", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new User { NameSurname = da["NameSurname"].ToString(), Email = da["Email"].ToString(), Gender = da["Gender"].ToString(), BırthDate = (DateTime)da["BırthDate"], CreateDate = (DateTime)da["CreateDate"], TelNumber1 = da["TelNumber1"].ToString(), TelNumber2 = da["TelNumber2"].ToString() });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from Users where NameSurname=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new User { NameSurname = da["NameSurname"].ToString(), Email = da["Email"].ToString(), Gender = da["Gender"].ToString(), BırthDate = (DateTime)da["BırthDate"], CreateDate = (DateTime)da["CreateDate"], TelNumber1 = da["TelNumber1"].ToString(), TelNumber2 = da["TelNumber2"].ToString() });
                }
            }

            return myList.ToList();
        }

        public User GetById(int id)
        {
            User entities = new User();
            _sqlCommand = new SqlCommand("Select * from Users where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = (new User { Id = (int)da["Id"], NameSurname = da["NameSurname"].ToString(), Email = da["Email"].ToString(), Gender = da["Gender"].ToString(), BırthDate = (DateTime)da["BirthDate"], CreateDate = (DateTime)da["CreateDate"], TelNumber1 = da["TelNumber1"].ToString(), TelNumber2 = da["TelNumber2"].ToString() });
            }
            return entities;
        }

        public User Update(User entity)
        {
            _sqlCommand = new SqlCommand("Update Users set NameSurname=@p1,Email=@p2,Gender=@p3,BirthDate=@p4,CreateDate=@p5,TelNumber1=@p6,TelNumber2=@p7 where Id=@p8", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.NameSurname);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.Email);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.Gender);
            _sqlCommand.Parameters.Add("@p4", SqlDbType.Date).Value = entity.BırthDate;
            _sqlCommand.Parameters.AddWithValue("@p5", entity.CreateDate);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.TelNumber1);
            _sqlCommand.Parameters.AddWithValue("@p7", entity.TelNumber2);
            _sqlCommand.Parameters.AddWithValue("@p8", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public User GetByName(string name)
        {
            User entities = new User();
            _sqlCommand = new SqlCommand("Select * from Users where Email=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", name);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new User { Id = (int)da["Id"], NameSurname = da["NameSurname"].ToString(), Email = da["Email"].ToString(), Gender = da["Gender"].ToString(), BırthDate = (DateTime)da["BirthDate"], CreateDate = (DateTime)da["CreateDate"], TelNumber1 = da["TelNumber1"].ToString(), TelNumber2 = da["TelNumber2"].ToString() };
            }
            return entities;
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from Users where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }
    }
}
