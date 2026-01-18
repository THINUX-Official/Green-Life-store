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
    public partial class ProfileForm : BaseForm
    {

        private CustomerDashboardForm customerDashboardForm;
        public ProfileForm(CustomerDashboardForm customerDashboardForm)
        {
            InitializeComponent();
            this.customerDashboardForm = customerDashboardForm;
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            customerDashboardForm.Show();
            this.Hide();
        }

        private void ProfileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
