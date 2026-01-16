using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class ManageOrdersForm : BaseForm
    {

        private AdminDashboardForm adminDashboardForm;

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        public ManageOrdersForm(AdminDashboardForm adminDashboardForm)
        {
            InitializeComponent();
            this.adminDashboardForm = adminDashboardForm;
        }

        private void LoadOrders()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "select * from products";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    connection.Open();
                    adapter.Fill(dt);

                    dgvOrders.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error Loading Products: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ManageOrdersForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            adminDashboardForm.Show();
            this.Hide();
        }

        private void ManageOrdersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }
    }
}
