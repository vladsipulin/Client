namespace Client
{
    partial class ReqOnServiceForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

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
            dgvSelectedServices = new DataGridView();
            label4 = new Label();
            label5 = new Label();
            тбЦенаСлужбы = new TextBox();
            lbWhoLogged = new Label();
            тбДатаЗаявки = new DateTimePicker();
            label3 = new Label();
            тбКолво = new TextBox();
            button1 = new Button();
            RADIOBTN_ONLINEPAY = new RadioButton();
            RADIOBTN_CASHPAY = new RadioButton();
            groupBox1 = new GroupBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSelectedServices).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // кбНСл
            // 
            кбНСл.FormattingEnabled = true;
            кбНСл.Location = new Point(181, 190);
            кбНСл.Name = "кбНСл";
            кбНСл.Size = new Size(143, 23);
            кбНСл.TabIndex = 26;
            кбНСл.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 193);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 25;
            label2.Text = "Служба быта:";
            label2.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(19, 31);
            label1.Margin = new Padding(10);
            label1.Name = "label1";
            label1.Size = new Size(416, 37);
            label1.TabIndex = 24;
            label1.Text = "Содержание заявки на услугу";
            // 
            // кбНКл
            // 
            кбНКл.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            кбНКл.AutoSize = true;
            кбНКл.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            кбНКл.Location = new Point(439, 87);
            кбНКл.Name = "кбНКл";
            кбНКл.Size = new Size(122, 21);
            кбНКл.TabIndex = 32;
            кбНКл.Text = "Номер клиента:";
            // 
            // тбНКл
            // 
            тбНКл.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            тбНКл.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            тбНКл.Location = new Point(582, 84);
            тбНКл.Name = "тбНКл";
            тбНКл.Size = new Size(200, 29);
            тбНКл.TabIndex = 31;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label9.Location = new Point(19, 446);
            label9.Name = "label9";
            label9.Size = new Size(179, 21);
            label9.TabIndex = 30;
            label9.Text = "Сумма за услугу, руб:";
            // 
            // тбСумма
            // 
            тбСумма.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            тбСумма.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            тбСумма.Location = new Point(218, 443);
            тбСумма.Name = "тбСумма";
            тбСумма.ReadOnly = true;
            тбСумма.Size = new Size(200, 29);
            тбСумма.TabIndex = 29;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(19, 383);
            label7.Name = "label7";
            label7.Size = new Size(105, 21);
            label7.TabIndex = 27;
            label7.Text = "Срок оплаты:";
            // 
            // тбСрокОплаты
            // 
            тбСрокОплаты.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            тбСрокОплаты.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            тбСрокОплаты.Location = new Point(218, 377);
            тбСрокОплаты.Name = "тбСрокОплаты";
            тбСрокОплаты.Size = new Size(200, 29);
            тбСрокОплаты.TabIndex = 28;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Controls.Add(dgvSelectedServices);
            panel1.Controls.Add(label4);
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
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(794, 343);
            panel1.TabIndex = 33;
            // 
            // dgvSelectedServices
            // 
            dgvSelectedServices.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSelectedServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSelectedServices.Location = new Point(19, 131);
            dgvSelectedServices.Name = "dgvSelectedServices";
            dgvSelectedServices.RowTemplate.Height = 40;
            dgvSelectedServices.Size = new Size(763, 180);
            dgvSelectedServices.TabIndex = 42;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(31, 257);
            label4.Name = "label4";
            label4.Size = new Size(93, 15);
            label4.TabIndex = 37;
            label4.Text = "Количество, ед:";
            label4.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(31, 222);
            label5.Name = "label5";
            label5.Size = new Size(85, 15);
            label5.TabIndex = 41;
            label5.Text = "Цена службы:";
            label5.Visible = false;
            // 
            // тбЦенаСлужбы
            // 
            тбЦенаСлужбы.Location = new Point(181, 219);
            тбЦенаСлужбы.Name = "тбЦенаСлужбы";
            тбЦенаСлужбы.Size = new Size(143, 23);
            тбЦенаСлужбы.TabIndex = 40;
            тбЦенаСлужбы.Visible = false;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(501, 18);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 39;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // тбДатаЗаявки
            // 
            тбДатаЗаявки.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            тбДатаЗаявки.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            тбДатаЗаявки.Location = new Point(582, 39);
            тбДатаЗаявки.Name = "тбДатаЗаявки";
            тбДатаЗаявки.Size = new Size(200, 29);
            тбДатаЗаявки.TabIndex = 35;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(462, 44);
            label3.Name = "label3";
            label3.Size = new Size(99, 21);
            label3.TabIndex = 34;
            label3.Text = "Дата заявки:";
            // 
            // тбКолво
            // 
            тбКолво.Location = new Point(181, 257);
            тбКолво.Name = "тбКолво";
            тбКолво.Size = new Size(143, 23);
            тбКолво.TabIndex = 36;
            тбКолво.Visible = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(462, 494);
            button1.Name = "button1";
            button1.Size = new Size(311, 50);
            button1.TabIndex = 38;
            button1.Text = "Оформить заявку";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // RADIOBTN_ONLINEPAY
            // 
            RADIOBTN_ONLINEPAY.AutoSize = true;
            RADIOBTN_ONLINEPAY.Checked = true;
            RADIOBTN_ONLINEPAY.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            RADIOBTN_ONLINEPAY.Location = new Point(13, 31);
            RADIOBTN_ONLINEPAY.Name = "RADIOBTN_ONLINEPAY";
            RADIOBTN_ONLINEPAY.Size = new Size(181, 25);
            RADIOBTN_ONLINEPAY.TabIndex = 39;
            RADIOBTN_ONLINEPAY.TabStop = true;
            RADIOBTN_ONLINEPAY.Text = "Электронный платеж";
            RADIOBTN_ONLINEPAY.UseVisualStyleBackColor = true;
            // 
            // RADIOBTN_CASHPAY
            // 
            RADIOBTN_CASHPAY.AutoSize = true;
            RADIOBTN_CASHPAY.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            RADIOBTN_CASHPAY.Location = new Point(13, 71);
            RADIOBTN_CASHPAY.Name = "RADIOBTN_CASHPAY";
            RADIOBTN_CASHPAY.Size = new Size(143, 25);
            RADIOBTN_CASHPAY.TabIndex = 40;
            RADIOBTN_CASHPAY.TabStop = true;
            RADIOBTN_CASHPAY.Text = "Оплата на кассе";
            RADIOBTN_CASHPAY.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(RADIOBTN_ONLINEPAY);
            groupBox1.Controls.Add(RADIOBTN_CASHPAY);
            groupBox1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.Location = new Point(462, 368);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(311, 108);
            groupBox1.TabIndex = 41;
            groupBox1.TabStop = false;
            groupBox1.Text = "Способ оплаты";
            // 
            // ReqOnServiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 556);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(тбКолво);
            Controls.Add(label9);
            Controls.Add(тбСумма);
            Controls.Add(label7);
            Controls.Add(тбСрокОплаты);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ReqOnServiceForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Подтверждение заказа";
            Load += ReqOnService_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSelectedServices).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private DataGridView dgvSelectedServices;
        private RadioButton RADIOBTN_ONLINEPAY;
        private RadioButton RADIOBTN_CASHPAY;
        private GroupBox groupBox1;
    }
}