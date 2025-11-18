using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Client_AddEditForm : Form
    {
        DBT_Client Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_FullName;
        TextBox textBox_Phone;
        TextBox textBox_Email;
        TextBox textBox_Address;
        //DateTimePicker dateTimePicker_RegistrationDate;

        public Client_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Client_AddEditForm(DBT_Client obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_FullName.Text = obj.FullName.ToString();
            textBox_Phone.Text = obj.Phone.ToString();
            textBox_Email.Text = obj.Email.ToString();
            textBox_Address.Text = obj.Address.ToString();
            //dateTimePicker_RegistrationDate.Value = (DateTime)((obj.RegistrationDate == null) ? DateTime.Now : obj.RegistrationDate);
        }

        private void Init()
        {
            this.Size = new Size(600, 500);
            this.Text = "Клиента - " + (Object == null ? "Добавить" : "Изменить").ToString();
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

            Label label_FullName = new Label();
            SetLabel(ref label_FullName, "ФИО");
            tableLayout.Controls.Add(label_FullName, 0, 0);
            textBox_FullName = new TextBox();
            textBox_FullName.Dock = DockStyle.Fill;
            textBox_FullName.MaxLength = 100;
            tableLayout.Controls.Add(textBox_FullName, 1, 0);

            Label label_Phone = new Label();
            SetLabel(ref label_Phone, "Номер телефона");
            tableLayout.Controls.Add(label_Phone, 0, 1);
            textBox_Phone = new TextBox();
            textBox_Phone.Dock = DockStyle.Fill;
            textBox_Phone.MaxLength = 20;
            tableLayout.Controls.Add(textBox_Phone, 1, 1);

            Label label_Email = new Label();
            SetLabel(ref label_Email, "Электронная почта");
            tableLayout.Controls.Add(label_Email, 0, 2);
            textBox_Email = new TextBox();
            textBox_Email.Dock = DockStyle.Fill;
            textBox_Email.MaxLength = 255;
            tableLayout.Controls.Add(textBox_Email, 1, 2);

            Label label_Address = new Label();
            SetLabel(ref label_Address, "Адрес");
            tableLayout.Controls.Add(label_Address, 0, 3);
            textBox_Address = new TextBox();
            textBox_Address.Dock = DockStyle.Fill;
            textBox_Address.MaxLength = 255;
            tableLayout.Controls.Add(textBox_Address, 1, 3);


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
            if (string.IsNullOrWhiteSpace(textBox_FullName.Text)) { MessageBox.Show("Поле 'ФИО' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Phone.Text)) { MessageBox.Show("Поле 'Номер телефона' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Email.Text)) { MessageBox.Show("Поле 'Электронная почта' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Address.Text)) { MessageBox.Show("Поле 'Адрес' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Client.Create(
                    new DBT_Client()
                    {
                        FullName = textBox_FullName.Text,
                        Phone = textBox_Phone.Text,
                        Email = textBox_Email.Text,
                        Address = textBox_Address.Text,
                        RegistrationDate = DateTime.Now,
                    }
                );
            }
            else
            {
                res = DBT_Client.Edit(
                    new DBT_Client()
                    {
                        ID_Client = Object.ID_Client,
                        FullName = textBox_FullName.Text,
                        Phone = textBox_Phone.Text,
                        Email = textBox_Email.Text,
                        Address = textBox_Address.Text,
                        RegistrationDate = Object.RegistrationDate
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
