namespace Program.scr.forms
{
    partial class AdminForm
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
            button_editor = new Button();
            comboBox_salons = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            panel1 = new Panel();
            button2 = new Button();
            SuspendLayout();
            // 
            // button_editor
            // 
            button_editor.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_editor.Location = new Point(827, 560);
            button_editor.Name = "button_editor";
            button_editor.Size = new Size(177, 41);
            button_editor.TabIndex = 4;
            button_editor.Text = "Редактор данных >>>";
            button_editor.UseVisualStyleBackColor = true;
            button_editor.Click += button_editor_Click;
            // 
            // comboBox_salons
            // 
            comboBox_salons.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox_salons.FormattingEnabled = true;
            comboBox_salons.Location = new Point(288, 6);
            comboBox_salons.Name = "comboBox_salons";
            comboBox_salons.Size = new Size(716, 27);
            comboBox_salons.TabIndex = 5;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(912, 39);
            button1.Name = "button1";
            button1.Size = new Size(92, 26);
            button1.TabIndex = 6;
            button1.Text = "Анализ";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(261, 19);
            label1.TabIndex = 7;
            label1.Text = "Аналитика выберите салон для анализа";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(12, 71);
            panel1.Name = "panel1";
            panel1.Size = new Size(992, 483);
            panel1.TabIndex = 8;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.Location = new Point(644, 560);
            button2.Name = "button2";
            button2.Size = new Size(177, 41);
            button2.TabIndex = 9;
            button2.Text = "Панель продаж >>>";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1016, 613);
            Controls.Add(button2);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(comboBox_salons);
            Controls.Add(button_editor);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Администрация";
            Load += AdminForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_editor;
        private ComboBox comboBox_salons;
        private Button button1;
        private Label label1;
        private Panel panel1;
        private Button button2;
    }
}