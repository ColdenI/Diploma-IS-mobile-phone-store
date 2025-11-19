using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Auth_AddEditForm : Form
    {
        DBT_Auth Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_ID_Employee;
        TextBox textBox_Login;
        TextBox textBox_Password;
        ComboBox comboBox_AccessLevel;

        public Auth_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Auth_AddEditForm(DBT_Auth obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_ID_Employee.Text = DBT_Employee.GetById(obj.ID_Employee).FullName;
            textBox_Login.Text = obj.Login.ToString();
            textBox_Password.Text = obj.Password.ToString();
            comboBox_AccessLevel.Text = obj.AccessLevel.ToString();
        }

        private void Init()
        {
            this.Size = new Size(600, 500);
            this.Text = "Авторизация - " + (Object == null ? "Добавить" : "Изменить").ToString();
            this.MinimumSize = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            button_apply = new Button()
            {
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_apply.Click += Button_apply_Click;
            button_apply.Text = Object == null ? "Добавить" : "Изменить";
            this.Controls.Add(button_apply);

            tableLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4
            };

            Label label_ID_Employee = new Label();
            SetLabel(ref label_ID_Employee, "Сотрудник");
            tableLayout.Controls.Add(label_ID_Employee, 0, 0);
            textBox_ID_Employee = new TextBox();
            textBox_ID_Employee.Dock = DockStyle.Fill;
            textBox_ID_Employee.MaxLength = 1000;
            textBox_ID_Employee.Enabled = false;
            tableLayout.Controls.Add(textBox_ID_Employee, 1, 0);

            Label label_Login = new Label();
            SetLabel(ref label_Login, "Логин");
            tableLayout.Controls.Add(label_Login, 0, 1);
            textBox_Login = new TextBox();
            textBox_Login.Dock = DockStyle.Fill;
            textBox_Login.MaxLength = 50;
            tableLayout.Controls.Add(textBox_Login, 1, 1);

            Label label_Password = new Label();
            SetLabel(ref label_Password, "Пароль");
            tableLayout.Controls.Add(label_Password, 0, 2);
            textBox_Password = new TextBox();
            textBox_Password.Dock = DockStyle.Fill;
            textBox_Password.MaxLength = 255;
            tableLayout.Controls.Add(textBox_Password, 1, 2);

            Label label_AccessLevel = new Label();
            SetLabel(ref label_AccessLevel, "Уровень доступа");
            tableLayout.Controls.Add(label_AccessLevel, 0, 3);
            comboBox_AccessLevel = new ComboBox();
            comboBox_AccessLevel.Dock = DockStyle.Fill;
            comboBox_AccessLevel.MaxLength = 50;
            tableLayout.Controls.Add(comboBox_AccessLevel, 1, 3);
            comboBox_AccessLevel.Items.Clear();
            comboBox_AccessLevel.Items.AddRange(core.Core.Roles);

            this.Controls.Add(tableLayout);
        }

        private void SetLabel(ref Label label, string text = "")
        {
            label.Font = new Font(Font.FontFamily, 12);
            label.TextAlign = ContentAlignment.TopLeft;
            label.Dock = DockStyle.Fill;
            label.AutoSize = false;
            label.Width = 200;
            label.Text = text;
        }

        private void Button_apply_Click(object? sender, EventArgs e)
        {
            //if (!int.TryParse(textBox_ID_Employee.Text, out int tp_ID_Employee)) { MessageBox.Show("Поле 'Сотрудник' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Login.Text)) { MessageBox.Show("Поле 'Логин' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Password.Text)) { MessageBox.Show("Поле 'Пароль' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(comboBox_AccessLevel.Text)) { MessageBox.Show("Поле 'Уровень доступа' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Auth.Create(
                    new DBT_Auth()
                    {
                        ID_Employee = int.Parse(textBox_ID_Employee.Text),
                        Login = textBox_Login.Text,
                        Password = textBox_Password.Text,
                        AccessLevel = comboBox_AccessLevel.Text
                    }
                );
            }
            else
            {
                res = DBT_Auth.Edit(
                    new DBT_Auth()
                    {
                        ID_Auth = Object.ID_Auth,
                        ID_Employee = Object.ID_Employee,
                        Login = textBox_Login.Text,
                        Password = textBox_Password.Text,
                        AccessLevel = comboBox_AccessLevel.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Логин занят!");
            else this.Close();
        }
    }
}
