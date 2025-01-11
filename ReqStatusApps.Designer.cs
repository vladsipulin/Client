namespace Client
{
    partial class ReqStatusApps
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
            panel1 = new Panel();
            keyLbl = new Label();
            label12 = new Label();
            тбДатаОплаты = new DateTimePicker();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label7 = new Label();
            тбДатаЗаселения = new DateTimePicker();
            label8 = new Label();
            тбДатаВыезда = new DateTimePicker();
            label9 = new Label();
            label10 = new Label();
            тбСтатусЗаявки = new Label();
            тбНазваниеГостиницы = new TextBox();
            тбНомерКорпуса = new TextBox();
            тбНомерЭтажа = new TextBox();
            тбВместимость = new TextBox();
            тбНомерКомнаты = new TextBox();
            тбСтоимостьОплаты = new TextBox();
            btnForward = new Button();
            btnBack = new Button();
            button1 = new Button();
            button2 = new Button();
            тбСтатус = new Label();
            label13 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(29, 30);
            label1.Margin = new Padding(10);
            label1.Name = "label1";
            label1.Size = new Size(251, 32);
            label1.TabIndex = 0;
            label1.Text = "Содержание заявки";
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(keyLbl);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(368, 96);
            panel1.TabIndex = 1;
            // 
            // keyLbl
            // 
            keyLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            keyLbl.AutoSize = true;
            keyLbl.Location = new Point(285, 44);
            keyLbl.Name = "keyLbl";
            keyLbl.Size = new Size(59, 15);
            keyLbl.TabIndex = 30;
            keyLbl.Text = "username";
            keyLbl.Visible = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(35, 270);
            label12.Name = "label12";
            label12.Size = new Size(79, 15);
            label12.TabIndex = 31;
            label12.Text = "Дата оплаты:";
            // 
            // тбДатаОплаты
            // 
            тбДатаОплаты.Location = new Point(183, 264);
            тбДатаОплаты.Name = "тбДатаОплаты";
            тбДатаОплаты.Size = new Size(143, 23);
            тбДатаОплаты.TabIndex = 29;
            тбДатаОплаты.ValueChanged += тбДатаОплаты_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(35, 235);
            label6.Name = "label6";
            label6.Size = new Size(97, 15);
            label6.TabIndex = 23;
            label6.Text = "Номер комнаты";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 206);
            label5.Name = "label5";
            label5.Size = new Size(132, 15);
            label5.TabIndex = 22;
            label5.Text = "Вместимость комнаты";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(35, 176);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 21;
            label4.Text = "Этаж";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 147);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 20;
            label3.Text = "Корпус";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 119);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 19;
            label2.Text = "Гостиница";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(35, 299);
            label7.Name = "label7";
            label7.Size = new Size(94, 15);
            label7.TabIndex = 33;
            label7.Text = "Дата заселения:";
            // 
            // тбДатаЗаселения
            // 
            тбДатаЗаселения.Location = new Point(183, 293);
            тбДатаЗаселения.Name = "тбДатаЗаселения";
            тбДатаЗаселения.Size = new Size(143, 23);
            тбДатаЗаселения.TabIndex = 32;
            тбДатаЗаселения.ValueChanged += тбДатаЗаселения_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(35, 328);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 35;
            label8.Text = "Дата выезда:";
            // 
            // тбДатаВыезда
            // 
            тбДатаВыезда.Location = new Point(183, 322);
            тбДатаВыезда.Name = "тбДатаВыезда";
            тбДатаВыезда.Size = new Size(143, 23);
            тбДатаВыезда.TabIndex = 34;
            тбДатаВыезда.ValueChanged += тбДатаВыезда_ValueChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(35, 354);
            label9.Name = "label9";
            label9.Size = new Size(137, 15);
            label9.TabIndex = 37;
            label9.Text = "Стоимость оплаты, руб";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(35, 425);
            label10.Name = "label10";
            label10.Size = new Size(75, 20);
            label10.TabIndex = 38;
            label10.Text = "Решение:";
            // 
            // тбСтатусЗаявки
            // 
            тбСтатусЗаявки.AutoSize = true;
            тбСтатусЗаявки.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            тбСтатусЗаявки.Location = new Point(183, 425);
            тбСтатусЗаявки.Name = "тбСтатусЗаявки";
            тбСтатусЗаявки.Size = new Size(131, 20);
            тбСтатусЗаявки.TabIndex = 39;
            тбСтатусЗаявки.Text = "Заселен/Выселен";
            // 
            // тбНазваниеГостиницы
            // 
            тбНазваниеГостиницы.Location = new Point(183, 116);
            тбНазваниеГостиницы.Name = "тбНазваниеГостиницы";
            тбНазваниеГостиницы.Size = new Size(143, 23);
            тбНазваниеГостиницы.TabIndex = 40;
            // 
            // тбНомерКорпуса
            // 
            тбНомерКорпуса.Location = new Point(183, 144);
            тбНомерКорпуса.Name = "тбНомерКорпуса";
            тбНомерКорпуса.Size = new Size(143, 23);
            тбНомерКорпуса.TabIndex = 41;
            // 
            // тбНомерЭтажа
            // 
            тбНомерЭтажа.Location = new Point(183, 173);
            тбНомерЭтажа.Name = "тбНомерЭтажа";
            тбНомерЭтажа.Size = new Size(143, 23);
            тбНомерЭтажа.TabIndex = 42;
            // 
            // тбВместимость
            // 
            тбВместимость.Location = new Point(183, 203);
            тбВместимость.Name = "тбВместимость";
            тбВместимость.Size = new Size(143, 23);
            тбВместимость.TabIndex = 43;
            // 
            // тбНомерКомнаты
            // 
            тбНомерКомнаты.Location = new Point(183, 232);
            тбНомерКомнаты.Name = "тбНомерКомнаты";
            тбНомерКомнаты.Size = new Size(143, 23);
            тбНомерКомнаты.TabIndex = 44;
            // 
            // тбСтоимостьОплаты
            // 
            тбСтоимостьОплаты.Location = new Point(183, 351);
            тбСтоимостьОплаты.Name = "тбСтоимостьОплаты";
            тбСтоимостьОплаты.Size = new Size(143, 23);
            тбСтоимостьОплаты.TabIndex = 45;
            // 
            // btnForward
            // 
            btnForward.Location = new Point(84, 471);
            btnForward.Name = "btnForward";
            btnForward.Size = new Size(47, 23);
            btnForward.TabIndex = 47;
            btnForward.Text = "-->";
            btnForward.UseVisualStyleBackColor = true;
            btnForward.Click += btnForward_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(31, 471);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(47, 23);
            btnBack.TabIndex = 46;
            btnBack.Text = "<--";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // button1
            // 
            button1.Location = new Point(159, 471);
            button1.Name = "button1";
            button1.Size = new Size(83, 23);
            button1.TabIndex = 48;
            button1.Text = "Обновить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(248, 471);
            button2.Name = "button2";
            button2.Size = new Size(83, 23);
            button2.TabIndex = 49;
            button2.Text = "Удалить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // тбСтатус
            // 
            тбСтатус.AutoSize = true;
            тбСтатус.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            тбСтатус.Location = new Point(183, 394);
            тбСтатус.Name = "тбСтатус";
            тбСтатус.Size = new Size(165, 20);
            тбСтатус.TabIndex = 51;
            тбСтатус.Text = "Отмена/Ожид,/Рассм.";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(35, 394);
            label13.Name = "label13";
            label13.Size = new Size(109, 20);
            label13.TabIndex = 50;
            label13.Text = "Статус заявки:";
            // 
            // ReqStatusApps
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(368, 516);
            Controls.Add(тбСтатус);
            Controls.Add(label13);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnForward);
            Controls.Add(btnBack);
            Controls.Add(тбСтоимостьОплаты);
            Controls.Add(тбНомерКомнаты);
            Controls.Add(тбВместимость);
            Controls.Add(тбНомерЭтажа);
            Controls.Add(тбНомерКорпуса);
            Controls.Add(тбНазваниеГостиницы);
            Controls.Add(тбСтатусЗаявки);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(тбДатаВыезда);
            Controls.Add(label7);
            Controls.Add(тбДатаЗаселения);
            Controls.Add(label12);
            Controls.Add(тбДатаОплаты);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ReqStatusApps";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Статус заявки на заселение клиента";
            Load += ReqStatusApps_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        internal Label keyLbl;
        private Label label12;
        private DateTimePicker тбДатаОплаты;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label7;
        private DateTimePicker тбДатаЗаселения;
        private Label label8;
        private DateTimePicker тбДатаВыезда;
        private Label label9;
        private Label label10;
        private Label тбСтатусЗаявки;
        private TextBox тбНазваниеГостиницы;
        private TextBox тбНомерКорпуса;
        private TextBox тбНомерЭтажа;
        private TextBox тбВместимость;
        private TextBox тбНомерКомнаты;
        private TextBox тбСтоимостьОплаты;
        private Button btnForward;
        private Button btnBack;
        private Button button1;
        private Button button2;
        private Label тбСтатус;
        private Label label13;
    }
}