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
    public partial class CustomerOrdersForm : BaseForm
    {

        private CustomerDashboardForm customerDashboardForm;
        public CustomerOrdersForm(CustomerDashboardForm customerDashboardForm)
        {
            InitializeComponent();
            this.customerDashboardForm = customerDashboardForm;
        }

        private void CustomerOrdersForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            customerDashboardForm.Show();
            this.Hide();
        }

        private void CustomerOrdersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }
    }
}
