namespace Program.scr.forms
{
    partial class RegistrationEmployeeForm
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
            button_reg = new Button();
            label1 = new Label();
            label2 = new Label();
            textBox_name = new TextBox();
            textBox_phone = new TextBox();
            textBox_email = new TextBox();
            textBox_login = new TextBox();
            textBox_password = new TextBox();
            textBox_password_agein = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            comboBox_role = new ComboBox();
            label9 = new Label();
            label10 = new Label();
            textBox_post = new TextBox();
            label11 = new Label();
            comboBox_salon = new ComboBox();
            textBox_licens = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)textBox_licens).BeginInit();
            SuspendLayout();
            // 
            // button_reg
            // 
            button_reg.Location = new Point(16, 377);
            button_reg.Name = "button_reg";
            button_reg.Size = new Size(360, 26);
            button_reg.TabIndex = 0;
            button_reg.Text = "Зарегистрировать";
            button_reg.UseVisualStyleBackColor = true;
            button_reg.Click += button_reg_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 55);
            label1.Name = "label1";
            label1.Size = new Size(89, 19);
            label1.TabIndex = 1;
            label1.Text = "Полное имя:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 14);
            label2.Name = "label2";
            label2.Size = new Size(299, 19);
            label2.TabIndex = 2;
            label2.Text = "Для регистрации введите следующие данные";
            // 
            // textBox_name
            // 
            textBox_name.Location = new Point(147, 52);
            textBox_name.MaxLength = 100;
            textBox_name.Name = "textBox_name";
            textBox_name.Size = new Size(225, 26);
            textBox_name.TabIndex = 3;
            // 
            // textBox_phone
            // 
            textBox_phone.Location = new Point(147, 84);
            textBox_phone.MaxLength = 20;
            textBox_phone.Name = "textBox_phone";
            textBox_phone.Size = new Size(225, 26);
            textBox_phone.TabIndex = 4;
            // 
            // textBox_email
            // 
            textBox_email.Location = new Point(147, 116);
            textBox_email.MaxLength = 255;
            textBox_email.Name = "textBox_email";
            textBox_email.Size = new Size(225, 26);
            textBox_email.TabIndex = 5;
            // 
            // textBox_login
            // 
            textBox_login.Location = new Point(147, 248);
            textBox_login.MaxLength = 50;
            textBox_login.Name = "textBox_login";
            textBox_login.Size = new Size(225, 26);
            textBox_login.TabIndex = 6;
            // 
            // textBox_password
            // 
            textBox_password.Location = new Point(147, 280);
            textBox_password.MaxLength = 250;
            textBox_password.Name = "textBox_password";
            textBox_password.PasswordChar = '*';
            textBox_password.Size = new Size(225, 26);
            textBox_password.TabIndex = 7;
            // 
            // textBox_password_agein
            // 
            textBox_password_agein.Location = new Point(147, 312);
            textBox_password_agein.MaxLength = 250;
            textBox_password_agein.Name = "textBox_password_agein";
            textBox_password_agein.PasswordChar = '*';
            textBox_password_agein.Size = new Size(225, 26);
            textBox_password_agein.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 87);
            label3.Name = "label3";
            label3.Size = new Size(119, 19);
            label3.TabIndex = 9;
            label3.Text = "Номер телефона:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 119);
            label4.Name = "label4";
            label4.Size = new Size(44, 19);
            label4.TabIndex = 10;
            label4.Text = "Email:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 251);
            label5.Name = "label5";
            label5.Size = new Size(50, 19);
            label5.TabIndex = 11;
            label5.Text = "Логин:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 283);
            label6.Name = "label6";
            label6.Size = new Size(59, 19);
            label6.TabIndex = 12;
            label6.Text = "Пароль:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 315);
            label7.Name = "label7";
            label7.Size = new Size(129, 19);
            label7.TabIndex = 13;
            label7.Text = "Повторите пароль:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 186);
            label8.Name = "label8";
            label8.Size = new Size(125, 19);
            label8.TabIndex = 14;
            label8.Text = "Заработная плата:";
            // 
            // comboBox_role
            // 
            comboBox_role.FormattingEnabled = true;
            comboBox_role.Items.AddRange(new object[] { "Admin", "Manager", "HeadManager", "Not" });
            comboBox_role.Location = new Point(147, 344);
            comboBox_role.Name = "comboBox_role";
            comboBox_role.Size = new Size(225, 27);
            comboBox_role.TabIndex = 16;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 347);
            label9.Name = "label9";
            label9.Size = new Size(42, 19);
            label9.TabIndex = 17;
            label9.Text = "Роль:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 218);
            label10.Name = "label10";
            label10.Size = new Size(82, 19);
            label10.TabIndex = 18;
            label10.Text = "Должность:";
            // 
            // textBox_post
            // 
            textBox_post.Location = new Point(147, 215);
            textBox_post.MaxLength = 100;
            textBox_post.Name = "textBox_post";
            textBox_post.Size = new Size(225, 26);
            textBox_post.TabIndex = 19;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 151);
            label11.Name = "label11";
            label11.Size = new Size(51, 19);
            label11.TabIndex = 20;
            label11.Text = "Салон:";
            // 
            // comboBox_salon
            // 
            comboBox_salon.FormattingEnabled = true;
            comboBox_salon.Items.AddRange(new object[] { "Admin", "Manager", "HeadManager", "Not" });
            comboBox_salon.Location = new Point(147, 148);
            comboBox_salon.Name = "comboBox_salon";
            comboBox_salon.Size = new Size(225, 27);
            comboBox_salon.TabIndex = 21;
            // 
            // textBox_licens
            // 
            textBox_licens.DecimalPlaces = 2;
            textBox_licens.Location = new Point(147, 182);
            textBox_licens.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            textBox_licens.Name = "textBox_licens";
            textBox_licens.Size = new Size(225, 26);
            textBox_licens.TabIndex = 22;
            // 
            // RegistrationEmployeeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(388, 415);
            Controls.Add(textBox_licens);
            Controls.Add(comboBox_salon);
            Controls.Add(label11);
            Controls.Add(textBox_post);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(comboBox_role);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox_password_agein);
            Controls.Add(textBox_password);
            Controls.Add(textBox_login);
            Controls.Add(textBox_email);
            Controls.Add(textBox_phone);
            Controls.Add(textBox_name);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button_reg);
            Name = "RegistrationEmployeeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Регистрация сотрудника";
            ((System.ComponentModel.ISupportInitialize)textBox_licens).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_reg;
        private Label label1;
        private Label label2;
        private TextBox textBox_name;
        private TextBox textBox_phone;
        private TextBox textBox_email;
        private TextBox textBox_login;
        private TextBox textBox_password;
        private TextBox textBox_password_agein;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private ComboBox comboBox_role;
        private Label label9;
        private Label label10;
        private TextBox textBox_post;
        private Label label11;
        private ComboBox comboBox_salon;
        private NumericUpDown textBox_licens;
    }
}