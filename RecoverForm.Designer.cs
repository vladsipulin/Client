namespace Client
{
    partial class RecoverForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.тбНП = new System.Windows.Forms.TextBox();
            this.regButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.тбКВ = new System.Windows.Forms.TextBox();
            this.тбПочта = new System.Windows.Forms.TextBox();
            this.changePassBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Новый пароль:";
            // 
            // тбНП
            // 
            this.тбНП.Enabled = false;
            this.тбНП.Location = new System.Drawing.Point(45, 154);
            this.тбНП.Name = "тбНП";
            this.тбНП.Size = new System.Drawing.Size(173, 23);
            this.тбНП.TabIndex = 14;
            // 
            // regButton
            // 
            this.regButton.Location = new System.Drawing.Point(46, 192);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(172, 38);
            this.regButton.TabIndex = 13;
            this.regButton.Text = "Получить код восстановления";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.regButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Код из сообщения:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Электронная почта:";
            // 
            // тбКВ
            // 
            this.тбКВ.Location = new System.Drawing.Point(46, 92);
            this.тбКВ.Name = "тбКВ";
            this.тбКВ.Size = new System.Drawing.Size(172, 23);
            this.тбКВ.TabIndex = 10;
            // 
            // тбПочта
            // 
            this.тбПочта.Location = new System.Drawing.Point(45, 37);
            this.тбПочта.Name = "тбПочта";
            this.тбПочта.Size = new System.Drawing.Size(173, 23);
            this.тбПочта.TabIndex = 9;
            // 
            // changePassBtn
            // 
            this.changePassBtn.Location = new System.Drawing.Point(46, 236);
            this.changePassBtn.Name = "changePassBtn";
            this.changePassBtn.Size = new System.Drawing.Size(172, 38);
            this.changePassBtn.TabIndex = 16;
            this.changePassBtn.Text = "Изменить пароль";
            this.changePassBtn.UseVisualStyleBackColor = true;
            this.changePassBtn.Click += new System.EventHandler(this.changePassBtn_Click);
            // 
            // RecoverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 299);
            this.Controls.Add(this.changePassBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.тбНП);
            this.Controls.Add(this.regButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.тбКВ);
            this.Controls.Add(this.тбПочта);
            this.Name = "RecoverForm";
            this.Text = "Изменение пароля";
            this.Load += new System.EventHandler(this.RecoverForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label3;
        private TextBox тбНП;
        private Button regButton;
        private Label label2;
        private Label label1;
        private TextBox тбКВ;
        private TextBox тбПочта;
        private Button changePassBtn;
    }
}