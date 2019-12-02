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
        #region global declarations
        SqlDataAdapter da;
        SqlConnection sqlConn;
        SqlCommand sqlCmd;
        string sqlText;
        #endregion

        #region Connection function
        public string getConnection()
        {
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
        #endregion

        #region getManagers
        public DataSet getManagers()
        {
            DataSet dsEmps = new DataSet();
            try
            {
                sqlText = "SELECT EmpID, FirstName + ' ' + LastName AS FullName FROM Staff.Employees WHERE Position <> 'General Worker' OR Position <> 'Student'";
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                da = new SqlDataAdapter(sqlText, sqlConn);
                da.Fill(dsEmps, "Employees");
                da.Dispose();

                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
            catch
            {
            }
            return dsEmps;
        }
        #endregion

        #region getCustomerID
        public DataSet getCustomerID()
        {
            DataSet ds = new DataSet();
            try
            {
                sqlText = "SELECT CustomerID, FullName FROM Customer.AllCustomers";
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                da = new SqlDataAdapter(sqlText, sqlConn);
                da.Fill(ds, "CustIDs");
                da.Dispose();

                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
            catch
            {

            }
            return ds;
           
        }
        #endregion

        #region getAllCustomers
        public DataSet getCustomerById(int custID)
        {
            DataSet ds = new DataSet();
            try
            {
                sqlText = "SELECT FirstName, LastName, IDNumber, IIF(Gender = 1, 'Male', 'Female') AS Gender, EmailAddress, ResidentialAddres ";
                sqlText += "FROM Customer.Customers WHERE CustID = " + custID;

                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                da = new SqlDataAdapter(sqlText, sqlConn);
                da.Fill(ds, "Customers");
                da.Dispose();

                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
            catch { }
            return ds;
        }
        #endregion

        #region getEmployeeIdsName
        public DataSet getEmployeeIdsNames()
        {
            DataSet dsEmpIdsNames = new DataSet();
            try
            {
                sqlText = "SELECT EmpID, FirstName + ' ' + LastName AS FullName FROM Staff.Employees";
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                da = new SqlDataAdapter(sqlText, sqlConn);
                da.Fill(dsEmpIdsNames, "Employees");
                da.Dispose();
                
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
            catch{ /*EXCEPTION*/ }
            return dsEmpIdsNames;
        }

        #endregion

        #region addDependent
        public string addDependent(string name, string sur, string id, int gener, int empID)
        {
            try
            {
                sqlText = "INSERT INTO Staff.Dependents (FirstName, LastName, IDNumber, Gender, EmpID) ";
                sqlText += "VALUES ('" + name + "','" + sur + "','" + id + "'," + gener + "," + empID + ")";
                if(sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                sqlCmd = new SqlCommand(sqlText, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();

                if(sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
                return "true";
            }catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        #endregion

        #region addEmployee
        public string addEmployee(string name, string sur, string id, int gender, string position, double salary, int managerId = 0)
        {
            try
            {
                if (managerId != 0)
                {
                    sqlText = "INSERT INTO Staff.Employees (FirstName, LastName, IDNumber, Gender, Position, ManagerID, Salary) ";
                    sqlText += "VALUES('" + name + "','" + sur + "','" + id + "'," + gender + ",'" + position + "'," + managerId + "," + salary + ")";
                }
                else
                {
                    sqlText = "INSERT INTO Staff.Employees (FirstName, LastName, IDNumber, Gender, Position, Salary) ";
                    sqlText += "VALUES('" + name + "','" + sur + "','" + id + "'," + gender + ",'" + position + "'," + salary + ")";
                }

                if (sqlConn.State == System.Data.ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                sqlCmd = new SqlCommand(sqlText, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();

                if (sqlConn.State == ConnectionState.Open)
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
        #endregion

        #region addCustomer
        public string addCustomer(string name, string sur, string id, int gender, string email, string resAdd)
        {
            try
            {
                sqlText = "INSERT INTO Customer.Customers (FirstName, LastName, IDNumber, Gender, EmailAddress, ResidentialAddres) ";
                sqlText = sqlText + "VALUES('" + name + "','" + sur + "','" + id + "'," + gender + ",'" + email + "','" + resAdd + "')";
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                sqlCmd = new SqlCommand(sqlText, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();

                if (sqlConn.State == ConnectionState.Open)
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
        #endregion

        #region Update Customer
        public string updateCustomerByCustomerID(int custID, string name, string sur, int gender, string email, string resAdd, string id) {
            try
            {
                if (isID(id) == false) {
                    throw new Exception("Incorrect ID Number");
                }
                sqlText = "UPDATE Customer.Customers SET FirstName = '" + name + "', LastName = '" + sur + "', IDNumber = '" + id + "', Gender = "+ gender + ", EmailAddress = '" + email +"', ResidentialAddres = '" + resAdd + "' WHERE CustID = " + 5001;
                
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                sqlCmd = new SqlCommand(sqlText, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();

                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
                return "true";
            }catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        #endregion

        #region ID Number Valitaion
        public bool isID(string id)
        {
            try
            {
                //removes every spaces maybe contained in the input
                id = id.Replace(" ", "");
                if (id.Length != 13)
                {
                    return false;
                }
                else
                {
                    //check if id contains only digits
                    long j = Convert.ToInt64(id);
                }

                //ensuring no date (day) is above 31 and month above 12
                if (Convert.ToInt16(id.Substring(4, 2)) > 31 || Convert.ToInt16(id.Substring(2, 2)) > 12)
                {
                    return false;
                }

                //ensures that Jan, Mar, May, Jul, Aug, Oct, Dec are the only months can end on the 31st
                if ((Convert.ToInt16(id.Substring(2, 3)) == 02 || Convert.ToInt16(id.Substring(2, 3)) == 04 || Convert.ToInt16(id.Substring(2, 3)) == 06 ||
                    Convert.ToInt16(id.Substring(2, 3)) == 09 || Convert.ToInt16(id.Substring(2, 3)) == 11 && (Convert.ToInt16(id.Substring(4, 2)) >= 31)))
                {
                    return false;
                }

                //ensures that Feb can only ends on the 29th
                if (Convert.ToInt16(id.Substring(2, 2)) == 2 && (Convert.ToInt16(id.Substring(4, 2))) > 29)
                {
                    return false;
                }

                //if the date of birth is 29 Feb, check if it's a leap year
                if (Convert.ToInt16(id.Substring(2, 2)) == 2 && (Convert.ToInt16(id.Substring(4, 2))) == 29)
                {
                    int yy = Convert.ToInt16(id.Substring(0, 2));
                    DateTime currentDate = DateTime.Today;
                    int currentYear = Convert.ToInt16(((currentDate.ToString()).Substring(8, 2)));
                    if (yy > currentYear)
                    {
                        yy = Convert.ToInt16("19" + yy);
                    }
                    else
                    {
                        yy = Convert.ToInt16("20" + yy);
                    }
                    if (yy.ToString().Length < 4)
                    {
                        yy *= 10;
                    }
                    if (!(yy % 400 == 0 && yy % 4 == 0))
                    {
                        return false;
                    }
                }

                int sum = 0; bool even = false;
                for (int i = 0; i < 13; ++i)
                {
                    if (even == false)
                    {
                        sum += Convert.ToInt16(id.Substring(i, 1));
                    }
                    else
                    {
                        int x = Convert.ToInt16(id.Substring(i, 1)) * 2;
                        if (x.ToString().Length > 1)
                        {
                            sum += Convert.ToInt16(Convert.ToInt16(x.ToString().Substring(0, 1)) + Convert.ToInt16(x.ToString().Substring(1, 1)));
                        }
                        else
                        {
                            sum += x;
                        }
                    }
                    even = !even;
                }
                if (sum % 10 != 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region getDateOfBirth
        public string getDate(string id)
        {
            try
            {
                string dd = id.Substring(4, 2);
                string mm = id.Substring(2, 2);
                string yy = id.Substring(0, 2);
                DateTime date = DateTime.Today;
                string currYear = date.ToString().Substring(8, 2);
                if (Convert.ToInt16(yy) > Convert.ToInt16(currYear))
                {
                    yy = "19" + yy;
                }
                else
                {
                    yy = "20" + yy;
                }

                return yy + "-" + mm + "-" + dd;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        #endregion
    }
}