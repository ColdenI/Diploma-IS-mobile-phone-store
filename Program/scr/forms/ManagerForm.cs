using Microsoft.Data.SqlClient;
using Program.scr.core;

namespace Program.scr.forms
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();

            UpdateTable();
        }

        private void UpdateTable()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.BringToFront();
            dataGridView.ReadOnly = true;
            //dataGridView.Dock = DockStyle.Fill;
            dataGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView.Columns.Add("_0", "Название");
            dataGridView.Columns.Add("_1", "Описание");
            dataGridView.Columns.Add("_2", "Стоимость");
            dataGridView.Columns.Add("_3", "Особенности");

            using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
            {
                connection.Open();
                using (var query = connection.CreateCommand())
                {
                    query.CommandText = "SELECT * FROM TariffPlan";
                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var index = dataGridView.Rows.Add();
                            dataGridView.Rows[index].Cells[0].Value = reader.GetString(1);
                            dataGridView.Rows[index].Cells[1].Value = reader.GetString(2);
                            dataGridView.Rows[index].Cells[2].Value = reader.GetDecimal(3);
                            dataGridView.Rows[index].Cells[3].Value = reader.GetString(4);
                        }
                    }
                }
            }
        }

        private void button_editor_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DataEditorForm().ShowDialog();
            UpdateTable();
            this.Visible = true;
        }

        private void button_newSale_Click(object sender, EventArgs e)
        {
            new Sale_AddEditForm().ShowDialog();
        }

        private void button_newClient_Click(object sender, EventArgs e)
        {
            new Client_AddEditForm().ShowDialog();
        }

        private void button_newContract_Click(object sender, EventArgs e)
        {
            new Contract_AddEditForm().ShowDialog();
        }
    }
}
