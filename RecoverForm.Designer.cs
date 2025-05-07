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
            label3 = new Label();
            тбНП = new TextBox();
            regButton = new Button();
            label2 = new Label();
            label1 = new Label();
            тбКВ = new TextBox();
            тбПочта = new TextBox();
            changePassBtn = new Button();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(72, 144);
            label3.Name = "label3";
            label3.Size = new Size(91, 15);
            label3.TabIndex = 15;
            label3.Text = "Новый пароль:";
            // 
            // тбНП
            // 
            тбНП.Enabled = false;
            тбНП.Location = new Point(72, 162);
            тбНП.Name = "тбНП";
            тбНП.Size = new Size(173, 23);
            тбНП.TabIndex = 14;
            // 
            // regButton
            // 
            regButton.Location = new Point(73, 200);
            regButton.Name = "regButton";
            regButton.Size = new Size(172, 38);
            regButton.TabIndex = 13;
            regButton.Text = "Получить код восстановления";
            regButton.UseVisualStyleBackColor = true;
            regButton.Click += regButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(72, 82);
            label2.Name = "label2";
            label2.Size = new Size(112, 15);
            label2.TabIndex = 12;
            label2.Text = "Код из сообщения:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(72, 27);
            label1.Name = "label1";
            label1.Size = new Size(116, 15);
            label1.TabIndex = 11;
            label1.Text = "Электронная почта:";
            // 
            // тбКВ
            // 
            тбКВ.Location = new Point(73, 100);
            тбКВ.Name = "тбКВ";
            тбКВ.Size = new Size(172, 23);
            тбКВ.TabIndex = 10;
            // 
            // тбПочта
            // 
            тбПочта.Location = new Point(72, 45);
            тбПочта.Name = "тбПочта";
            тбПочта.Size = new Size(173, 23);
            тбПочта.TabIndex = 9;
            // 
            // changePassBtn
            // 
            changePassBtn.Location = new Point(73, 244);
            changePassBtn.Name = "changePassBtn";
            changePassBtn.Size = new Size(172, 38);
            changePassBtn.TabIndex = 16;
            changePassBtn.Text = "Изменить пароль";
            changePassBtn.UseVisualStyleBackColor = true;
            changePassBtn.Click += changePassBtn_Click;
            // 
            // RecoverForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(316, 308);
            Controls.Add(changePassBtn);
            Controls.Add(label3);
            Controls.Add(тбНП);
            Controls.Add(regButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(тбКВ);
            Controls.Add(тбПочта);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "RecoverForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Восстановление доступа к аккаунту портье";
            Load += RecoverForm_Load;
            ResumeLayout(false);
            PerformLayout();
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