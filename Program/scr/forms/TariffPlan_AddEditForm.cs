using Program.scr.core.dbt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program.scr.forms
{
    public partial class TariffPlan_AddEditForm : Form
    {
        DBT_TariffPlan Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_Name;
        TextBox textBox_Description;
        TextBox textBox_Cost;
        TextBox textBox_Features;

        public TariffPlan_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public TariffPlan_AddEditForm(DBT_TariffPlan obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_Name.Text = obj.Name.ToString();
            textBox_Description.Text = obj.Description.ToString();
            textBox_Cost.Text = obj.Cost.ToString();
            textBox_Features.Text = obj.Features.ToString();
        }

        private void Init()
        {
            this.Size = new Size(600, 500);
            this.Text = "Тарифы - " + (Object == null ? "Добавить" : "Изменить").ToString();
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

            Label label_Name = new Label();
            SetLabel(ref label_Name, "Наименование");
            tableLayout.Controls.Add(label_Name, 0, 0);
            textBox_Name = new TextBox();
            textBox_Name.Dock = DockStyle.Fill;
            textBox_Name.MaxLength = 100;
            tableLayout.Controls.Add(textBox_Name, 1, 0);

            Label label_Description = new Label();
            SetLabel(ref label_Description, "Описание");
            tableLayout.Controls.Add(label_Description, 0, 1);
            textBox_Description = new TextBox();
            textBox_Description.Dock = DockStyle.Fill;
            textBox_Description.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_Description, 1, 1);

            Label label_Cost = new Label();
            SetLabel(ref label_Cost, "Цена");
            tableLayout.Controls.Add(label_Cost, 0, 2);
            textBox_Cost = new TextBox();
            textBox_Cost.Dock = DockStyle.Fill;
            textBox_Cost.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_Cost, 1, 2);

            Label label_Features = new Label();
            SetLabel(ref label_Features, "Особенности");
            tableLayout.Controls.Add(label_Features, 0, 3);
            textBox_Features = new TextBox();
            textBox_Features.Dock = DockStyle.Fill;
            textBox_Features.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_Features, 1, 3);

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
            if (string.IsNullOrWhiteSpace(textBox_Description.Text)) { MessageBox.Show("Поле 'Описание' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_Cost.Text, out decimal tp_Cost)) { MessageBox.Show("Поле 'Цена' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_TariffPlan.Create(
                    new DBT_TariffPlan()
                    {
                        Name = textBox_Name.Text,
                        Description = textBox_Description.Text,
                        Cost = decimal.Parse(textBox_Cost.Text),
                        Features = textBox_Features.Text
                    }
                );
            }
            else
            {
                res = DBT_TariffPlan.Edit(
                    new DBT_TariffPlan()
                    {
                        ID_Tariff = Object.ID_Tariff,
                        Name = textBox_Name.Text,
                        Description = textBox_Description.Text,
                        Cost = decimal.Parse(textBox_Cost.Text),
                        Features = textBox_Features.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
