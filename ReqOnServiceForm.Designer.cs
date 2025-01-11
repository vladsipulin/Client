namespace Client
{
    partial class ReqOnServiceForm
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
            кбНСл = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            кбНКл = new Label();
            тбНКл = new TextBox();
            label9 = new Label();
            тбСумма = new TextBox();
            label7 = new Label();
            тбСрокОплаты = new DateTimePicker();
            panel1 = new Panel();
            label5 = new Label();
            тбЦенаСлужбы = new TextBox();
            lbWhoLogged = new Label();
            тбДатаЗаявки = new DateTimePicker();
            label3 = new Label();
            label4 = new Label();
            тбКолво = new TextBox();
            button1 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // кбНСл
            // 
            кбНСл.FormattingEnabled = true;
            кбНСл.Location = new Point(181, 90);
            кбНСл.Name = "кбНСл";
            кбНСл.Size = new Size(143, 23);
            кбНСл.TabIndex = 26;
            кбНСл.SelectionChangeCommitted += кбНСл_SelectionChangeCommitted;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 93);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 25;
            label2.Text = "Служба быта:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(19, 31);
            label1.Margin = new Padding(10);
            label1.Name = "label1";
            label1.Size = new Size(375, 32);
            label1.TabIndex = 24;
            label1.Text = "Оформление заявки на услугу";
            // 
            // кбНКл
            // 
            кбНКл.AutoSize = true;
            кбНКл.Location = new Point(31, 153);
            кбНКл.Name = "кбНКл";
            кбНКл.Size = new Size(95, 15);
            кбНКл.TabIndex = 32;
            кбНКл.Text = "Номер клиента:";
            // 
            // тбНКл
            // 
            тбНКл.Location = new Point(181, 150);
            тбНКл.Name = "тбНКл";
            тбНКл.Size = new Size(143, 23);
            тбНКл.TabIndex = 31;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label9.Location = new Point(33, 305);
            label9.Name = "label9";
            label9.Size = new Size(127, 15);
            label9.TabIndex = 30;
            label9.Text = "Сумма за услугу, руб:";
            // 
            // тбСумма
            // 
            тбСумма.Location = new Point(181, 302);
            тбСумма.Name = "тбСумма";
            тбСумма.Size = new Size(143, 23);
            тбСумма.TabIndex = 29;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(33, 215);
            label7.Name = "label7";
            label7.Size = new Size(82, 15);
            label7.TabIndex = 27;
            label7.Text = "Срок оплаты:";
            // 
            // тбСрокОплаты
            // 
            тбСрокОплаты.Location = new Point(181, 209);
            тбСрокОплаты.Name = "тбСрокОплаты";
            тбСрокОплаты.Size = new Size(143, 23);
            тбСрокОплаты.TabIndex = 28;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Controls.Add(label5);
            panel1.Controls.Add(тбЦенаСлужбы);
            panel1.Controls.Add(lbWhoLogged);
            panel1.Controls.Add(тбДатаЗаявки);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(кбНКл);
            panel1.Controls.Add(кбНСл);
            panel1.Controls.Add(тбНКл);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(687, 187);
            panel1.TabIndex = 33;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(31, 122);
            label5.Name = "label5";
            label5.Size = new Size(85, 15);
            label5.TabIndex = 41;
            label5.Text = "Цена службы:";
            // 
            // тбЦенаСлужбы
            // 
            тбЦенаСлужбы.Location = new Point(181, 119);
            тбЦенаСлужбы.Name = "тбЦенаСлужбы";
            тбЦенаСлужбы.Size = new Size(143, 23);
            тбЦенаСлужбы.TabIndex = 40;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(407, 18);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 39;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // тбДатаЗаявки
            // 
            тбДатаЗаявки.Location = new Point(519, 40);
            тбДатаЗаявки.Name = "тбДатаЗаявки";
            тбДатаЗаявки.Size = new Size(143, 23);
            тбДатаЗаявки.TabIndex = 35;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(439, 45);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 34;
            label3.Text = "Дата заявки:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 260);
            label4.Name = "label4";
            label4.Size = new Size(93, 15);
            label4.TabIndex = 37;
            label4.Text = "Количество, ед:";
            // 
            // тбКолво
            // 
            тбКолво.Location = new Point(181, 257);
            тбКолво.Name = "тбКолво";
            тбКолво.Size = new Size(143, 23);
            тбКолво.TabIndex = 36;
            тбКолво.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(519, 347);
            button1.Name = "button1";
            button1.Size = new Size(143, 35);
            button1.TabIndex = 38;
            button1.Text = "Оформить заявку";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ReqOnServiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(687, 394);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(panel1);
            Controls.Add(тбКолво);
            Controls.Add(label9);
            Controls.Add(тбСумма);
            Controls.Add(label7);
            Controls.Add(тбСрокОплаты);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ReqOnServiceForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Заявка на услугу";
            Load += ReqOnService_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox кбНСл;
        private Label label2;
        private Label label1;
        private Label кбНКл;
        private TextBox тбНКл;
        private Label label9;
        private TextBox тбСумма;
        private Label label7;
        private DateTimePicker тбСрокОплаты;
        private Panel panel1;
        private DateTimePicker тбДатаЗаявки;
        private Label label3;
        private Label label4;
        private TextBox тбКолво;
        private Button button1;
        internal Label lbWhoLogged;
        private Label label5;
        private TextBox тбЦенаСлужбы;
    }
}