namespace Client
{
    partial class ClientQueriesForAppartments
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
            panel1 = new Panel();
            dateTimePicker1 = new DateTimePicker();
            кбНГ = new ComboBox();
            кбНК = new ComboBox();
            кбНЭ = new ComboBox();
            кбНКомнаты = new ComboBox();
            кбВместимостьКомнаты = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dateTimePicker2 = new DateTimePicker();
            label7 = new Label();
            label8 = new Label();
            dateTimePicker3 = new DateTimePicker();
            tbCost = new TextBox();
            label9 = new Label();
            button1 = new Button();
            tbRoomPrice = new TextBox();
            label10 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(кбНГ);
            panel1.Controls.Add(кбНК);
            panel1.Controls.Add(кбНЭ);
            panel1.Controls.Add(кбНКомнаты);
            panel1.Controls.Add(кбВместимостьКомнаты);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(649, 252);
            panel1.TabIndex = 8;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(483, 27);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(143, 23);
            dateTimePicker1.TabIndex = 15;
            // 
            // кбНГ
            // 
            кбНГ.FormattingEnabled = true;
            кбНГ.Location = new Point(181, 78);
            кбНГ.Name = "кбНГ";
            кбНГ.Size = new Size(143, 23);
            кбНГ.TabIndex = 14;
            кбНГ.SelectionChangeCommitted += кбНГ_SelectionChangeCommitted;
            // 
            // кбНК
            // 
            кбНК.FormattingEnabled = true;
            кбНК.Location = new Point(181, 107);
            кбНК.Name = "кбНК";
            кбНК.Size = new Size(143, 23);
            кбНК.TabIndex = 13;
            кбНК.SelectionChangeCommitted += кбНК_SelectionChangeCommitted;
            // 
            // кбНЭ
            // 
            кбНЭ.FormattingEnabled = true;
            кбНЭ.Location = new Point(181, 135);
            кбНЭ.Name = "кбНЭ";
            кбНЭ.Size = new Size(143, 23);
            кбНЭ.TabIndex = 12;
            кбНЭ.SelectionChangeCommitted += кбНЭ_SelectionChangeCommitted;
            // 
            // кбНКомнаты
            // 
            кбНКомнаты.FormattingEnabled = true;
            кбНКомнаты.Location = new Point(181, 194);
            кбНКомнаты.Name = "кбНКомнаты";
            кбНКомнаты.Size = new Size(143, 23);
            кбНКомнаты.TabIndex = 11;
            кбНКомнаты.SelectionChangeCommitted += кбНКомнаты_SelectionChangeCommitted;
            // 
            // кбВместимостьКомнаты
            // 
            кбВместимостьКомнаты.FormattingEnabled = true;
            кбВместимостьКомнаты.Location = new Point(181, 165);
            кбВместимостьКомнаты.Name = "кбВместимостьКомнаты";
            кбВместимостьКомнаты.Size = new Size(143, 23);
            кбВместимостьКомнаты.TabIndex = 10;
            кбВместимостьКомнаты.SelectionChangeCommitted += кбВместимостьКомнаты_SelectionChangeCommitted;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(33, 197);
            label6.Name = "label6";
            label6.Size = new Size(97, 15);
            label6.TabIndex = 9;
            label6.Text = "Номер комнаты";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 168);
            label5.Name = "label5";
            label5.Size = new Size(132, 15);
            label5.TabIndex = 8;
            label5.Text = "Вместимость комнаты";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 138);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 6;
            label4.Text = "Этаж";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 109);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 3;
            label3.Text = "Корпус";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 81);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 2;
            label2.Text = "Гостиница";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(30, 19);
            label1.Margin = new Padding(10);
            label1.Name = "label1";
            label1.Size = new Size(420, 32);
            label1.TabIndex = 0;
            label1.Text = "Оформление заявки на заселение";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(181, 271);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(143, 23);
            dateTimePicker2.TabIndex = 16;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(33, 277);
            label7.Name = "label7";
            label7.Size = new Size(94, 15);
            label7.TabIndex = 16;
            label7.Text = "Дата заселения:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(33, 310);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 17;
            label8.Text = "Дата выезда:";
            // 
            // dateTimePicker3
            // 
            dateTimePicker3.Location = new Point(181, 304);
            dateTimePicker3.Name = "dateTimePicker3";
            dateTimePicker3.Size = new Size(143, 23);
            dateTimePicker3.TabIndex = 18;
            dateTimePicker3.ValueChanged += dateTimePicker3_ValueChanged;
            // 
            // tbCost
            // 
            tbCost.Location = new Point(181, 392);
            tbCost.Name = "tbCost";
            tbCost.Size = new Size(143, 23);
            tbCost.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label9.Location = new Point(33, 395);
            label9.Name = "label9";
            label9.Size = new Size(142, 15);
            label9.TabIndex = 20;
            label9.Text = "Стоимость оплаты, руб:";
            // 
            // button1
            // 
            button1.Location = new Point(483, 416);
            button1.Name = "button1";
            button1.Size = new Size(143, 35);
            button1.TabIndex = 21;
            button1.Text = "Оформить заявку";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // tbRoomPrice
            // 
            tbRoomPrice.Location = new Point(181, 338);
            tbRoomPrice.Name = "tbRoomPrice";
            tbRoomPrice.Size = new Size(143, 23);
            tbRoomPrice.TabIndex = 22;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(33, 341);
            label10.Name = "label10";
            label10.Size = new Size(117, 15);
            label10.TabIndex = 23;
            label10.Text = "Цена за 1 ночь, руб:";
            // 
            // ClientQueriesForAppartments
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(649, 468);
            Controls.Add(label10);
            Controls.Add(tbRoomPrice);
            Controls.Add(button1);
            Controls.Add(label9);
            Controls.Add(tbCost);
            Controls.Add(dateTimePicker3);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(dateTimePicker2);
            Controls.Add(panel1);
            Name = "ClientQueriesForAppartments";
            Text = "Заявка на заселение";
            Load += ClientQueriesForAppartments_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox кбНГ;
        private ComboBox кбНК;
        private ComboBox кбНЭ;
        private ComboBox кбНКомнаты;
        private ComboBox кбВместимостьКомнаты;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private Label label7;
        private Label label8;
        private DateTimePicker dateTimePicker3;
        private TextBox tbCost;
        private Label label9;
        private Button button1;
        private TextBox tbRoomPrice;
        private Label label10;
    }
}