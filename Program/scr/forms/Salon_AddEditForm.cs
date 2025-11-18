using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Salon_AddEditForm : Form
    {
        DBT_Salon Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_Address;
        TextBox textBox_Phone;
        TextBox textBox_Manager;
        TextBox textBox_Region;

        public Salon_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Salon_AddEditForm(DBT_Salon obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_Address.Text = obj.Address.ToString();
            textBox_Phone.Text = obj.Phone.ToString();
            textBox_Manager.Text = obj.Manager.ToString();
            textBox_Region.Text = obj.Region.ToString();
        }

        private void Init()
        {
            this.Size = new Size(600, 500);
            this.Text = "Салоны - " + (Object == null ? "Добавить" : "Изменить").ToString();
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

            Label label_Address = new Label();
            SetLabel(ref label_Address, "Адрес");
            tableLayout.Controls.Add(label_Address, 0, 0);
            textBox_Address = new TextBox();
            textBox_Address.Dock = DockStyle.Fill;
            textBox_Address.MaxLength = 255;
            tableLayout.Controls.Add(textBox_Address, 1, 0);

            Label label_Phone = new Label();
            SetLabel(ref label_Phone, "Номер телефона");
            tableLayout.Controls.Add(label_Phone, 0, 1);
            textBox_Phone = new TextBox();
            textBox_Phone.Dock = DockStyle.Fill;
            textBox_Phone.MaxLength = 20;
            tableLayout.Controls.Add(textBox_Phone, 1, 1);

            Label label_Manager = new Label();
            SetLabel(ref label_Manager, "Контактное лицо");
            tableLayout.Controls.Add(label_Manager, 0, 2);
            textBox_Manager = new TextBox();
            textBox_Manager.Dock = DockStyle.Fill;
            textBox_Manager.MaxLength = 100;
            tableLayout.Controls.Add(textBox_Manager, 1, 2);

            Label label_Region = new Label();
            SetLabel(ref label_Region, "Регион");
            tableLayout.Controls.Add(label_Region, 0, 3);
            textBox_Region = new TextBox();
            textBox_Region.Dock = DockStyle.Fill;
            textBox_Region.MaxLength = 100;
            tableLayout.Controls.Add(textBox_Region, 1, 3);

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
            if (string.IsNullOrWhiteSpace(textBox_Address.Text)) { MessageBox.Show("Поле 'Адрес' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Phone.Text)) { MessageBox.Show("Поле 'Номер телефона' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Manager.Text)) { MessageBox.Show("Поле 'Контактное лицо' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Region.Text)) { MessageBox.Show("Поле 'Регион' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Salon.Create(
                    new DBT_Salon()
                    {
                        Address = textBox_Address.Text,
                        Phone = textBox_Phone.Text,
                        Manager = textBox_Manager.Text,
                        Region = textBox_Region.Text
                    }
                );
            }
            else
            {
                res = DBT_Salon.Edit(
                    new DBT_Salon()
                    {
                        ID_Salon = Object.ID_Salon,
                        Address = textBox_Address.Text,
                        Phone = textBox_Phone.Text,
                        Manager = textBox_Manager.Text,
                        Region = textBox_Region.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
