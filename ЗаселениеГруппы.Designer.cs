namespace Client
{
    partial class ЗаселениеГруппы
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
            lbWhoLogged = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            кбВыборСтатуса = new ComboBox();
            кбСтатус = new ComboBox();
            label12 = new Label();
            label11 = new Label();
            тбСтоимостьОплаты = new TextBox();
            тбДатаВыезда = new DateTimePicker();
            label9 = new Label();
            label4 = new Label();
            кбНС = new ComboBox();
            label10 = new Label();
            кбНГр = new ComboBox();
            label8 = new Label();
            тбДатаЗаселения = new DateTimePicker();
            label7 = new Label();
            label6 = new Label();
            кбНОрг = new ComboBox();
            checkBox1 = new CheckBox();
            label5 = new Label();
            кбНЗаселенияГруппы = new ComboBox();
            тбДатаОплаты = new DateTimePicker();
            button2 = new Button();
            label2 = new Label();
            кбНДоговора = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            label3 = new Label();
            кбНГ = new ComboBox();
            кбНК = new ComboBox();
            кбНЭ = new ComboBox();
            кбНКомнаты = new ComboBox();
            кбВместимостьКомнаты = new ComboBox();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            tbRoomPrice = new TextBox();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            button4 = new Button();
            button5 = new Button();
            кбНЗГ = new ComboBox();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Moccasin;
            panel1.Controls.Add(lbWhoLogged);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(988, 334);
            panel1.TabIndex = 58;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(1847, 28);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 51;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Moccasin;
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(кбСтатус);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(тбСтоимостьОплаты);
            groupBox1.Controls.Add(тбДатаВыезда);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(кбНС);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(кбНГр);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(тбДатаЗаселения);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(кбНОрг);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(кбНЗаселенияГруппы);
            groupBox1.Controls.Add(тбДатаОплаты);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(кбНДоговора);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(16, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(956, 291);
            groupBox1.TabIndex = 49;
            groupBox1.TabStop = false;
            groupBox1.Text = "Заселение группы";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(кбВыборСтатуса);
            groupBox2.Location = new Point(746, 89);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(188, 59);
            groupBox2.TabIndex = 61;
            groupBox2.TabStop = false;
            groupBox2.Text = "Выбор заселений со статусом";
            // 
            // кбВыборСтатуса
            // 
            кбВыборСтатуса.FormattingEnabled = true;
            кбВыборСтатуса.Items.AddRange(new object[] { "Заселить", "Выселить", "Ожидание", "Отменено" });
            кбВыборСтатуса.Location = new Point(8, 22);
            кбВыборСтатуса.Name = "кбВыборСтатуса";
            кбВыборСтатуса.Size = new Size(168, 23);
            кбВыборСтатуса.TabIndex = 71;
            кбВыборСтатуса.SelectedIndexChanged += кбВыборСтатуса_SelectedIndexChanged;
            кбВыборСтатуса.SelectionChangeCommitted += кбВыборСтатуса_SelectionChangeCommitted;
            // 
            // кбСтатус
            // 
            кбСтатус.FormattingEnabled = true;
            кбСтатус.Items.AddRange(new object[] { "Заселить", "Выселить", "Ожидание", "Отменено" });
            кбСтатус.Location = new Point(516, 250);
            кбСтатус.Name = "кбСтатус";
            кбСтатус.Size = new Size(202, 23);
            кбСтатус.TabIndex = 70;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Arial", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label12.Location = new Point(406, 253);
            label12.Name = "label12";
            label12.Size = new Size(62, 15);
            label12.TabIndex = 69;
            label12.Text = "Решение:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Arial", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label11.Location = new Point(406, 214);
            label11.Name = "label11";
            label11.Size = new Size(68, 15);
            label11.TabIndex = 68;
            label11.Text = "Итого, руб:";
            // 
            // тбСтоимостьОплаты
            // 
            тбСтоимостьОплаты.Location = new Point(516, 211);
            тбСтоимостьОплаты.Name = "тбСтоимостьОплаты";
            тбСтоимостьОплаты.Size = new Size(202, 23);
            тбСтоимостьОплаты.TabIndex = 67;
            // 
            // тбДатаВыезда
            // 
            тбДатаВыезда.Location = new Point(516, 171);
            тбДатаВыезда.Name = "тбДатаВыезда";
            тбДатаВыезда.Size = new Size(202, 23);
            тбДатаВыезда.TabIndex = 66;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(406, 177);
            label9.Name = "label9";
            label9.Size = new Size(80, 15);
            label9.TabIndex = 65;
            label9.Text = "Дата выезда:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(15, 253);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 63;
            label4.Text = "Сотрудник:";
            // 
            // кбНС
            // 
            кбНС.FormattingEnabled = true;
            кбНС.Location = new Point(166, 250);
            кбНС.Name = "кбНС";
            кбНС.Size = new Size(202, 23);
            кбНС.TabIndex = 64;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(15, 214);
            label10.Name = "label10";
            label10.Size = new Size(92, 15);
            label10.TabIndex = 61;
            label10.Text = "Номер группы:";
            // 
            // кбНГр
            // 
            кбНГр.FormattingEnabled = true;
            кбНГр.Location = new Point(166, 211);
            кбНГр.Name = "кбНГр";
            кбНГр.Size = new Size(202, 23);
            кбНГр.TabIndex = 62;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(1314, 257);
            label8.Name = "label8";
            label8.Size = new Size(59, 15);
            label8.TabIndex = 58;
            label8.Text = "username";
            label8.Visible = false;
            // 
            // тбДатаЗаселения
            // 
            тбДатаЗаселения.Location = new Point(516, 129);
            тбДатаЗаселения.Name = "тбДатаЗаселения";
            тбДатаЗаселения.Size = new Size(202, 23);
            тбДатаЗаселения.TabIndex = 44;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(406, 133);
            label7.Name = "label7";
            label7.Size = new Size(96, 15);
            label7.TabIndex = 43;
            label7.Text = "Дата заселения:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(15, 174);
            label6.Name = "label6";
            label6.Size = new Size(83, 15);
            label6.TabIndex = 40;
            label6.Text = "Организация:";
            // 
            // кбНОрг
            // 
            кбНОрг.FormattingEnabled = true;
            кбНОрг.Location = new Point(166, 171);
            кбНОрг.Name = "кбНОрг";
            кбНОрг.Size = new Size(202, 23);
            кбНОрг.TabIndex = 41;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(15, 43);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(164, 19);
            checkBox1.TabIndex = 39;
            checkBox1.Text = "Новое заселение группы";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(15, 95);
            label5.Name = "label5";
            label5.Size = new Size(108, 15);
            label5.TabIndex = 37;
            label5.Text = "Номер заселения:";
            // 
            // кбНЗаселенияГруппы
            // 
            кбНЗаселенияГруппы.FormattingEnabled = true;
            кбНЗаселенияГруппы.Location = new Point(166, 92);
            кбНЗаселенияГруппы.Name = "кбНЗаселенияГруппы";
            кбНЗаселенияГруппы.Size = new Size(202, 23);
            кбНЗаселенияГруппы.TabIndex = 38;
            кбНЗаселенияГруппы.SelectionChangeCommitted += кбНЗаселенияГруппы_SelectionChangeCommitted;
            // 
            // тбДатаОплаты
            // 
            тбДатаОплаты.Enabled = false;
            тбДатаОплаты.Location = new Point(516, 89);
            тбДатаОплаты.Name = "тбДатаОплаты";
            тбДатаОплаты.Size = new Size(202, 23);
            тбДатаОплаты.TabIndex = 35;
            // 
            // button2
            // 
            button2.Location = new Point(746, 171);
            button2.Name = "button2";
            button2.Size = new Size(188, 23);
            button2.TabIndex = 33;
            button2.Text = "Изменить сведения";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(15, 135);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 27;
            label2.Text = "Код договора:";
            // 
            // кбНДоговора
            // 
            кбНДоговора.FormattingEnabled = true;
            кбНДоговора.Location = new Point(166, 132);
            кбНДоговора.Name = "кбНДоговора";
            кбНДоговора.Size = new Size(202, 23);
            кбНДоговора.TabIndex = 28;
            кбНДоговора.SelectionChangeCommitted += кбНДоговора_SelectionChangeCommitted;
            // 
            // button1
            // 
            button1.Location = new Point(188, 40);
            button1.Name = "button1";
            button1.Size = new Size(99, 23);
            button1.TabIndex = 32;
            button1.Text = "Заселить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(406, 93);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 30;
            label1.Text = "Дата оплаты:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(16, 347);
            label3.Name = "label3";
            label3.Size = new Size(287, 20);
            label3.TabIndex = 59;
            label3.Text = "Выбор комнат в заявку бронирования:";
            // 
            // кбНГ
            // 
            кбНГ.FormattingEnabled = true;
            кбНГ.Location = new Point(102, 388);
            кбНГ.Name = "кбНГ";
            кбНГ.Size = new Size(143, 23);
            кбНГ.TabIndex = 69;
            кбНГ.SelectionChangeCommitted += кбНГ_SelectionChangeCommitted;
            // 
            // кбНК
            // 
            кбНК.FormattingEnabled = true;
            кбНК.Location = new Point(102, 416);
            кбНК.Name = "кбНК";
            кбНК.Size = new Size(143, 23);
            кбНК.TabIndex = 68;
            кбНК.SelectionChangeCommitted += кбНК_SelectionChangeCommitted;
            // 
            // кбНЭ
            // 
            кбНЭ.FormattingEnabled = true;
            кбНЭ.Location = new Point(102, 445);
            кбНЭ.Name = "кбНЭ";
            кбНЭ.Size = new Size(143, 23);
            кбНЭ.TabIndex = 67;
            кбНЭ.SelectionChangeCommitted += кбНЭ_SelectionChangeCommitted;
            // 
            // кбНКомнаты
            // 
            кбНКомнаты.FormattingEnabled = true;
            кбНКомнаты.Location = new Point(423, 416);
            кбНКомнаты.Name = "кбНКомнаты";
            кбНКомнаты.Size = new Size(143, 23);
            кбНКомнаты.TabIndex = 66;
            // 
            // кбВместимостьКомнаты
            // 
            кбВместимостьКомнаты.FormattingEnabled = true;
            кбВместимостьКомнаты.Location = new Point(423, 387);
            кбВместимостьКомнаты.Name = "кбВместимостьКомнаты";
            кбВместимостьКомнаты.Size = new Size(143, 23);
            кбВместимостьКомнаты.TabIndex = 65;
            кбВместимостьКомнаты.SelectionChangeCommitted += кбВместимостьКомнаты_SelectionChangeCommitted;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(275, 419);
            label13.Name = "label13";
            label13.Size = new Size(97, 15);
            label13.TabIndex = 64;
            label13.Text = "Номер комнаты";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(275, 390);
            label14.Name = "label14";
            label14.Size = new Size(132, 15);
            label14.TabIndex = 63;
            label14.Text = "Вместимость комнаты";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(22, 448);
            label15.Name = "label15";
            label15.Size = new Size(34, 15);
            label15.TabIndex = 62;
            label15.Text = "Этаж";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(22, 419);
            label16.Name = "label16";
            label16.Size = new Size(47, 15);
            label16.TabIndex = 61;
            label16.Text = "Корпус";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(22, 391);
            label17.Name = "label17";
            label17.Size = new Size(65, 15);
            label17.TabIndex = 60;
            label17.Text = "Гостиница";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(275, 448);
            label18.Name = "label18";
            label18.Size = new Size(117, 15);
            label18.TabIndex = 71;
            label18.Text = "Цена за 1 ночь, руб:";
            // 
            // tbRoomPrice
            // 
            tbRoomPrice.Location = new Point(423, 445);
            tbRoomPrice.Name = "tbRoomPrice";
            tbRoomPrice.Size = new Size(143, 23);
            tbRoomPrice.TabIndex = 70;
            // 
            // button3
            // 
            button3.Location = new Point(611, 387);
            button3.Name = "button3";
            button3.Size = new Size(171, 23);
            button3.TabIndex = 72;
            button3.Text = "Забронировать комнату";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 487);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(956, 141);
            dataGridView1.TabIndex = 73;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // button4
            // 
            button4.Location = new Point(611, 416);
            button4.Name = "button4";
            button4.Size = new Size(171, 23);
            button4.TabIndex = 74;
            button4.Text = "Изменить сведения";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(611, 445);
            button5.Name = "button5";
            button5.Size = new Size(171, 23);
            button5.TabIndex = 75;
            button5.Text = "Убрать из заселения";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // кбНЗГ
            // 
            кбНЗГ.FormattingEnabled = true;
            кбНЗГ.Location = new Point(829, 388);
            кбНЗГ.Name = "кбНЗГ";
            кбНЗГ.Size = new Size(121, 23);
            кбНЗГ.TabIndex = 76;
            // 
            // ЗаселениеГруппы
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(988, 640);
            Controls.Add(кбНЗГ);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(dataGridView1);
            Controls.Add(button3);
            Controls.Add(кбНГ);
            Controls.Add(кбНК);
            Controls.Add(кбНЭ);
            Controls.Add(кбНКомнаты);
            Controls.Add(кбВместимостьКомнаты);
            Controls.Add(label13);
            Controls.Add(label14);
            Controls.Add(label15);
            Controls.Add(label16);
            Controls.Add(label17);
            Controls.Add(label18);
            Controls.Add(tbRoomPrice);
            Controls.Add(panel1);
            Controls.Add(label3);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ЗаселениеГруппы";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Управление заселением групп";
            Load += ЗаселениеГруппы_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        internal Label lbWhoLogged;
        private GroupBox groupBox1;
        private Label label10;
        private ComboBox кбНГр;
        internal Label label8;
        private DateTimePicker тбДатаЗаселения;
        private Label label7;
        private Label label6;
        private ComboBox кбНОрг;
        private CheckBox checkBox1;
        private Label label5;
        private ComboBox кбНЗаселенияГруппы;
        private DateTimePicker тбДатаОплаты;
        private Button button2;
        private Label label2;
        private ComboBox кбНДоговора;
        private Button button1;
        private Label label1;
        private Label label3;
        private Label label4;
        private ComboBox кбНС;
        private DateTimePicker тбДатаВыезда;
        private Label label9;
        private Label label11;
        private TextBox тбСтоимостьОплаты;
        private Label label12;
        private ComboBox кбСтатус;
        private GroupBox groupBox2;
        private ComboBox кбВыборСтатуса;
        private ComboBox кбНГ;
        private ComboBox кбНК;
        private ComboBox кбНЭ;
        private ComboBox кбНКомнаты;
        private ComboBox кбВместимостьКомнаты;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private TextBox tbRoomPrice;
        private Button button3;
        private DataGridView dataGridView1;
        private Button button4;
        private Button button5;
        private ComboBox кбНЗГ;
    }
}