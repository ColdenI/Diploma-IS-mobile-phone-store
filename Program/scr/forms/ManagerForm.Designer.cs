namespace Program.scr.forms
{
    partial class ManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_newSale = new Button();
            button_newContract = new Button();
            button_newClient = new Button();
            button_editor = new Button();
            label1 = new Label();
            dataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // button_newSale
            // 
            button_newSale.Font = new Font("Segoe UI Semibold", 14.7454548F, FontStyle.Bold);
            button_newSale.Location = new Point(12, 342);
            button_newSale.Name = "button_newSale";
            button_newSale.Size = new Size(177, 96);
            button_newSale.TabIndex = 0;
            button_newSale.Text = "Новая продажа";
            button_newSale.UseVisualStyleBackColor = true;
            button_newSale.Click += button_newSale_Click;
            // 
            // button_newContract
            // 
            button_newContract.Font = new Font("Segoe UI Semibold", 14.7454548F, FontStyle.Bold);
            button_newContract.Location = new Point(195, 342);
            button_newContract.Name = "button_newContract";
            button_newContract.Size = new Size(177, 96);
            button_newContract.TabIndex = 1;
            button_newContract.Text = "Новый контракт";
            button_newContract.UseVisualStyleBackColor = true;
            button_newContract.Click += button_newContract_Click;
            // 
            // button_newClient
            // 
            button_newClient.Font = new Font("Segoe UI Semibold", 14.7454548F, FontStyle.Bold);
            button_newClient.Location = new Point(378, 342);
            button_newClient.Name = "button_newClient";
            button_newClient.Size = new Size(177, 96);
            button_newClient.TabIndex = 2;
            button_newClient.Text = "Новый клиент";
            button_newClient.UseVisualStyleBackColor = true;
            button_newClient.Click += button_newClient_Click;
            // 
            // button_editor
            // 
            button_editor.Location = new Point(611, 397);
            button_editor.Name = "button_editor";
            button_editor.Size = new Size(177, 41);
            button_editor.TabIndex = 3;
            button_editor.Text = "Редактор данных >>>";
            button_editor.UseVisualStyleBackColor = true;
            button_editor.Click += button_editor_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(59, 19);
            label1.TabIndex = 4;
            label1.Text = "Тарифы";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 31);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersWidth = 47;
            dataGridView.Size = new Size(776, 305);
            dataGridView.TabIndex = 5;
            // 
            // ManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView);
            Controls.Add(label1);
            Controls.Add(button_editor);
            Controls.Add(button_newClient);
            Controls.Add(button_newContract);
            Controls.Add(button_newSale);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ManagerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Менеджер окно";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_newSale;
        private Button button_newContract;
        private Button button_newClient;
        private Button button_editor;
        private Label label1;
        private DataGridView dataGridView;
    }
}