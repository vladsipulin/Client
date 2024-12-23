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
            this.тбЛогин = new System.Windows.Forms.TextBox();
            this.тбПароль = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.regButton = new System.Windows.Forms.Button();
            this.recoverButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // тбЛогин
            // 
            this.тбЛогин.Location = new System.Drawing.Point(97, 64);
            this.тбЛогин.Name = "тбЛогин";
            this.тбЛогин.Size = new System.Drawing.Size(138, 23);
            this.тбЛогин.TabIndex = 0;
            // 
            // тбПароль
            // 
            this.тбПароль.Location = new System.Drawing.Point(97, 148);
            this.тбПароль.Name = "тбПароль";
            this.тбПароль.PasswordChar = '*';
            this.тбПароль.Size = new System.Drawing.Size(138, 23);
            this.тбПароль.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Логин:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль:";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(97, 207);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(138, 23);
            this.loginButton.TabIndex = 4;
            this.loginButton.Text = "Войти";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // regButton
            // 
            this.regButton.Location = new System.Drawing.Point(97, 236);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(138, 23);
            this.regButton.TabIndex = 5;
            this.regButton.Text = "Зарегистрироваться";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.regButton_Click);
            // 
            // recoverButton
            // 
            this.recoverButton.Location = new System.Drawing.Point(97, 265);
            this.recoverButton.Name = "recoverButton";
            this.recoverButton.Size = new System.Drawing.Size(138, 23);
            this.recoverButton.TabIndex = 6;
            this.recoverButton.Text = "Изменить пароль";
            this.recoverButton.UseVisualStyleBackColor = true;
            this.recoverButton.Click += new System.EventHandler(this.recoverButton_Click);
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 349);
            this.Controls.Add(this.recoverButton);
            this.Controls.Add(this.regButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.тбПароль);
            this.Controls.Add(this.тбЛогин);
            this.Name = "AuthorizationForm";
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.AuthorizationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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