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
                sqlConn = new SqlConnection("Data Source=DESKTOP-G1E2CG2;Initial Catalog=ShopDB;Integrated Security=True");
                sqlConn.Open();
                sqlConn.Close();

                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string addEmployee(string name, string sur, string id, DateTime dob, int gender, string position, int managerId = 0, double salary) {
            try
            {
                if (managerId != 0)
                {
                    sqlText = "INSERT INTO Staff.Employees (FirstName, LastName, IDNumber, DOB, Gender, Position, ManagerID, Salary) ";
                    sqlText += "VALUES('" + name + "','" + sur + "','" + id + "','" + dob + "'," + gender + ",'" + position + "'," + managerId + "," + salary + ")";
                }else
                {
                    sqlText = "INSERT INTO Staff.Employees (FirstName, LastName, IDNumber, DOB, Gender, Position, Salary) ";
                    sqlText += "VALUES('" + name + "','" + sur + "','" + id + "','" + dob + "'," + gender + ",'" + position + "'," + salary + ")";
                }

                if (sqlConn.State == System.Data.ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                sqlCmd = new SqlCommand(sqlText, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();

                if (sqlConn.State == ConnectionState.Open) {
                    sqlConn.Close();
                }
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
                sqlText = "INSERT INTO Customer.Customers (FirstName, LastName, IDNumber, Gender, EmailAddress, ResidentialAddres) ";
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
