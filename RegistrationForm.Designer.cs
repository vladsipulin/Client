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
            тбЛогин = new TextBox();
            тбПароль = new TextBox();
            label2 = new Label();
            label1 = new Label();
            regButton = new Button();
            тбПочта = new TextBox();
            label3 = new Label();
            label4 = new Label();
            тбФИО = new TextBox();
            label5 = new Label();
            label6 = new Label();
            полеДР = new DateTimePicker();
            кбПол = new ComboBox();
            label7 = new Label();
            тбID = new TextBox();
            SuspendLayout();
            // 
            // тбЛогин
            // 
            тбЛогин.Location = new Point(52, 91);
            тбЛогин.Name = "тбЛогин";
            тбЛогин.Size = new Size(175, 23);
            тбЛогин.TabIndex = 0;
            // 
            // тбПароль
            // 
            тбПароль.Location = new Point(52, 146);
            тбПароль.Name = "тбПароль";
            тбПароль.Size = new Size(174, 23);
            тбПароль.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 128);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 5;
            label2.Text = "Пароль:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 73);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 4;
            label1.Text = "Логин:";
            // 
            // regButton
            // 
            regButton.Location = new Point(54, 463);
            regButton.Name = "regButton";
            regButton.Size = new Size(173, 35);
            regButton.TabIndex = 6;
            regButton.Text = "Добавить портье";
            regButton.UseVisualStyleBackColor = true;
            regButton.Click += regButton_Click;
            // 
            // тбПочта
            // 
            тбПочта.Location = new Point(52, 204);
            тбПочта.Name = "тбПочта";
            тбПочта.Size = new Size(174, 23);
            тбПочта.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(53, 186);
            label3.Name = "label3";
            label3.Size = new Size(116, 15);
            label3.TabIndex = 8;
            label3.Text = "Электронная почта:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(53, 247);
            label4.Name = "label4";
            label4.Size = new Size(142, 15);
            label4.TabIndex = 10;
            label4.Text = "Фамилия Имя Отчество:";
            // 
            // тбФИО
            // 
            тбФИО.Location = new Point(52, 265);
            тбФИО.Name = "тбФИО";
            тбФИО.Size = new Size(174, 23);
            тбФИО.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(53, 307);
            label5.Name = "label5";
            label5.Size = new Size(33, 15);
            label5.TabIndex = 12;
            label5.Text = "Пол:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(52, 361);
            label6.Name = "label6";
            label6.Size = new Size(93, 15);
            label6.TabIndex = 14;
            label6.Text = "Дата рождения:";
            // 
            // полеДР
            // 
            полеДР.Location = new Point(52, 386);
            полеДР.Name = "полеДР";
            полеДР.Size = new Size(174, 23);
            полеДР.TabIndex = 15;
            // 
            // кбПол
            // 
            кбПол.FormattingEnabled = true;
            кбПол.Items.AddRange(new object[] { "муж.", "жен." });
            кбПол.Location = new Point(52, 325);
            кбПол.Name = "кбПол";
            кбПол.Size = new Size(174, 23);
            кбПол.TabIndex = 16;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(54, 20);
            label7.Name = "label7";
            label7.Size = new Size(89, 15);
            label7.TabIndex = 18;
            label7.Text = "Номер портье:";
            // 
            // тбID
            // 
            тбID.Location = new Point(52, 38);
            тбID.Name = "тбID";
            тбID.Size = new Size(175, 23);
            тбID.TabIndex = 17;
            // 
            // RegistrationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(287, 526);
            Controls.Add(label7);
            Controls.Add(тбID);
            Controls.Add(кбПол);
            Controls.Add(полеДР);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(тбФИО);
            Controls.Add(label3);
            Controls.Add(тбПочта);
            Controls.Add(regButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(тбПароль);
            Controls.Add(тбЛогин);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "RegistrationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Создание нового портье";
            Load += RegistrationForm_Load;
            ResumeLayout(false);
            PerformLayout();
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
        private TextBox тбID;
    }
}