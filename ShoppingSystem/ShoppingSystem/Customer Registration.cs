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
    public partial class Form1 : Form
    {
        BusinessClass bclass = new BusinessClass();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string con = bclass.getConnection();
            if (!con.ToLower().Equals("true"))
            {
                MessageBox.Show(con, "Connection Failed!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            Employee_Registration formEmp = new Employee_Registration();
            formEmp.ShowDialog();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string addCust;
            string name = txtName.Text.Trim();
            string sur = txtSur.Text.Trim();
            string id = txtIDNumber.Text.Trim();
            int gender = cboGender.SelectedIndex;
            string email = txtEmailAddress.Text.Trim();
            string resAdd = txtResAddress.Text.Trim();

            addCust = bclass.addCustomer(name, sur, id, gender, email, resAdd);
            if (addCust.ToLower().Equals("true"))
            {
                MessageBox.Show("Trasaction executed successfully", "Record Added!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
            {
                MessageBox.Show(addCust, "Transaction Falied!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
