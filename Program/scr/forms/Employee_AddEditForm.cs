using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Employee_AddEditForm : Form
    {
        DBT_Employee Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_FullName;
        TextBox textBox_Position;
        TextBox textBox_Phone;
        TextBox textBox_Email;
        TextBox textBox_Salary;
        ComboBox comboBox_ID_Salon;

        private List<DBT_Salon> salons;

        public Employee_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Employee_AddEditForm(DBT_Employee obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_FullName.Text = obj.FullName.ToString();
            textBox_Position.Text = obj.Position.ToString();
            textBox_Phone.Text = obj.Phone.ToString();
            textBox_Email.Text = obj.Email.ToString();
            textBox_Salary.Text = obj.Salary.ToString();
            comboBox_ID_Salon.SelectedIndex = indexOf_salons(obj.ID_Salon);

            comboBox_ID_Salon.Enabled = false;
        }

        private void Init()
        {
            salons = new List<DBT_Salon>();

            this.Size = new Size(600, 500);
            this.Text = "Сотрудники - " + (Object == null ? "Добавить" : "Изменить").ToString();
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
                RowCount = 7
            };

            Label label_FullName = new Label();
            SetLabel(ref label_FullName, "ФИО");
            tableLayout.Controls.Add(label_FullName, 0, 0);
            textBox_FullName = new TextBox();
            textBox_FullName.Dock = DockStyle.Fill;
            textBox_FullName.MaxLength = 100;
            tableLayout.Controls.Add(textBox_FullName, 1, 0);

            Label label_Position = new Label();
            SetLabel(ref label_Position, "Должность");
            tableLayout.Controls.Add(label_Position, 0, 1);
            textBox_Position = new TextBox();
            textBox_Position.Dock = DockStyle.Fill;
            textBox_Position.MaxLength = 100;
            tableLayout.Controls.Add(textBox_Position, 1, 1);

            Label label_Phone = new Label();
            SetLabel(ref label_Phone, "Номер телефона");
            tableLayout.Controls.Add(label_Phone, 0, 2);
            textBox_Phone = new TextBox();
            textBox_Phone.Dock = DockStyle.Fill;
            textBox_Phone.MaxLength = 20;
            tableLayout.Controls.Add(textBox_Phone, 1, 2);

            Label label_Email = new Label();
            SetLabel(ref label_Email, "Электронная почта");
            tableLayout.Controls.Add(label_Email, 0, 3);
            textBox_Email = new TextBox();
            textBox_Email.Dock = DockStyle.Fill;
            textBox_Email.MaxLength = 255;
            tableLayout.Controls.Add(textBox_Email, 1, 3);

            Label label_Salary = new Label();
            SetLabel(ref label_Salary, "Заработная плата");
            tableLayout.Controls.Add(label_Salary, 0, 4);
            textBox_Salary = new TextBox();
            textBox_Salary.Dock = DockStyle.Fill;
            textBox_Salary.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_Salary, 1, 4);



            Label label_ID_Salon = new Label();
            SetLabel(ref label_ID_Salon, "Салон");
            tableLayout.Controls.Add(label_ID_Salon, 0, 6);
            comboBox_ID_Salon = new ComboBox();
            comboBox_ID_Salon.Dock = DockStyle.Fill;
            comboBox_ID_Salon.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_ID_Salon, 1, 6);
            LoadComboBox_salons();

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

        private void LoadComboBox_salons()
        {
            salons = DBT_Salon.GetAll();

            comboBox_ID_Salon.Items.Clear();
            foreach (var i in salons)
                comboBox_ID_Salon.Items.Add(i.Address);
        }
        private int indexOf_salons(int id)
        {
            for (int i = 0; i < salons.Count; i++)
            {
                if (salons[i].ID_Salon == id) return i;
            }
            return -1;
        }

        private void Button_apply_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_FullName.Text)) { MessageBox.Show("Поле 'ФИО' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Position.Text)) { MessageBox.Show("Поле 'Должность' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Phone.Text)) { MessageBox.Show("Поле 'Номер телефона' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Email.Text)) { MessageBox.Show("Поле 'Электронная почта' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_Salary.Text, out decimal tp_Salary)) { MessageBox.Show("Поле 'заработная плата' имеет некорректное значение!"); return; }
            if (comboBox_ID_Salon.SelectedIndex == -1) { MessageBox.Show("Поле 'Салон' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Employee.Create(
                    new DBT_Employee()
                    {
                        FullName = textBox_FullName.Text,
                        Position = textBox_Position.Text,
                        Phone = textBox_Phone.Text,
                        Email = textBox_Email.Text,
                        Salary = decimal.Parse(textBox_Salary.Text),
                        HireDate = DateTime.Now,
                        ID_Salon = salons[comboBox_ID_Salon.SelectedIndex].ID_Salon
                    }
                );
            }
            else
            {
                res = DBT_Employee.Edit(
                    new DBT_Employee()
                    {
                        ID_Employee = Object.ID_Employee,
                        FullName = textBox_FullName.Text,
                        Position = textBox_Position.Text,
                        Phone = textBox_Phone.Text,
                        Email = textBox_Email.Text,
                        Salary = decimal.Parse(textBox_Salary.Text),
                        HireDate = Object.HireDate,
                        ID_Salon = Object.ID_Salon
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
