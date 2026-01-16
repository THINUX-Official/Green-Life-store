using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class AdminDashboardForm : BaseForm
    {

        private LoginForm loginForm;

        public AdminDashboardForm(LoginForm loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginForm.Show();
            this.Hide();
        }

        private void AdminDashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            ManageProductsForm manageProductsForm = new ManageProductsForm(this);
            manageProductsForm.Show();
            this.Hide();
        }

        private void btnManageCustomers_Click(object sender, EventArgs e)
        {
            ManageCustomersForm manageCustomersForm = new ManageCustomersForm(this); 
            manageCustomersForm.Show();
            this.Hide();
        }

        private void btnManageOrders_Click(object sender, EventArgs e)
        {
            ManageOrdersForm manageOrdersForm = new ManageOrdersForm(this);
            manageOrdersForm.Show();
            this.Hide();
        }
    }
}
