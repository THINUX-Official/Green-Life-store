using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class ManageCustomersForm : BaseForm
    {

        private AdminDashboardForm adminDashboardForm;

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        public ManageCustomersForm(AdminDashboardForm adminDashboardForm)
        {
            InitializeComponent();
            this.adminDashboardForm = adminDashboardForm;
        }

        private void LoadCustomers()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT customer_id, name, email, phone, address from customers WHERE active_status = 1";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    connection.Open();
                    adapter.Fill(dt);

                    dgvCustomers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error Loading Customers: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ManageCustomersForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            adminDashboardForm.Show();
            this.Hide();
        }

        private void ManageCustomersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to remove this customer?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No) return;

            int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["customer_id"].Value);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"
                    UPDATE customers 
                    SET active_status = 0, 
                    modified_date = NOW() 
                    WHERE customer_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", customerId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            LoadCustomers();
            MessageBox.Show("Customer removed successfully.");
        }
    }
}
