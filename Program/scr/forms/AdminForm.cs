using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class AdminForm : Form
    {
        WebBrowser webBrowser;
        private List<DBT_Salon> salons;

        public AdminForm()
        {
            InitializeComponent();

            webBrowser = new WebBrowser();
            webBrowser.Dock = DockStyle.Fill;
            panel1.Controls.Add(webBrowser);

            comboBox_salons.Items.Clear();
            salons = new List<DBT_Salon>();

            salons = DBT_Salon.GetAll();

            foreach (var i in salons)
            {
                comboBox_salons.Items.Add(
                    $"{i.Address}"
                    );
            }
        }

        private void button_editor_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DataEditorForm().ShowDialog();
            this.Visible = true;
        }

        private async void AdminForm_Load(object sender, EventArgs e)
        {
            webBrowser.DocumentText = await core.Core.Analytics.GenerateAnalyticsHtmlAsync(1);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (comboBox_salons.SelectedIndex == -1) return;
            webBrowser.DocumentText = await core.Core.Analytics.GenerateAnalyticsHtmlAsync(salons[comboBox_salons.SelectedIndex].ID_Salon);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ManagerForm().ShowDialog();
            this.Visible = true;
        }
    }
}
