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
    public class InvoiceDetailManager : IInvoicesDetailService
    {
        private INorthwindContext _northwindContext;
        public InvoiceDetailManager(INorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        SqlCommand _sqlCommand;
        public InvoiceDetail Add(InvoiceDetail entity)
        {

            _sqlCommand = new SqlCommand("INSERT INTO InvoiceDetail (InvoiceId,OrderDetailId,ProductsId,Amount,UnitPrice,LineTotal)values(@p1,@p2,@p3,@p4,@p5,@p6)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.InvoiceId);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.OrderDetailId);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.ProductsId);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.Amount);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.UnitPrice);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.LineTotal);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public List<InvoiceDetail> GetAll(string filter = null)
        {
            List<InvoiceDetail> myList = new List<InvoiceDetail>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Payments", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new InvoiceDetail { InvoiceId = (int)da["InvoiceId"], OrderDetailId = (int)da["OrderDetailId"], ProductsId = (int)da["ProductsId"], Amount = (int)da["Amount"], UnitPrice = (double)da["UnitPrice"], LineTotal = (double)da["LineTotal"] });
                }
            }
            else
            {
                _sqlCommand = new SqlCommand("Select * from Payments where Date_=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter);
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new InvoiceDetail { InvoiceId = (int)da["InvoiceId"], OrderDetailId = (int)da["OrderDetailId"], ProductsId = (int)da["ProductsId"], Amount = (int)da["Amount"], UnitPrice = (double)da["UnitPrice"], LineTotal = (double)da["LineTotal"] });
                }
            }

            return myList.ToList();
        }

        public InvoiceDetail GetById(int id)
        {
            InvoiceDetail entities = new InvoiceDetail();
            _sqlCommand = new SqlCommand("Select * from InvoiceDetail where OrderDetailId=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new InvoiceDetail { Id = (int)da["Id"], InvoiceId = Convert.ToInt32(da["InvoiceId"]), OrderDetailId = Convert.ToInt32(da["OrderDetailId"]), ProductsId = Convert.ToInt32(da["ProductsId"]), Amount = Convert.ToInt32(da["Amount"]), UnitPrice = Convert.ToDouble(da["UnitPrice"]), LineTotal = Convert.ToDouble(da["LineTotal"]) };
            }
            return entities;
        }

        public InvoiceDetail Update(InvoiceDetail entity)
        {
            _sqlCommand = new SqlCommand("Update InvoiceDetail set InvoiceId=@p1,OrderDetailId=@p2,ProductsId=@p3,Amount=@p4,UnitPrice=@p5,LineTotal=@p6 where Id=@p7", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.InvoiceId);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.OrderDetailId);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.ProductsId);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.Amount);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.UnitPrice);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.LineTotal);
            _sqlCommand.Parameters.AddWithValue("@p7", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public InvoiceDetail GetByName(string name)
        {
            InvoiceDetail entities = new InvoiceDetail();
            _sqlCommand = new SqlCommand("Select * from InvoiceDetail where OrderNo=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", name);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = entities = new InvoiceDetail { Id = (int)da["Id"], InvoiceId = (int)da["InvoiceId"], OrderDetailId = (int)da["OrderDetailId"], ProductsId = (int)da["ProductsId"], Amount = (int)da["Amount"], UnitPrice = (double)da["UnitPrice"], LineTotal = (double)da["LineTotal"] };
            }
            return entities;
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from InvoiceDetail where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }
    }
}
