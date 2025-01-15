namespace Client
{
    partial class AuthorizationForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            тбЛогин = new TextBox();
            тбПароль = new TextBox();
            label1 = new Label();
            label2 = new Label();
            loginButton = new Button();
            regButton = new Button();
            recoverButton = new Button();
            SuspendLayout();
            // 
            // тбЛогин
            // 
            тбЛогин.Location = new Point(97, 64);
            тбЛогин.Name = "тбЛогин";
            тбЛогин.Size = new Size(138, 23);
            тбЛогин.TabIndex = 0;
            // 
            // тбПароль
            // 
            тбПароль.Location = new Point(97, 148);
            тбПароль.Name = "тбПароль";
            тбПароль.PasswordChar = '*';
            тбПароль.Size = new Size(138, 23);
            тбПароль.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(97, 35);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 2;
            label1.Text = "Логин:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(98, 123);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 3;
            label2.Text = "Пароль:";
            // 
            // loginButton
            // 
            loginButton.Location = new Point(97, 207);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(138, 23);
            loginButton.TabIndex = 4;
            loginButton.Text = "Войти";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // regButton
            // 
            regButton.Location = new Point(97, 236);
            regButton.Name = "regButton";
            regButton.Size = new Size(138, 23);
            regButton.TabIndex = 5;
            regButton.Text = "Зарегистрироваться";
            regButton.UseVisualStyleBackColor = true;
            regButton.Click += regButton_Click;
            // 
            // recoverButton
            // 
            recoverButton.Location = new Point(97, 265);
            recoverButton.Name = "recoverButton";
            recoverButton.Size = new Size(138, 23);
            recoverButton.TabIndex = 6;
            recoverButton.Text = "Изменить пароль";
            recoverButton.UseVisualStyleBackColor = true;
            recoverButton.Click += recoverButton_Click;
            // 
            // AuthorizationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(351, 349);
            Controls.Add(recoverButton);
            Controls.Add(regButton);
            Controls.Add(loginButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(тбПароль);
            Controls.Add(тбЛогин);
            Name = "AuthorizationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация";
            Load += AuthorizationForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox тбЛогин;
        private TextBox тбПароль;
        private Label label1;
        private Label label2;
        private Button loginButton;
        private Button regButton;
        private Button recoverButton;
    }
}