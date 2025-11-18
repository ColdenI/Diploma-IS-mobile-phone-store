using Program.scr.core;
using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Contract_AddEditForm : Form
    {
        DBT_Contract Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        ComboBox comboBox_ID_Client;
        ComboBox comboBox_ID_Tariff;
        //ComboBox comboBox_ID_Employee;
        TextBox textBox_Status;

        private List<DBT_Client> clients;
        private List<DBT_TariffPlan> tariffs;
        //private List<DBT_Employee> employees;

        public Contract_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Contract_AddEditForm(DBT_Contract obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            comboBox_ID_Client.SelectedIndex = indexOf_clients(obj.ID_Client);
            comboBox_ID_Tariff.SelectedIndex = indexOf_tariffs(obj.ID_Tariff);
            //comboBox_ID_Employee.SelectedIndex = indexOf_employees(obj.ID_Employee);
            textBox_Status.Text = obj.Status.ToString();
        }

        private void Init()
        {
            clients = new List<DBT_Client>();
            tariffs = new List<DBT_TariffPlan>();
            //employees = new List<DBT_Employee>();

            this.Size = new Size(600, 500);
            this.Text = "Контракты - " + (Object == null ? "Добавить" : "Изменить").ToString();
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
                RowCount = 5
            };

            Label label_ID_Client = new Label();
            SetLabel(ref label_ID_Client, "Клиент");
            tableLayout.Controls.Add(label_ID_Client, 0, 0);
            comboBox_ID_Client = new ComboBox();
            comboBox_ID_Client.Dock = DockStyle.Fill;
            comboBox_ID_Client.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_ID_Client, 1, 0);

            Label label_ID_Tariff = new Label();
            SetLabel(ref label_ID_Tariff, "Тариф");
            tableLayout.Controls.Add(label_ID_Tariff, 0, 1);
            comboBox_ID_Tariff = new ComboBox();
            comboBox_ID_Tariff.Dock = DockStyle.Fill;
            comboBox_ID_Tariff.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_ID_Tariff, 1, 1);

            //Label label_ID_Employee = new Label();
            //SetLabel(ref label_ID_Employee, "Сотрудник");
            //tableLayout.Controls.Add(label_ID_Employee, 0, 2);
            //comboBox_ID_Employee = new ComboBox();
            //comboBox_ID_Employee.Dock = DockStyle.Fill;
            //comboBox_ID_Employee.MaxLength = 1000;
            //tableLayout.Controls.Add(comboBox_ID_Employee, 1, 2);

            Label label_Status = new Label();
            SetLabel(ref label_Status, "Статус");
            tableLayout.Controls.Add(label_Status, 0, 4);
            textBox_Status = new TextBox();
            textBox_Status.Dock = DockStyle.Fill;
            textBox_Status.MaxLength = 50;
            tableLayout.Controls.Add(textBox_Status, 1, 4);

            LoadComboBox_clients();
            //LoadComboBox_employees();
            LoadComboBox_tariffs();

            this.Controls.Add(tableLayout);
        }

        private void LoadComboBox_clients()
        {
            clients = DBT_Client.GetAll();

            comboBox_ID_Client.Items.Clear();
            foreach (var i in clients)
                comboBox_ID_Client.Items.Add(i.FullName);
        }
        private int indexOf_clients(int id)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].ID_Client == id) return i;
            }
            return -1;
        }
        private void LoadComboBox_tariffs()
        {
            tariffs = DBT_TariffPlan.GetAll();

            comboBox_ID_Tariff.Items.Clear();
            foreach (var i in tariffs)
                comboBox_ID_Tariff.Items.Add(i.Name);
        }
        private int indexOf_tariffs(int id)
        {
            for (int i = 0; i < tariffs.Count; i++)
            {
                if (tariffs[i].ID_Tariff == id) return i;
            }
            return -1;
        }
        /*
        private void LoadComboBox_employees()
        {
            employees = DBT_Employee.GetAll();

            comboBox_ID_Employee.Items.Clear();
            foreach (var i in employees)
                comboBox_ID_Employee.Items.Add(i.FullName);
        }
        private int indexOf_employees(int id)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].ID_Employee == id) return i;
            }
            return -1;
        }
        */
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
            if (comboBox_ID_Client.SelectedIndex == -1) { MessageBox.Show("Поле 'Клиент' имеет некорректное значение!"); return; }
            if (comboBox_ID_Tariff.SelectedIndex == -1) { MessageBox.Show("Поле 'Тариф' имеет некорректное значение!"); return; }
            //if (comboBox_ID_Employee.SelectedIndex == -1) { MessageBox.Show("Поле 'Сотрудник' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Status.Text)) { MessageBox.Show("Поле 'Статус' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Contract.Create(
                    new DBT_Contract()
                    {
                        ID_Client = clients[comboBox_ID_Client.SelectedIndex].ID_Client,
                        ID_Tariff = tariffs[comboBox_ID_Tariff.SelectedIndex].ID_Tariff,
                        ID_Employee = Core.ThisEmployee.ID_Employee,
                        SigningDate = DateTime.Now,
                        Status = textBox_Status.Text
                    }
                );
            }
            else
            {
                res = DBT_Contract.Edit(
                    new DBT_Contract()
                    {
                        ID_Contract = Object.ID_Contract,
                        ID_Client = clients[comboBox_ID_Client.SelectedIndex].ID_Client,
                        ID_Tariff = tariffs[comboBox_ID_Tariff.SelectedIndex].ID_Tariff,
                        ID_Employee = Core.ThisEmployee.ID_Employee,
                        SigningDate = Object.SigningDate,
                        Status = textBox_Status.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
