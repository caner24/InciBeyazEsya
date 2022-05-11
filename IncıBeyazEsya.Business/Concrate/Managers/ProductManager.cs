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
    public class ProductManager : IProductService
    {
        public INorthwindContext _northwindContext;
        public ProductManager(INorthwindContext context)
        {
            _northwindContext = context;
        }

        SqlCommand _sqlCommand;
        public Product Add(Product entity)
        {
            _sqlCommand = new SqlCommand("INSERT INTO Product (ProductCodes,UnitPrice,ProductName,Category,ProductColor,ProductImage1,ProductImage2,ProductImage3,ProductInfo,ProductAbility,Amount,Marka)values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity.ProductCodes);
            _sqlCommand.Parameters.AddWithValue("@p2", entity.UnitPrice);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.ProductName.ToString());
            _sqlCommand.Parameters.AddWithValue("@p4", entity.Category.ToString());
            _sqlCommand.Parameters.AddWithValue("@p5", entity.ProductColor.ToString());
            _sqlCommand.Parameters.Add("@p6", SqlDbType.Image, entity.ProductImage1.Length).Value = entity.ProductImage1;
            _sqlCommand.Parameters.Add("@p7", SqlDbType.Image, entity.ProductImage2.Length).Value = entity.ProductImage2;
            _sqlCommand.Parameters.Add("@p8", SqlDbType.Image, entity.ProductImage3.Length).Value = entity.ProductImage3;
            _sqlCommand.Parameters.AddWithValue("@p9", entity.ProductInfo.ToString());
            _sqlCommand.Parameters.AddWithValue("@p10", entity.ProductAbility.ToString());
            _sqlCommand.Parameters.AddWithValue("@p11", entity.Amount);
            _sqlCommand.Parameters.AddWithValue("@p12", entity.Marka);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public List<Product> GetAll(string filter = null)
        {
            List<Product> myList = new List<Product>();
            if (filter == null)
            {
                _sqlCommand = new SqlCommand("Select * from Product", _northwindContext.GetConnection());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Product {Marka=da["Marka"].ToString(), Id = (int)da["Id"], ProductCodes = da["ProductCodes"].ToString(), UnitPrice = (double)da["UnitPrice"], Category = da["Category"].ToString(), ProductImage1 = (byte[])da["ProductImage1"], ProductImage2 = (byte[])da["ProductImage2"], ProductImage3 = (byte[])da["ProductImage3"], ProductInfo = da["ProductInfo"].ToString(), ProductName = da["ProductName"].ToString(), ProductColor = da["ProductColor"].ToString(), ProductAbility = da["ProductAbility"].ToString(), Amount = Convert.ToInt32(da["Amount"]) });
                }
            }
            else
            {

                _sqlCommand = new SqlCommand("Select * from Product where ProductCodes=@p1", _northwindContext.GetConnection());
                _sqlCommand.Parameters.AddWithValue("@p1", filter.ToString());
                SqlDataReader da = _sqlCommand.ExecuteReader();
                while (da.Read())
                {
                    myList.Add(new Product { Marka = da["Marka"].ToString(), Id = (int)da["Id"], ProductCodes = da["ProductCodes"].ToString(), UnitPrice = (double)da["UnitPrice"], Category = da["Category"].ToString(), ProductImage1 = (byte[])da["ProductImage1"], ProductImage2 = (byte[])da["ProductImage2"], ProductImage3 = (byte[])da["ProductImage3"], ProductInfo = da["ProductInfo"].ToString(), ProductName = da["ProductName"].ToString(), ProductColor = da["ProductColor"].ToString(), ProductAbility = da["ProductAbility"].ToString(), Amount = (int)da["Amount"] });
                }
            }

            return myList.ToList();
        }
        public Product GetById(int id)
        {
            Product entities = new Product();
            _sqlCommand = new SqlCommand("Select * from Product where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", id);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Product { Id = (int)da["Id"],Amount= (int)da["Amount"],ProductAbility=da["ProductAbility"].ToString(), Marka =da["Marka"].ToString(), ProductCodes = da["ProductCodes"].ToString(), UnitPrice = (double)da["UnitPrice"], Category = da["Category"].ToString(), ProductImage1 = (byte[])da["ProductImage1"], ProductImage2 = (byte[])da["ProductImage2"], ProductImage3 = (byte[])da["ProductImage3"], ProductInfo = da["ProductInfo"].ToString(), ProductName = da["ProductName"].ToString(), ProductColor = da["ProductColor"].ToString() };
            }
            return entities;
        }

        public Product Update(Product entity)
        {
            _sqlCommand = new SqlCommand("Update Product set ProductAbility=@p1, UnitPrice=@p2,ProductName=@p3,Category=@p4,ProductImage1=@p5,ProductImage2=@p6,ProductImage3=@p7,ProductInfo=@p8,ProductColor=@p9,Marka=@p11,Amount=@p12 where Id=@p10", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p2", entity.UnitPrice);
            _sqlCommand.Parameters.AddWithValue("@p1", entity.ProductAbility);
            _sqlCommand.Parameters.AddWithValue("@p3", entity.ProductName);
            _sqlCommand.Parameters.AddWithValue("@p4", entity.Category);
            _sqlCommand.Parameters.AddWithValue("@p5", entity.ProductImage1);
            _sqlCommand.Parameters.AddWithValue("@p6", entity.ProductImage2);
            _sqlCommand.Parameters.AddWithValue("@p7", entity.ProductImage3);
            _sqlCommand.Parameters.AddWithValue("@p8", entity.ProductInfo);
            _sqlCommand.Parameters.AddWithValue("@p9", entity.ProductColor);
            _sqlCommand.Parameters.AddWithValue("@p11", entity.Marka);
            _sqlCommand.Parameters.AddWithValue("@p12", entity.Amount);
            _sqlCommand.Parameters.AddWithValue("@p10", entity.Id);
            _sqlCommand.ExecuteNonQuery();
            return entity;
        }

        public Product GetByName(string name)
        {
            Product entities = new Product();
            _sqlCommand = new SqlCommand("Select * from Product where ProductName=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", name);
            SqlDataReader da = _sqlCommand.ExecuteReader();
            while (da.Read())
            {
                entities = new Product {Marka=da["Marka"].ToString(), Id= (int)da["Id"], ProductCodes = da["ProductCodes"].ToString(), UnitPrice = (double)da["UnitPrice"], Category = da["Category"].ToString(), ProductImage1 = (byte[])da["ProductImage1"], ProductImage2 = (byte[])da["ProductImage2"], ProductImage3 = (byte[])da["ProductImage3"], ProductInfo = da["ProductInfo"].ToString(), ProductName = da["ProductName"].ToString(), ProductColor = da["ProductColor"].ToString() };
            }
            return entities;
        }

        public void Delete(int entity)
        {
            _sqlCommand = new SqlCommand("Delete from Product where Id=@p1", _northwindContext.GetConnection());
            _sqlCommand.Parameters.AddWithValue("@p1", entity);
            _sqlCommand.ExecuteNonQuery();
        }
    }
}
