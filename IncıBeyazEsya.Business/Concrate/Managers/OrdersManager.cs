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
    public class OrdersManager : IOrdersService
    {
        private INorthwindContext _northwindContext;

        SqlCommand _sqlCommand;
        public OrdersManager(INorthwindContext context)
        {
            _northwindContext = context;
        }
        public Orders Add(Orders entity)
        {
            _sqlCommand = new SqlCommand("INSERT INTO Orders (UserId,Date_,TotalPrice,AddresId,OrderNo)values(@p1,@p2,@p3,@p5,@p6)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.UserId);
            _sqlCommand.Parameters.Add("@p2", System.Data.SqlDbType.Date).Value = entity.Date_;
            _sqlCommand.Parameters.AddWithValue("@p3", entity.TotalPrice);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.AddresId);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.OrderNo.ToString());
            _sqlCommand.ExecuteNonQuery();
            return entity;

        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from Orders where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }

        public List<Orders> GetAll(string filter = null)
        {
            List<Orders> myList = new List<Orders>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Orders", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Orders { UserId = (int)da["UserId"], Date_ = (DateTime)da["Date_"], TotalPrice = (double)da["TotalPrice"], Status_ = da["Status_"].ToString(), AddresId = (int)da["AddresId"] });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from Orders where Date_=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Orders { UserId = (int)da["UserId"], Date_ = (DateTime)da["Date_"], TotalPrice = (double)da["TotalPrice"], Status_ = da["Status_"].ToString(), AddresId = (int)da["AddresId"] });
                }
            }

            return myList.ToList();
        }

        public Orders GetById(int id)
        {
            Orders entities = new Orders();
            _sqlCommand = new SqlCommand("Select * from Orders where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Orders { Id = (int)da["Id"], UserId = (int)da["UserId"], Date_ = (DateTime)da["Date_"], TotalPrice = (double)da["TotalPrice"], Status_ = da["Status_"].ToString(), AddresId = (int)da["AddresId"] };
            }
            return entities;
        }

        public Orders GetByName(string name)
        {
            Orders entities = new Orders();
            _sqlCommand = new SqlCommand("Select * from Orders where OrderNo=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", name);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Orders { Id = (int)da["Id"], UserId = (int)da["UserId"], Date_ = (DateTime)da["Date_"], TotalPrice = (double)da["TotalPrice"], Status_ = da["Status_"].ToString(), AddresId = (int)da["AddresId"] };
            }
            return entities;
        }

        public Orders Update(Orders entity)
        {
            _sqlCommand = new SqlCommand("Update Orders set UserId=@p1,Date_=@p2,TotalPrice=@p3,Status_=@p4,AddresId=@p5 where Id=@p6", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.UserId);
            _sqlCommand.Parameters.Add("@p2", SqlDbType.Date).Value = entity.Date_;
            _sqlCommand.Parameters.AddWithValue("@p3", entity.TotalPrice);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.Status_);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.AddresId);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }
    }
}
