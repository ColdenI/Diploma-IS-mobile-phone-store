using Program.scr.core;
using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Sale_AddEditForm : Form
    {
        DBT_Sale Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        ComboBox comboBox_ID_Client;
        //ComboBox comboBox_ID_Employee;
        ComboBox comboBox_ID_Product;
        TextBox textBox_Comment;

        private List<DBT_Client> clients;
        private List<DBT_Employee> employees;
        private List<DBT_Product> products;

        public Sale_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Sale_AddEditForm(DBT_Sale obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            if(obj.ID_Client != null) comboBox_ID_Client.SelectedIndex = indexOf_clients((int)obj.ID_Client);
            //comboBox_ID_Employee.SelectedIndex = indexOf_employees(obj.ID_Employee);
            comboBox_ID_Product.SelectedIndex = indexOf_products(obj.ID_Product);
            textBox_Comment.Text = obj.Comment.ToString();
        }

        private void Init()
        {
            clients = new List<DBT_Client>();
            employees = new List<DBT_Employee>();
            products = new List<DBT_Product>();

            this.Size = new Size(600, 500);
            this.Text = "Продажи - " + (Object == null ? "Добавить" : "Изменить").ToString();
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
                RowCount = 6
            };

            Label label_ID_Client = new Label();
            SetLabel(ref label_ID_Client, "Клиент");
            tableLayout.Controls.Add(label_ID_Client, 0, 0);
            comboBox_ID_Client = new ComboBox();
            comboBox_ID_Client.Dock = DockStyle.Fill;
            comboBox_ID_Client.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_ID_Client, 1, 0);

            //Label label_ID_Employee = new Label();
            //SetLabel(ref label_ID_Employee, "Сотрудник");
            //tableLayout.Controls.Add(label_ID_Employee, 0, 1);
            //comboBox_ID_Employee = new ComboBox();
            //comboBox_ID_Employee.Dock = DockStyle.Fill;
            //comboBox_ID_Employee.MaxLength = 1000;
            //tableLayout.Controls.Add(comboBox_ID_Employee, 1, 1);

            Label label_ID_Product = new Label();
            SetLabel(ref label_ID_Product, "Товар");
            tableLayout.Controls.Add(label_ID_Product, 0, 2);
            comboBox_ID_Product = new ComboBox();
            comboBox_ID_Product.Dock = DockStyle.Fill;
            comboBox_ID_Product.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_ID_Product, 1, 2);


            Label label_Comment = new Label();
            SetLabel(ref label_Comment, "Комментарий");
            tableLayout.Controls.Add(label_Comment, 0, 5);
            textBox_Comment = new TextBox();
            textBox_Comment.Dock = DockStyle.Fill;
            textBox_Comment.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_Comment, 1, 5);

            LoadComboBox_clients();
            //LoadComboBox_employees();
            LoadComboBox_products();

            this.Controls.Add(tableLayout);
        }

        //private void LoadComboBox_employees()
        //{
        //    employees = DBT_Employee.GetAll();

        //    comboBox_ID_Employee.Items.Clear();
        //    foreach (var i in employees)
        //        comboBox_ID_Employee.Items.Add(i.FullName);
        //}
        private int indexOf_employees(int id)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].ID_Employee == id) return i;
            }
            return -1;
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
        private void LoadComboBox_products()
        {
            products = DBT_Product.GetAll();

            comboBox_ID_Product.Items.Clear();
            foreach (var i in products)
                comboBox_ID_Product.Items.Add(i.Name);
        }
        private int indexOf_products(int id)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].ID_Product == id) return i;
            }
            return -1;
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
            int? _client = null;
            if (comboBox_ID_Client.SelectedIndex != -1) _client = clients[comboBox_ID_Client.SelectedIndex].ID_Client;
            //if (comboBox_ID_Employee.SelectedIndex == -1) { MessageBox.Show("Поле 'Сотрудник' имеет некорректное значение!"); return; }
            if (comboBox_ID_Product.SelectedIndex == -1) { MessageBox.Show("Поле 'Товар' имеет некорректное значение!"); return; }
            //if (!decimal.TryParse(textBox_TotalAmount.Text, out decimal tp_TotalAmount)) { MessageBox.Show("Поле 'Сумма заказа' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Sale.Create(
                    new DBT_Sale()
                    {
                        ID_Client = _client,
                        ID_Employee = Core.ThisEmployee.ID_Employee,
                        ID_Product = products[comboBox_ID_Product.SelectedIndex].ID_Product,
                        SaleDate = DateTime.Now,
                        TotalAmount = products[comboBox_ID_Product.SelectedIndex].Price,
                        Comment = textBox_Comment.Text
                    }
                );
            }
            else
            {
                res = DBT_Sale.Edit(
                    new DBT_Sale()
                    {
                        ID_Sale = Object.ID_Sale,
                        ID_Client = _client,
                        ID_Employee = Object.ID_Employee,
                        ID_Product = products[comboBox_ID_Product.SelectedIndex].ID_Product,
                        SaleDate = Object.SaleDate,
                        TotalAmount = Object.TotalAmount,
                        Comment = textBox_Comment.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
