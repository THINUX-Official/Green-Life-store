using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GreenLifeStore.Utils
{
    internal static class ExceptionHandler
    {
        public static void Handle(Exception ex, string userContext = null)
        {
            string message;

            if (ex is MySqlException)
            {
                message = "A database error occurred. Please try again later.";
            }
            else if (ex is InvalidOperationException)
            {
                message = ex.Message;
            }
            else if (ex is UnauthorizedAccessException)
            {
                message = "You do not have permission to perform this action.";
            }
            else
            {
                message = "An unexpected system error occurred. Please contact support.";
            }

            if (!string.IsNullOrWhiteSpace(userContext))
            {
                message = userContext + "\n\n" + message;
            }

            MessageBox.Show(
                message,
                "System Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
