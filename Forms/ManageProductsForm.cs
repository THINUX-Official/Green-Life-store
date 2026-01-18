using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class ManageProductsForm : BaseForm
    {

        private AdminDashboardForm adminDashboardForm;

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        public ManageProductsForm(AdminDashboardForm adminDashboardForm)
        {
            InitializeComponent();
            this.adminDashboardForm = adminDashboardForm;
        }

        public void LoadProducts()
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

                    dgvProducts.DataSource = dt;
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

        private void ManageProductsForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            adminDashboardForm.Show();
            this.Hide();
        }

        private void ManageProductsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm(this);
            addProductForm.Show();
            this.Hide();
        }
    }
}
