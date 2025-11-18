using Microsoft.Data.SqlClient;
using Program.scr.core;
using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void button_auth_Click(object sender, EventArgs e)
        {
            string login = textBox_login.Text.Trim();
            string password = textBox_password.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT ID_Employee, ID_Auth FROM Auth WHERE Login = @login AND Password = @password";
                        query.Parameters.AddWithValue("@login", login);
                        query.Parameters.AddWithValue("@password", password);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.IsDBNull(0))
                                {
                                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                Core.ThisEmployee = DBT_Employee.GetById(reader.GetInt32(0));
                                Core.ThisUser = DBT_Auth.GetById(reader.GetInt32(1));
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (Core.ThisUser == null)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Core.ThisUser.AccessLevel == "Not")
            {
                MessageBox.Show("Нет доступа :(", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Hide();
            if (Core.ThisUser.AccessLevel == "Admin") new AdminForm().ShowDialog();
            else if (Core.ThisUser.AccessLevel == "Manager") new ManagerForm().ShowDialog();
            else if (Core.ThisUser.AccessLevel == "HeadManager") new ManagerForm().ShowDialog();
            else MessageBox.Show("Роль не опознана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Application.Exit();
        }
    }
}
