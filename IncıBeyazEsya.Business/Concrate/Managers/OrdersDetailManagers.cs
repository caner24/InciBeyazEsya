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
    public class OrdersDetailManagers :IOrderDetailsService
    {
        private INorthwindContext _northwindContext;

        SqlCommand _sqlCommand;
        public OrdersDetailManagers(INorthwindContext context)
        {
            _northwindContext = context;
        }
        public OrdersDetails Add(OrdersDetails entity)
        {
            _sqlCommand = new SqlCommand("INSERT INTO OrdersDetails (OrderId,ProductId,Amount,UnitPrice,LineTotal,orderDetailUnique)values(@p1,@p2,@p3,@p4,@p5,@p6)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.OrderId);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.ProductId);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.Amount);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.UnitPrice);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.LineTotal);
            _sqlCommand.Parameters.AddWithValue("@p6",entity.orderDetailUniq);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from OrdersDetails where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }

        public List<OrdersDetails> GetAll(string filter = null)
        {
            List<OrdersDetails> myList = new List<OrdersDetails>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from OrdersDetails", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new OrdersDetails { Id = (int)da["Id"], OrderId = (int)da["Payments"], ProductId = (int)da["ProductId"], Amount = (int)da["ProductId"], UnitPrice = (double)da["PaymentTotal"], LineTotal= (double)da["LineTotal"] });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from OrdersDetails where Amount=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new OrdersDetails { OrderId = (int)da["Payments"], ProductId = (int)da["ProductId"], Amount = (int)da["Amount"], UnitPrice = (double)da["PaymentTotal"], LineTotal = (double)da["LineTotal"] });
                }
            }

            return myList.ToList();
        }

        public OrdersDetails GetById(int id)
        {
            OrdersDetails entities = new OrdersDetails();
            _sqlCommand = new SqlCommand("Select * from OrdersDetails where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new OrdersDetails { OrderId = (int)da["OrderId"], ProductId = (int)da["ProductId"], Amount = (int)da["Amount"], UnitPrice = (double)da["UnitPrice"], LineTotal = (double)da["LineTotal"] };
            }
            return entities;
        }

        public OrdersDetails GetByName(string name)
        {
            OrdersDetails entities = new OrdersDetails();
            _sqlCommand = new SqlCommand("Select * from OrdersDetails where orderDetailUnique=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", name);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new OrdersDetails {orderDetailUniq= (Guid)da["orderDetailUnique"] ,Id= (int)da["Id"], OrderId = (int)da["OrderId"], ProductId = (int)da["ProductId"], Amount = (int)da["Amount"], UnitPrice =(double)da["UnitPrice"], LineTotal =(double)da["LineTotal"] };
            }
            return entities;
        }

        public OrdersDetails Update(OrdersDetails entity)
        {
            _sqlCommand = new SqlCommand("Update OrdersDetails set OrderId=@p1,ProductId=@p2,Amount=@p3,UnitPrice=@p4,LineTotal=@p5 where Id=@p6", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.OrderId);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.ProductId);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.Amount);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.UnitPrice);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.LineTotal);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }
    }
}
