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
    public partial class Add_Dependent : Form
    {
        #region General Declarations
        BusinessClass bc = new BusinessClass();
        #endregion

        #region InitializeComponent()
        public Add_Dependent()
        {
            InitializeComponent();
        }
        #endregion

        #region Form_Load
        private void Add_Dependent_Load(object sender, EventArgs e)
        {
            string conStr = bc.getConnection();
            if (!conStr.ToLower().Equals("true"))
            {
                MessageBox.Show(conStr, "Error occured!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                this.Dispose();
            }
            loadEmployees();
        }
        #endregion

        #region loadEmployees
        private void loadEmployees()
        {
            try
            {
                DataSet ds = bc.getEmployeeIdsNames();
                if (ds.Tables.Count == 0 || ds.Tables["Employees"].Rows.Count == 0)
                {
                    throw new Exception("Empty Dataset! Program couldn't get Employee details for adding a new Dependant.");
                }
                if (cboEmployee.Items.Count > 1)
                {
                    cboEmployee.Items.Clear();
                }
                for (int i = 0; i < ds.Tables["Employees"].Rows.Count; ++i)
                {
                    cboEmployee.Items.Add(ds.Tables["Employees"].Rows[i][0].ToString() + " - " + ds.Tables["Employees"].Rows[i][1].ToString());
                }
                
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occurred!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                this.Dispose();
            }
        }
        #endregion

        #region btnSave
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.ToString();
                string sur = txtSur.Text.ToString();
                string id = txtID.Text.ToString();
                int gender = cboGender.SelectedIndex;
                int empId = Convert.ToInt16(cboEmployee.Text.ToString().Substring(0, 4));
                string res = bc.addDependent(name.Trim(), sur.Trim(), id.Trim(), gender, empId);

                if (res.ToLower().Equals("true"))
                {
                    MessageBox.Show("Dependent added successfully!", "Transaction Succeeded!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception(res);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error ocurred", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}