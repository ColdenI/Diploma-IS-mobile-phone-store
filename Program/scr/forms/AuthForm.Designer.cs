namespace Program.scr.forms
{
    partial class AuthForm
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
            label1 = new Label();
            label2 = new Label();
            textBox_login = new TextBox();
            textBox_password = new TextBox();
            button_auth = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(47, 19);
            label1.TabIndex = 0;
            label1.Text = "Логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 66);
            label2.Name = "label2";
            label2.Size = new Size(56, 19);
            label2.TabIndex = 1;
            label2.Text = "Пароль";
            // 
            // textBox_login
            // 
            textBox_login.Location = new Point(102, 12);
            textBox_login.MaxLength = 64;
            textBox_login.Name = "textBox_login";
            textBox_login.Size = new Size(166, 26);
            textBox_login.TabIndex = 2;
            // 
            // textBox_password
            // 
            textBox_password.Location = new Point(102, 63);
            textBox_password.MaxLength = 64;
            textBox_password.Name = "textBox_password";
            textBox_password.PasswordChar = '*';
            textBox_password.Size = new Size(166, 26);
            textBox_password.TabIndex = 3;
            // 
            // button_auth
            // 
            button_auth.Location = new Point(182, 105);
            button_auth.Name = "button_auth";
            button_auth.Size = new Size(86, 26);
            button_auth.TabIndex = 4;
            button_auth.Text = "Войти";
            button_auth.UseVisualStyleBackColor = true;
            button_auth.Click += button_auth_Click;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(283, 140);
            Controls.Add(button_auth);
            Controls.Add(textBox_password);
            Controls.Add(textBox_login);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox_login;
        private TextBox textBox_password;
        private Button button_auth;
    }
}