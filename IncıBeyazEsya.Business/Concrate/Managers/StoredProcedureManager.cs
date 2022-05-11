using IncıBeyazEsya.Business.Abstract;
using IncıBeyazEsya.DataAcces.Abstract;
using InciBeyazEsya.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncıBeyazEsya.Business.Concrate.Managers
{
    public class StoredProcedureManager : IStoredProcedureService
    {
        private INorthwindContext _northwindContext;
        public StoredProcedureManager(INorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        public SProcedure sqlCommand(string parameter)
        {
            SProcedure procedures = new SProcedure();
            SqlCommand cmd = new SqlCommand("GetDetailsFiecheNo", _northwindContext.GetConnection());
            SqlDataReader oku;

            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@Fieche", SqlDbType.VarChar,200);
            param.Value =parameter.ToString();
            oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                procedures = new SProcedure { ProductId = Convert.ToInt32(oku[0].ToString()), OrdersId = Convert.ToInt32(oku[1].ToString()), AdressId = Convert.ToInt32(oku[2].ToString()), CityId = Convert.ToInt32(oku[3].ToString()), DistrictsId = Convert.ToInt32(oku[4].ToString()), TownsId = Convert.ToInt32(oku[5].ToString()), UserId = Convert.ToInt32(oku[6].ToString()), OrderDetailId = Convert.ToInt32(oku[7].ToString()), InvoicesId = Convert.ToInt32(oku[8].ToString()), TotalPrice = Convert.ToDouble(oku[9].ToString()), Status = oku[10].ToString(), Date_ = Convert.ToDateTime(oku[11].ToString()), NameSurname = oku[12].ToString(), ProcName = oku[13].ToString(), Amount = oku[14].ToString() };
            }
            return procedures;
        }
    }
}
