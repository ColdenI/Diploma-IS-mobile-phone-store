using Program.scr.core;

namespace Program.scr.forms
{
    public partial class DataEditorForm : Form
    {
        private UserControl UControl;

        public DataEditorForm()
        {
            InitializeComponent();

            if (Core.ThisUser.AccessLevel == "Manager" || Core.ThisUser.AccessLevel == "HeadManager")
            {
                toolStripButton4.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton2.Visible = false;
            }

            ShowControl(new uc.Client_ViewUserControl());
        }

        private void ShowControl(UserControl control)
        {
            if (UControl != null)
            {
                panel1.Controls.Remove(UControl);
                UControl = null;
            }

            UControl = control;
            UControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(UControl);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ShowControl(new uc.Auth_ViewUserControl());
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ShowControl(new uc.Employee_ViewUserControl());
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ShowControl(new uc.Client_ViewUserControl());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowControl(new uc.Salon_ViewUserControl());
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ShowControl(new uc.TariffPlan_ViewUserControl());
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ShowControl(new uc.Product_ViewUserControl());
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ShowControl(new uc.Contract_ViewUserControl());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowControl(new uc.Sale_ViewUserControl());
        }
    }
}
