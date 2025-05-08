namespace Client
{
    partial class ReturnPaymentForm
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
            groupBox1 = new GroupBox();
            кбСпособВозврата = new ComboBox();
            кбНЗаявкиКлон = new ComboBox();
            тбСрокОплаты = new DateTimePicker();
            тбСуммаВозврата = new TextBox();
            тбДатаВозврата = new DateTimePicker();
            label5 = new Label();
            lbWhoLogged = new Label();
            label7 = new Label();
            label6 = new Label();
            кбНС = new ComboBox();
            label4 = new Label();
            кбНКл = new ComboBox();
            label2 = new Label();
            кбНЗаявки = new ComboBox();
            BTN_CREATENEW = new Button();
            label1 = new Label();
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Moccasin;
            groupBox1.Controls.Add(кбСпособВозврата);
            groupBox1.Controls.Add(кбНЗаявкиКлон);
            groupBox1.Controls.Add(тбСрокОплаты);
            groupBox1.Controls.Add(тбСуммаВозврата);
            groupBox1.Controls.Add(тбДатаВозврата);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(lbWhoLogged);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(кбНС);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(кбНКл);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(кбНЗаявки);
            groupBox1.Controls.Add(BTN_CREATENEW);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(16, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(519, 299);
            groupBox1.TabIndex = 53;
            groupBox1.TabStop = false;
            groupBox1.Text = "Основные данные заявки";
            // 
            // кбСпособВозврата
            // 
            кбСпособВозврата.FormattingEnabled = true;
            кбСпособВозврата.Items.AddRange(new object[] { "Электронный", "Наличные" });
            кбСпособВозврата.Location = new Point(163, 181);
            кбСпособВозврата.Name = "кбСпособВозврата";
            кбСпособВозврата.Size = new Size(145, 23);
            кбСпособВозврата.TabIndex = 58;
            // 
            // кбНЗаявкиКлон
            // 
            кбНЗаявкиКлон.Enabled = false;
            кбНЗаявкиКлон.FormattingEnabled = true;
            кбНЗаявкиКлон.Location = new Point(543, 184);
            кбНЗаявкиКлон.Name = "кбНЗаявкиКлон";
            кбНЗаявкиКлон.Size = new Size(128, 23);
            кбНЗаявкиКлон.TabIndex = 57;
            кбНЗаявкиКлон.Visible = false;
            // 
            // тбСрокОплаты
            // 
            тбСрокОплаты.Location = new Point(543, 213);
            тбСрокОплаты.Name = "тбСрокОплаты";
            тбСрокОплаты.Size = new Size(128, 23);
            тбСрокОплаты.TabIndex = 56;
            тбСрокОплаты.Visible = false;
            // 
            // тбСуммаВозврата
            // 
            тбСуммаВозврата.Location = new Point(163, 225);
            тбСуммаВозврата.Name = "тбСуммаВозврата";
            тбСуммаВозврата.Size = new Size(145, 23);
            тбСуммаВозврата.TabIndex = 54;
            // 
            // тбДатаВозврата
            // 
            тбДатаВозврата.Location = new Point(163, 22);
            тбДатаВозврата.Name = "тбДатаВозврата";
            тбДатаВозврата.Size = new Size(145, 23);
            тбДатаВозврата.TabIndex = 52;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(54, 28);
            label5.Name = "label5";
            label5.Size = new Size(90, 15);
            label5.TabIndex = 38;
            label5.Text = "Дата возврата:";
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(634, 28);
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
            label7.Location = new Point(15, 228);
            label7.Name = "label7";
            label7.Size = new Size(129, 15);
            label7.TabIndex = 40;
            label7.Text = "Сумма возврата, руб:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(39, 184);
            label6.Name = "label6";
            label6.Size = new Size(105, 15);
            label6.TabIndex = 38;
            label6.Text = "Способ возврата:";
            // 
            // кбНС
            // 
            кбНС.FormattingEnabled = true;
            кбНС.Location = new Point(163, 136);
            кбНС.Name = "кбНС";
            кбНС.Size = new Size(252, 23);
            кбНС.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(76, 139);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 36;
            label4.Text = "Сотрудник:";
            // 
            // кбНКл
            // 
            кбНКл.FormattingEnabled = true;
            кбНКл.Location = new Point(163, 58);
            кбНКл.Name = "кбНКл";
            кбНКл.Size = new Size(252, 23);
            кбНКл.TabIndex = 35;
            кбНКл.SelectionChangeCommitted += кбНКл_SelectionChangeCommitted;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(55, 100);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 27;
            label2.Text = "Номер заявки:";
            // 
            // кбНЗаявки
            // 
            кбНЗаявки.FormattingEnabled = true;
            кбНЗаявки.Location = new Point(163, 97);
            кбНЗаявки.Name = "кбНЗаявки";
            кбНЗаявки.Size = new Size(145, 23);
            кбНЗаявки.TabIndex = 28;
            кбНЗаявки.SelectionChangeCommitted += кбНЗаявки_SelectionChangeCommitted;
            // 
            // BTN_CREATENEW
            // 
            BTN_CREATENEW.Location = new Point(354, 251);
            BTN_CREATENEW.Name = "BTN_CREATENEW";
            BTN_CREATENEW.Size = new Size(149, 32);
            BTN_CREATENEW.TabIndex = 32;
            BTN_CREATENEW.Text = "Подтвердить возврат";
            BTN_CREATENEW.UseVisualStyleBackColor = true;
            BTN_CREATENEW.Click += BTN_CREATENEW_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(44, 61);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 30;
            label1.Text = "Клиент в заявке:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Moccasin;
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(551, 323);
            panel1.TabIndex = 55;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 376);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(519, 136);
            dataGridView1.TabIndex = 56;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(16, 344);
            label3.Name = "label3";
            label3.Size = new Size(161, 20);
            label3.TabIndex = 57;
            label3.Text = "Подробности заявки:";
            // 
            // ReturnPaymentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(551, 533);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(panel1);
            Name = "ReturnPaymentForm";
            Text = "Управление заявками на возврат средств";
            Load += ReturnPaymentForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox кбНЗаявкиКлон;
        private DateTimePicker тбСрокОплаты;
        private TextBox тбСуммаВозврата;
        private DateTimePicker тбДатаВозврата;
        private Label label5;
        internal Label lbWhoLogged;
        private Label label7;
        private Label label6;
        private ComboBox кбНС;
        private Label label4;
        private ComboBox кбНКл;
        private Label label2;
        private ComboBox кбНЗаявки;
        private Button BTN_CREATENEW;
        private Label label1;
        private Panel panel1;
        private DataGridView dataGridView1;
        private Label label3;
        private ComboBox кбСпособВозврата;
    }
}