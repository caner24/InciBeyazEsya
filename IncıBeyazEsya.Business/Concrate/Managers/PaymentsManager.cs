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
    public class PaymentsManager : IPaymentsService
    {
        private INorthwindContext _northwindContext;

        SqlCommand _sqlCommand;
        public PaymentsManager(INorthwindContext context)
        {
            _northwindContext = context;
        }
        public Payments Add(Payments entity)
        {
            _sqlCommand = new SqlCommand("INSERT INTO Payments (OrderId,Date_,PaymentType,PaymentTotal)values(@p1,@p2,@p3,@p5)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.OrderId);
            _sqlCommand.Parameters.Add("@p2", SqlDbType.Date).Value = entity.Date_;
            _sqlCommand.Parameters.AddWithValue("@p3", entity.PaymentType);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.PaymentTotal);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from Payments where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }

        public List<Payments> GetAll(string filter = null)
        {
            List<Payments> myList = new List<Payments>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Payments", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Payments { OrderId = (int)da["Payments"], Date_ = (DateTime)da["Date_"], PaymentType = da["PaymentType"].ToString(), PaymentTotal = (double)da["PaymentTotal"] });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from Payments where Date_=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Payments { OrderId = (int)da["Payments"], Date_ = (DateTime)da["Date_"], PaymentType = da["PaymentType"].ToString(), PaymentTotal = (double)da["PaymentTotal"] });
                }
            }

            return myList.ToList();
        }

        public Payments GetById(int id)
        {
            Payments entities = new Payments();
            _sqlCommand = new SqlCommand("Select * from Payments where OrderId=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Payments {Id= (int)da["Id"], IsOk = da["IsOk"].ToString(), OrderId = (int)da["OrderId"], Date_ = (DateTime)da["Date_"], PaymentType = da["PaymentType"].ToString(), PaymentTotal = (double)da["PaymentTotal"] };
            }
            return entities;
        }

        public Payments GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Payments Update(Payments entity)
        {
            _sqlCommand = new SqlCommand("Update Payments set OrderId=@p1,Date_=@p2,PaymentType=@p3,IsOk=@p4,PaymentTotal=@p5 where Id=@p6", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.OrderId);
            _sqlCommand.Parameters.Add("@p2", SqlDbType.Date).Value = entity.Date_;
            _sqlCommand.Parameters.AddWithValue("@p3", entity.PaymentType);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.IsOk);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.PaymentTotal);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }
    }
}
