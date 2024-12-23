namespace Client
{
    partial class RegistrationForm
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
            this.тбЛогин = new System.Windows.Forms.TextBox();
            this.тбПароль = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.regButton = new System.Windows.Forms.Button();
            this.тбПочта = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.тбФИО = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.полеДР = new System.Windows.Forms.DateTimePicker();
            this.кбПол = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.тбНКл = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // тбЛогин
            // 
            this.тбЛогин.Location = new System.Drawing.Point(54, 91);
            this.тбЛогин.Name = "тбЛогин";
            this.тбЛогин.Size = new System.Drawing.Size(173, 23);
            this.тбЛогин.TabIndex = 0;
            // 
            // тбПароль
            // 
            this.тбПароль.Location = new System.Drawing.Point(54, 146);
            this.тбПароль.Name = "тбПароль";
            this.тбПароль.Size = new System.Drawing.Size(172, 23);
            this.тбПароль.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Пароль:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Логин:";
            // 
            // regButton
            // 
            this.regButton.Location = new System.Drawing.Point(54, 463);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(173, 35);
            this.regButton.TabIndex = 6;
            this.regButton.Text = "Зарегистрироваться";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.regButton_Click);
            // 
            // тбПочта
            // 
            this.тбПочта.Location = new System.Drawing.Point(54, 204);
            this.тбПочта.Name = "тбПочта";
            this.тбПочта.Size = new System.Drawing.Size(172, 23);
            this.тбПочта.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Электронная почта:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "ФИО";
            // 
            // тбФИО
            // 
            this.тбФИО.Location = new System.Drawing.Point(54, 265);
            this.тбФИО.Name = "тбФИО";
            this.тбФИО.Size = new System.Drawing.Size(172, 23);
            this.тбФИО.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 307);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Пол";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 370);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Дата рождения";
            // 
            // полеДР
            // 
            this.полеДР.Location = new System.Drawing.Point(54, 397);
            this.полеДР.Name = "полеДР";
            this.полеДР.Size = new System.Drawing.Size(172, 23);
            this.полеДР.TabIndex = 15;
            // 
            // кбПол
            // 
            this.кбПол.FormattingEnabled = true;
            this.кбПол.Items.AddRange(new object[] {
            "муж.",
            "жен."});
            this.кбПол.Location = new System.Drawing.Point(54, 325);
            this.кбПол.Name = "кбПол";
            this.кбПол.Size = new System.Drawing.Size(172, 23);
            this.кбПол.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "Номер клиента:";
            // 
            // тбНКл
            // 
            this.тбНКл.Location = new System.Drawing.Point(53, 38);
            this.тбНКл.Name = "тбНКл";
            this.тбНКл.Size = new System.Drawing.Size(174, 23);
            this.тбНКл.TabIndex = 17;
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 536);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.тбНКл);
            this.Controls.Add(this.кбПол);
            this.Controls.Add(this.полеДР);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.тбФИО);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.тбПочта);
            this.Controls.Add(this.regButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.тбПароль);
            this.Controls.Add(this.тбЛогин);
            this.Name = "RegistrationForm";
            this.Text = "Регистрация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox тбЛогин;
        private TextBox тбПароль;
        private Label label2;
        private Label label1;
        private Button regButton;
        private TextBox тбПочта;
        private Label label3;
        private Label label4;
        private TextBox тбФИО;
        private Label label5;
        private Label label6;
        private DateTimePicker полеДР;
        private ComboBox кбПол;
        private Label label7;
        private TextBox тбНКл;
    }
}