using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Product_AddEditForm : Form
    {
        DBT_Product Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_Name;
        TextBox textBox_Type;
        TextBox textBox_Manufacturer;
        TextBox textBox_Model;
        TextBox textBox_Price;
        TextBox textBox_Warranty;

        public Product_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Product_AddEditForm(DBT_Product obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_Name.Text = obj.Name.ToString();
            textBox_Type.Text = obj.Type.ToString();
            textBox_Manufacturer.Text = obj.Manufacturer.ToString();
            textBox_Model.Text = obj.Model.ToString();
            textBox_Price.Text = obj.Price.ToString();
            textBox_Warranty.Text = obj.Warranty.ToString();
        }

        private void Init()
        {
            this.Size = new Size(600, 500);
            this.Text = "Товары - " + (Object == null ? "Добавить" : "Изменить").ToString();
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

            Label label_Name = new Label();
            SetLabel(ref label_Name, "Наименование");
            tableLayout.Controls.Add(label_Name, 0, 0);
            textBox_Name = new TextBox();
            textBox_Name.Dock = DockStyle.Fill;
            textBox_Name.MaxLength = 255;
            tableLayout.Controls.Add(textBox_Name, 1, 0);

            Label label_Type = new Label();
            SetLabel(ref label_Type, "Тип");
            tableLayout.Controls.Add(label_Type, 0, 1);
            textBox_Type = new TextBox();
            textBox_Type.Dock = DockStyle.Fill;
            textBox_Type.MaxLength = 50;
            tableLayout.Controls.Add(textBox_Type, 1, 1);

            Label label_Manufacturer = new Label();
            SetLabel(ref label_Manufacturer, "Производитель");
            tableLayout.Controls.Add(label_Manufacturer, 0, 2);
            textBox_Manufacturer = new TextBox();
            textBox_Manufacturer.Dock = DockStyle.Fill;
            textBox_Manufacturer.MaxLength = 100;
            tableLayout.Controls.Add(textBox_Manufacturer, 1, 2);

            Label label_Model = new Label();
            SetLabel(ref label_Model, "Модель");
            tableLayout.Controls.Add(label_Model, 0, 3);
            textBox_Model = new TextBox();
            textBox_Model.Dock = DockStyle.Fill;
            textBox_Model.MaxLength = 100;
            tableLayout.Controls.Add(textBox_Model, 1, 3);

            Label label_Price = new Label();
            SetLabel(ref label_Price, "Цена");
            tableLayout.Controls.Add(label_Price, 0, 4);
            textBox_Price = new TextBox();
            textBox_Price.Dock = DockStyle.Fill;
            textBox_Price.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_Price, 1, 4);

            Label label_Warranty = new Label();
            SetLabel(ref label_Warranty, "Гарантия");
            tableLayout.Controls.Add(label_Warranty, 0, 5);
            textBox_Warranty = new TextBox();
            textBox_Warranty.Dock = DockStyle.Fill;
            textBox_Warranty.MaxLength = 50;
            tableLayout.Controls.Add(textBox_Warranty, 1, 5);

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
            if (string.IsNullOrWhiteSpace(textBox_Name.Text)) { MessageBox.Show("Поле 'Наименование' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Type.Text)) { MessageBox.Show("Поле 'Тип' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Manufacturer.Text)) { MessageBox.Show("Поле 'Производитель' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_Price.Text, out decimal tp_Price)) { MessageBox.Show("Поле 'Цена' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Product.Create(
                    new DBT_Product()
                    {
                        Name = textBox_Name.Text,
                        Type = textBox_Type.Text,
                        Manufacturer = textBox_Manufacturer.Text,
                        Model = textBox_Model.Text,
                        Price = decimal.Parse(textBox_Price.Text),
                        Warranty = textBox_Warranty.Text
                    }
                );
            }
            else
            {
                res = DBT_Product.Edit(
                    new DBT_Product()
                    {
                        ID_Product = Object.ID_Product,
                        Name = textBox_Name.Text,
                        Type = textBox_Type.Text,
                        Manufacturer = textBox_Manufacturer.Text,
                        Model = textBox_Model.Text,
                        Price = decimal.Parse(textBox_Price.Text),
                        Warranty = textBox_Warranty.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
