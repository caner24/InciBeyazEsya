using IncıBeyazEsya.DataAcces.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncıBeyazEsya.DataAcces.Concrate
{
    public class NorthwindContext : INorthwindContext
    {
        public SqlConnection GetConnection()
        {
            SqlConnection myConnection = new SqlConnection("Data Source=CANER;Initial Catalog=InciBeyazEsya;User ID=myAdmin;Password=123456admin;");
            myConnection.Open();
            return myConnection;
        }

    }
}
