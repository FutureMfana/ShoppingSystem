﻿using System;
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
    public partial class Employee_Registration : Form
    {
        #region General Declarations
        BusinessClass bclass = new BusinessClass();
        #endregion

        #region InitializeComponent()
        public Employee_Registration()
        {
            InitializeComponent();
        }
        #endregion

        #region Form_Load()
        private void Employee_Registration_Load(object sender, EventArgs e)
        {
            try
            {
                string con = bclass.getConnection();
                if (!con.ToLower().Equals("true"))
                {
                    MessageBox.Show(con, "Connection Failed!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    return;
                }
                loadManagers();
                cboGender.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error occured!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region loadManagers()
        private void loadManagers()
        {
            try
            {
                DataSet dsEmps = bclass.getEmployeeIdsNames();
                if (dsEmps.Tables.Count == 0 || dsEmps.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("No tables found in the DataSet object or  or table got no rows");
                }
                if (cboManager.Items.Count > 1)
                {
                    cboManager.Items.Clear();
                }
                cboGender.SelectedIndex = 0;
                //MessageBox.Show(dsEmps.Tables["Employees"].Rows[0][0].ToString());
                for (int i = 0; i < dsEmps.Tables["Employees"].Rows.Count; ++i)
                {
                    cboManager.Items.Add(dsEmps.Tables["Employees"].Rows[i][0] + " - " + dsEmps.Tables["Employees"].Rows[i][1].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error occured!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                return;
            }
        }
        #endregion

        #region btnSubmit
        private void btnSumit_Click(object sender, EventArgs e)
        {
            try
            {
                int manager = Convert.ToInt16(cboManager.SelectedItem.ToString().Substring(0,4));
                string name = txtName.Text.ToString();
                string sur = txtSur.Text.ToString();
                double salary = Convert.ToDouble(txtSalary.Text);
                string id = txtIDNumber.Text.ToString();
                
                string managerID = cboManager.SelectedText.ToString().Substring(0, cboManager.SelectedText.ToString().IndexOf("-") + 1);
                string position = txtPosition.Text.ToString();

                int gender = cboGender.SelectedIndex -1;
                string res = bclass.addEmployee(name, sur, id, gender, position, salary, manager);
                if (res.ToLower().Equals("true"))
                {
                    MessageBox.Show("Employee added succesfully", "Transaction executed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }else
                {
                    throw new Exception(res);
                }
                cboManager.Items.Clear();
                loadManagers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error occured!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                return;
            }
        }
        #endregion

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void lblUpdateEmployee_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Update_Employee frmUpdateEmp = new Update_Employee();
            frmUpdateEmp.Show();
            this.Hide();
        }
    }
}
