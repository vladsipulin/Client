namespace Client
{
    partial class УчетПокупокУслугКлиентов
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
            label2 = new Label();
            кбНЗаявки = new ComboBox();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            label1 = new Label();
            groupBox1 = new GroupBox();
            кбНЗаявкиКлон = new ComboBox();
            тбСрокОплаты = new DateTimePicker();
            checkBox1 = new CheckBox();
            тбСтоимостьОплаты = new TextBox();
            тбШтраф = new TextBox();
            тбДатаОплаты = new DateTimePicker();
            label5 = new Label();
            lbWhoLogged = new Label();
            label7 = new Label();
            label6 = new Label();
            кбНС = new ComboBox();
            label4 = new Label();
            кбНКл = new ComboBox();
            button3 = new Button();
            button2 = new Button();
            label3 = new Label();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(18, 63);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 27;
            label2.Text = "Номер заявки:";
            // 
            // кбНЗаявки
            // 
            кбНЗаявки.FormattingEnabled = true;
            кбНЗаявки.Location = new Point(15, 81);
            кбНЗаявки.Name = "кбНЗаявки";
            кбНЗаявки.Size = new Size(128, 23);
            кбНЗаявки.TabIndex = 28;
            кбНЗаявки.SelectionChangeCommitted += кбНЗаявки_SelectionChangeCommitted;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 377);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(686, 136);
            dataGridView1.TabIndex = 50;
            // 
            // button1
            // 
            button1.Location = new Point(422, 81);
            button1.Name = "button1";
            button1.Size = new Size(113, 23);
            button1.TabIndex = 32;
            button1.Text = "Создать запись";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(16, 132);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 30;
            label1.Text = "Клиент в заявке:";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Moccasin;
            groupBox1.Controls.Add(кбНЗаявкиКлон);
            groupBox1.Controls.Add(тбСрокОплаты);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(тбСтоимостьОплаты);
            groupBox1.Controls.Add(тбШтраф);
            groupBox1.Controls.Add(тбДатаОплаты);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(lbWhoLogged);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(кбНС);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(кбНКл);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(кбНЗаявки);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(16, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(686, 291);
            groupBox1.TabIndex = 53;
            groupBox1.TabStop = false;
            groupBox1.Text = "Учет заявки клиента на услугу";
            // 
            // кбНЗаявкиКлон
            // 
            кбНЗаявкиКлон.Enabled = false;
            кбНЗаявкиКлон.FormattingEnabled = true;
            кбНЗаявкиКлон.Location = new Point(540, 219);
            кбНЗаявкиКлон.Name = "кбНЗаявкиКлон";
            кбНЗаявкиКлон.Size = new Size(128, 23);
            кбНЗаявкиКлон.TabIndex = 57;
            кбНЗаявкиКлон.Visible = false;
            // 
            // тбСрокОплаты
            // 
            тбСрокОплаты.Location = new Point(540, 248);
            тбСрокОплаты.Name = "тбСрокОплаты";
            тбСрокОплаты.Size = new Size(128, 23);
            тбСрокОплаты.TabIndex = 56;
            тбСрокОплаты.Visible = false;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(16, 27);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(108, 19);
            checkBox1.TabIndex = 55;
            checkBox1.Text = "Новая покупка";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // тбСтоимостьОплаты
            // 
            тбСтоимостьОплаты.Location = new Point(126, 245);
            тбСтоимостьОплаты.Name = "тбСтоимостьОплаты";
            тбСтоимостьОплаты.Size = new Size(257, 23);
            тбСтоимостьОплаты.TabIndex = 54;
            // 
            // тбШтраф
            // 
            тбШтраф.Location = new Point(126, 207);
            тбШтраф.Name = "тбШтраф";
            тбШтраф.Size = new Size(257, 23);
            тбШтраф.TabIndex = 53;
            // 
            // тбДатаОплаты
            // 
            тбДатаОплаты.Location = new Point(172, 81);
            тбДатаОплаты.Name = "тбДатаОплаты";
            тбДатаОплаты.Size = new Size(128, 23);
            тбДатаОплаты.TabIndex = 52;
            тбДатаОплаты.ValueChanged += тбДатаОплаты_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(175, 63);
            label5.Name = "label5";
            label5.Size = new Size(80, 15);
            label5.TabIndex = 38;
            label5.Text = "Дата оплаты:";
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(586, 40);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 51;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label7.Location = new Point(17, 248);
            label7.Name = "label7";
            label7.Size = new Size(87, 15);
            label7.TabIndex = 40;
            label7.Text = "К оплате, руб:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(16, 210);
            label6.Name = "label6";
            label6.Size = new Size(75, 15);
            label6.TabIndex = 38;
            label6.Text = "Штраф, руб:";
            // 
            // кбНС
            // 
            кбНС.FormattingEnabled = true;
            кбНС.Location = new Point(126, 168);
            кбНС.Name = "кбНС";
            кбНС.Size = new Size(257, 23);
            кбНС.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(16, 171);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 36;
            label4.Text = "Сотрудник:";
            // 
            // кбНКл
            // 
            кбНКл.Enabled = false;
            кбНКл.FormattingEnabled = true;
            кбНКл.Location = new Point(126, 129);
            кбНКл.Name = "кбНКл";
            кбНКл.Size = new Size(257, 23);
            кбНКл.TabIndex = 35;
            // 
            // button3
            // 
            button3.Location = new Point(540, 190);
            button3.Name = "button3";
            button3.Size = new Size(128, 23);
            button3.TabIndex = 34;
            button3.Text = "Удалить запись";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(422, 110);
            button2.Name = "button2";
            button2.Size = new Size(113, 23);
            button2.TabIndex = 33;
            button2.Text = "Изменить запись";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(16, 345);
            label3.Name = "label3";
            label3.Size = new Size(161, 20);
            label3.TabIndex = 52;
            label3.Text = "Подробности заявки:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Moccasin;
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(714, 332);
            panel1.TabIndex = 54;
            // 
            // УчетПокупокУслугКлиентов
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(714, 530);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "УчетПокупокУслугКлиентов";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Провести покупки услуг по заявкам клиентов";
            Load += УчетПокупокУслугКлиентов_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private ComboBox кбНЗаявки;
        private DataGridView dataGridView1;
        private Button button1;
        private Label label1;
        private GroupBox groupBox1;
        private Button button3;
        private Button button2;
        internal Label lbWhoLogged;
        private Label label3;
        private Panel panel1;
        private ComboBox кбНКл;
        private Label label7;
        private Label label6;
        private ComboBox кбНС;
        private Label label4;
        private Label label5;
        private DateTimePicker тбДатаОплаты;
        private TextBox тбШтраф;
        private TextBox тбСтоимостьОплаты;
        private CheckBox checkBox1;
        private DateTimePicker тбСрокОплаты;
        private ComboBox кбНЗаявкиКлон;
    }
}