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
                sqlConn = new SqlConnection("Provider=SQLNCLI11;Data Source=DESKTOP-G1E2CG2;Integrated Security=SSPI;Initial Catalog=ShopDB");
                sqlConn.Open();
                sqlConn.Close();

                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string addCustomer(string name, string sur, string id, int gender,string email, string resAdd) {
            try
            {
                sqlText = "INSERT INTO Customer.Customers (FirstName, LastName, IDNumber, Gender, EmailAddress, ResidentialAddress) ";
                sqlText = sqlText + "VALUES('" + name + "','" + sur + "','" + id + "'," + gender + ",'" + email + "','" + resAdd + "')";
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                sqlCmd = new SqlCommand(sqlText, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();

                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
