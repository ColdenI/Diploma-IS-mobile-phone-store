using Microsoft.Data.SqlClient;
using Program.scr.core;
using System.Data;
using System.Text.RegularExpressions;

namespace Program.scr.forms
{
    public partial class RegistrationEmployeeForm : Form
    {
        public RegistrationEmployeeForm()
        {
            InitializeComponent();
            LoadSalonsIntoComboBox();

            comboBox_role.Items.Clear();
            comboBox_role.Items.AddRange(Core.Roles);
        }

        private void LoadSalonsIntoComboBox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SQL._sqlConnectStr))
                {
                    conn.Open();
                    string sql = "SELECT ID_Salon, Address FROM Salon ORDER BY Address"; // Выбираем ID и отображаемый текст
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBox_salon.DisplayMember = "Address"; // Что отображать
                    comboBox_salon.ValueMember = "ID_Salon";  // Какое значение использовать
                    comboBox_salon.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки салонов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_reg_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Проверка на пустые поля (добавлен comboBoxSalon)
                if (string.IsNullOrWhiteSpace(textBox_name.Text) ||
                    string.IsNullOrWhiteSpace(textBox_phone.Text) ||
                    string.IsNullOrWhiteSpace(textBox_email.Text) ||
                    string.IsNullOrWhiteSpace(textBox_post.Text) || // Должность
                    string.IsNullOrWhiteSpace(textBox_login.Text) ||
                    string.IsNullOrWhiteSpace(textBox_password.Text) ||
                    string.IsNullOrWhiteSpace(textBox_password_agein.Text) ||
                    comboBox_role.SelectedIndex == -1 ||
                    comboBox_salon.SelectedIndex == -1) // Проверяем, выбран ли салон
                {
                    MessageBox.Show("Все поля должны быть заполнены, включая выбор салона.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Проверка длины имени
                if (textBox_name.Text.Length < 4)
                {
                    MessageBox.Show("Полное имя должно содержать более 3 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. Проверка длины логина
                if (textBox_login.Text.Length < 6)
                {
                    MessageBox.Show("Логин должен содержать более 5 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4. Проверка совпадения паролей
                if (textBox_password.Text != textBox_password_agein.Text)
                {
                    MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 5. Проверка пароля: длина > 8, есть заглавная и строчная буква
                if (!IsValidPassword(textBox_password.Text))
                {
                    MessageBox.Show("Пароль должен быть длиннее 8 символов и содержать хотя бы одну заглавную и одну строчную букву.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 6. Проверка существования логина в таблице Auth
                if (IsLoginExists(textBox_login.Text))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 7. Регистрация сотрудника и авторизации
                using (SqlConnection conn = new SqlConnection(SQL._sqlConnectStr))
                {
                    conn.Open();

                    // Начинаем транзакцию
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Вставляем в Employee
                            string insertEmployeeSql = @"
                                INSERT INTO Employee (FullName, Position, Phone, Email, Salary, HireDate, ID_Salon) 
                                VALUES (@FullName, @Position, @Phone, @Email, @Salary, @HireDate, @ID_Salon);
                                SELECT SCOPE_IDENTITY();"; // Получаем ID вставленного сотрудника

                            SqlCommand cmdEmployee = new SqlCommand(insertEmployeeSql, conn, transaction);
                            cmdEmployee.Parameters.AddWithValue("@FullName", textBox_name.Text);
                            cmdEmployee.Parameters.AddWithValue("@Position", textBox_post.Text); // Должность
                            cmdEmployee.Parameters.AddWithValue("@Phone", textBox_phone.Text);
                            cmdEmployee.Parameters.AddWithValue("@Email", textBox_email.Text);
                            cmdEmployee.Parameters.AddWithValue("@Salary", textBox_licens.Value); // Установите значение по умолчанию или из TextBox
                            cmdEmployee.Parameters.AddWithValue("@HireDate", DateTime.Now); // Или из DateTimePicker
                            cmdEmployee.Parameters.AddWithValue("@ID_Salon", (int)comboBox_salon.SelectedValue); // Выбранный ID салона

                            object employeeIdObj = cmdEmployee.ExecuteScalar();
                            int employeeId = Convert.ToInt32(employeeIdObj);

                            // Вставляем в Auth
                            string insertAuthSql = @"
                                INSERT INTO Auth (ID_Employee, Login, Password, AccessLevel) 
                                VALUES (@ID_Employee, @Login, @Password, @AccessLevel);";

                            SqlCommand cmdAuth = new SqlCommand(insertAuthSql, conn, transaction);
                            cmdAuth.Parameters.AddWithValue("@ID_Employee", employeeId); // Привязываем к новому сотруднику
                            cmdAuth.Parameters.AddWithValue("@Login", textBox_login.Text);
                            cmdAuth.Parameters.AddWithValue("@Password", textBox_password.Text); // Пароль в открытом виде, как вы просили
                            cmdAuth.Parameters.AddWithValue("@AccessLevel", comboBox_role.SelectedItem.ToString()); // Роль

                            cmdAuth.ExecuteNonQuery();

                            // Фиксируем транзакцию
                            transaction.Commit();

                            MessageBox.Show("Регистрация сотрудника прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при регистрации сотрудника: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsValidPassword(string password)
        {
            if (password.Length < 9) return false;
            if (!Regex.IsMatch(password, @"[a-z]")) return false;
            if (!Regex.IsMatch(password, @"[A-Z]")) return false;
            return true;
        }

        // Проверка существования логина в таблице Auth
        private bool IsLoginExists(string login)
        {
            using (SqlConnection conn = new SqlConnection(SQL._sqlConnectStr))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Auth WHERE Login = @Login";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Login", login);
                int count = Convert.ToInt32(cmd.ExecuteScalar()); // Используем Convert.ToInt32
                return count > 0;
            }
        }

    }
}
