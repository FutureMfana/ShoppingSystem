using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ShoppingSystem
{
    class BusinessClass
    {
        SqlConnection sqlConn;
        SqlCommand sqlCmd;
        string sqlText;

        public string getConnection() {
            try
            {
                sqlConn = new SqlConnection("rovider=SQLNCLI11;Data Source=DESKTOP-G1E2CG2;Integrated Security=SSPI;Initial Catalog=ShopDB");
                sqlConn.Open();
                sqlConn.Close();

                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }


        }
    }
}
