using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoppingSystem
{
    public partial class Update_Customer : Form
    {
        #region General Declarations
        BusinessClass bc = new BusinessClass();
        #endregion

        #region formInitiliazation
        public Update_Customer()
        {
            InitializeComponent();
        }
        #endregion

        #region Update_Customer_Load
        private void Update_Customer_Load(object sender, EventArgs e)
        {
            string con = bc.getConnection();
            if (!con.Equals("true"))
            {
                MessageBox.Show(con, "Error Occurred!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                this.Dispose();
            }
            LoadCustomers();
        }
        #endregion

        #region claerInputFieds
        private void clearInputFields()
        {
            txtEmail.Text = "";
            txtGender.Text = "";
            txtID.Text = "";
            txtName.Text = "";
            txtResAdd.Text = "";
            txtSur.Text = "";
        }
        #endregion

        #region LoadCustomers
        private void LoadCustomers()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = bc.getCustomerID();
                
                if (cboCustID.Items.Count > 0)
                {

                    cboCustID.Items.Clear();
                    /*for (int i = 0; i < cboCustID.Items.Count; ++i)
                    {
                        cboCustID.Items.RemoveAt(i);
                    }*/
                }
                
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("No customers found in the Database");
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count ; ++i)
                {
                    cboCustID.Items.Add(ds.Tables[0].Rows[i][0] + " - " + ds.Tables[0].Rows[i][1]);
                }

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Ocurred!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                this.Dispose();
            }
        }
        #endregion

        private void cboCustID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboCustID.SelectedIndex < 0)
                {
                    return;
                }
                
                int id = Convert.ToInt16(cboCustID.SelectedItem.ToString().Substring(0,4));

                DataSet ds = new DataSet();
                ds = bc.getCustomerById(id);

                //Displaying customer information
                txtName.Text = ds.Tables["Customers"].Rows[0]["FirstName"].ToString();
                txtSur.Text = ds.Tables["Customers"].Rows[0]["LastName"].ToString();
                txtID.Text = ds.Tables["Customers"].Rows[0]["IDNumber"].ToString();
                txtGender.Text = ds.Tables["Customers"].Rows[0]["Gender"].ToString();
                txtEmail.Text = ds.Tables["Customers"].Rows[0]["EmailAddress"].ToString();
                txtResAdd.Text = ds.Tables["Customers"].Rows[0]["ResidentialAddres"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Occurred!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.ToString().Trim();
                string sur = txtSur.Text.ToString().Trim();
                string email = txtEmail.Text.ToString().Trim();
                string idNumber = txtID.Text.ToString().Trim();
                string resAdd = txtResAdd.Text.ToString().Trim();
                int gender = 0;
                int id = Convert.ToInt16(cboCustID.SelectedItem.ToString().Substring(0, 4));

                if (txtGender.Text.ToString().Trim().ToLower().Equals("male"))
                {
                    gender = 1;
                }

                string updateCustomer = bc.updateCustomerByCustomerID(id, name, sur, gender, email, resAdd, idNumber);
                if (updateCustomer.Equals("true"))
                {
                    MessageBox.Show("Customer information is updated successfully!", "Transaction Completed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearInputFields();
                    LoadCustomers();
                }
                else
                {
                    throw new Exception();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Occured!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
