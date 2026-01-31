using System;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class AdminDashboardForm : BaseForm
    {

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";


        private LoginForm loginForm;

        public AdminDashboardForm(LoginForm loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            LoadDashboardStatistics();
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

        private void LoadDashboardStatistics()
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
                {
                    connection.Open();

                    // delivered sales count
                    string salesQuery = @"
                        SELECT IFNULL(SUM(total_amount), 0)
                        FROM orders
                        WHERE order_status = 'Delivered'";

                    using (var salesCmd = new MySql.Data.MySqlClient.MySqlCommand(salesQuery, connection))
                    {
                        lblSalesCount.Text = Convert.ToDecimal(salesCmd.ExecuteScalar()).ToString("0.00");
                    }

                    // stock count
                    string stockQuery = @"
                        SELECT IFNULL(SUM(stock_quantity), 0)
                        FROM products
                        WHERE active_status = 1";

                    using (var stockCmd = new MySql.Data.MySqlClient.MySqlCommand(stockQuery, connection))
                    {
                        lblStockCount.Text = stockCmd.ExecuteScalar().ToString();
                    }

                    // active order count
                    string activeOrdersQuery = @"
                        SELECT COUNT(*)
                        FROM orders
                        WHERE order_status IN ('Pending', 'Confirmed')";

                    using (var orderCmd = new MySql.Data.MySqlClient.MySqlCommand(activeOrdersQuery, connection))
                    {
                        lblActiveOrderCount.Text = orderCmd.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ExceptionHandler.Handle(ex, "Failed to load dashboard statistics.");
            }
        }
    }
}
