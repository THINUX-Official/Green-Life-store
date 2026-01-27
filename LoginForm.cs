using GreenLifeStore.Forms;
using GreenLifeStore.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GreenLifeStore
{
    public partial class LoginForm : BaseForm
    {

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(this);
            registerForm.Show();
            this.Hide();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private bool AuthenticateAdmin(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT admin_id FROM admins WHERE username = @username AND password = @password";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                connection.Open();
                return cmd.ExecuteScalar() != null;
            }
        }

        private int AuthenticateCustomer(string email, string password)
        {

            string hashedPassword = PasswordHasher.HashPassword(password);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT customer_id FROM customers WHERE email = @email AND password = @password";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", hashedPassword);

                connection.Open();
                object result = cmd.ExecuteScalar();
                return result == null ? -1 : Convert.ToInt32(result);
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string input = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter Username and Password.");
                return;
            }

            try
            {
                if (AuthenticateAdmin(input, password))
                {
                    AdminDashboardForm adminDashboardForm = new AdminDashboardForm(this);
                    adminDashboardForm.Show();
                    this.Hide();
                    return;
                }

                int customerId = AuthenticateCustomer(input, password);

                if (customerId != -1)
                {
                    CustomerDashboardForm customerDashboardForm = new CustomerDashboardForm(this, customerId);
                    customerDashboardForm.Show();
                    this.Hide();
                    return;
                }

                MessageBox.Show(
                    "Invalid email or password.",
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(
                    ex.ToString(),
                    "Login Exception",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
