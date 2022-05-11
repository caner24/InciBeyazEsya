using IncıBeyazEsya.Business.Abstract;
using IncıBeyazEsya.DataAcces.Abstract;
using InciBeyazEsya.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IncıBeyazEsya.Business.Concrate.Managers
{
    public class InvoicesManager : IInvoicesService
    {
        private INorthwindContext _northwindContext;
        public InvoicesManager(INorthwindContext context)
        {
            _northwindContext = context;
        }
        SqlCommand _sqlCommand;
        public Invoices Add(Invoices entity)
        {

            _sqlCommand = new SqlCommand("INSERT INTO Invoices (OrderId,Date_,AdressId,CargoFıchneNo,TotalPrice)values(@p1,@p2,@p3,@p4,@p5)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.OrderId);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.Date_);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.AdressId);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.CargoFıchneNo);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.TotalPrice);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public List<Invoices> GetAll(string filter = null)
        {
            List<Invoices> myList = new List<Invoices>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Invoices", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Invoices { OrderId = (int)da["Payments"], Date_ = (DateTime)da["Date_"], AdressId = (int)da["AdressId"], CargoFıchneNo = da["CargoFıchneNo"].ToString(), TotalPrice = (double)da["TotalPrice"] });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from Payments where Date_=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Invoices { OrderId = (int)da["Payments"], Date_ = (DateTime)da["Date_"], AdressId = (int)da["AdressId"], CargoFıchneNo = da["CargoFıchneNo"].ToString(), TotalPrice = (double)da["TotalPrice"] });
                }
            }

            return myList.ToList();
        }

        public Invoices GetById(int id)
        {
            Invoices entities = new Invoices();
            _sqlCommand = new SqlCommand("Select * from Invoices where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Invoices { Id = (int)da["Id"], OrderId = (int)da["OrderId"], Date_ = (DateTime)da["Date_"], AdressId = (int)da["AdressId"], CargoFıchneNo = da["CargoFıchneNo"].ToString(), TotalPrice = (double)da["TotalPrice"] };
            }
            return entities;
        }

        public Invoices Update(Invoices entity)
        {
            _sqlCommand = new SqlCommand("Update Invoices set OrderId=@p1,Date_=@p2,AdressId=@p3,CargoFıchneNo=@p4,TotalPrice=@p5 where Id=@p6", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.OrderId);
            _sqlCommand.Parameters.Add("@p2", SqlDbType.Date).Value = entity.Date_;
            _sqlCommand.Parameters.AddWithValue("@p3", entity.AdressId);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.CargoFıchneNo);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.TotalPrice);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public Invoices GetByName(string name)
        {
            Invoices entities = new Invoices();
            _sqlCommand = new SqlCommand("Select * from Invoices where CargoFıchneNo=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", name);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = entities = new Invoices {Id= (int)da["Id"], OrderId = (int)da["OrderId"], Date_ = (DateTime)da["Date_"], AdressId = (int)da["AdressId"], CargoFıchneNo = da["CargoFıchneNo"].ToString(), TotalPrice = (double)da["TotalPrice"] };
            }
            return entities;
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from Invoices where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }
    }
}
