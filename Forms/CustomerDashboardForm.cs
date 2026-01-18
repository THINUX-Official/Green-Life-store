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
    public partial class CustomerDashboardForm : BaseForm
    {
        private LoginForm loginForm;
        private int customerId;
        public CustomerDashboardForm(LoginForm loginForm, int customerId)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            this.customerId = customerId;
        }

        private void CustomerDashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginForm.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm(this);
            profileForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void CustomerDashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnMyOrders_Click(object sender, EventArgs e)
        {
            CustomerOrdersForm customerOrdersForm = new CustomerOrdersForm(this);
            customerOrdersForm.Show();
            this.Hide();
        }
    }
}
