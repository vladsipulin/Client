namespace Client
{
    partial class MainForm_Porter
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
            lbWhoLogged = new Label();
            label6 = new Label();
            keyLbl = new Label();
            label5 = new Label();
            comboBox1 = new ComboBox();
            label1 = new Label();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(460, 9);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(38, 15);
            lbWhoLogged.TabIndex = 13;
            lbWhoLogged.Text = "label5";
            lbWhoLogged.Visible = false;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.BackColor = Color.LightSteelBlue;
            label6.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Location = new Point(569, 26);
            label6.Name = "label6";
            label6.Size = new Size(102, 17);
            label6.TabIndex = 11;
            label6.Text = "Номер портье:";
            // 
            // keyLbl
            // 
            keyLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            keyLbl.AutoSize = true;
            keyLbl.BackColor = Color.LightSteelBlue;
            keyLbl.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            keyLbl.ForeColor = SystemColors.ActiveCaptionText;
            keyLbl.Location = new Point(670, 26);
            keyLbl.Name = "keyLbl";
            keyLbl.Size = new Size(48, 17);
            keyLbl.TabIndex = 12;
            keyLbl.Text = "123456";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.LightSteelBlue;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.DarkBlue;
            label5.Location = new Point(25, 88);
            label5.Name = "label5";
            label5.Size = new Size(114, 20);
            label5.TabIndex = 29;
            label5.Text = "Бизнес-форма:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Управление договорами с организациями", "Управление заявками клиентов на услуги", "Управление заявками клиентов на возврат средств за услуги", "Отчет об отзывах клиентов о терминале", "Отчет о популярных услугах на основе заявок клиентов" });
            comboBox1.Location = new Point(145, 88);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(385, 23);
            comboBox1.TabIndex = 30;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.LightSteelBlue;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.DarkBlue;
            label1.Location = new Point(25, 18);
            label1.Name = "label1";
            label1.Size = new Size(400, 32);
            label1.TabIndex = 28;
            label1.Text = "Рабочая среда сотрудника портье";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(0, 148);
            panel1.Name = "panel1";
            panel1.Size = new Size(730, 297);
            panel1.TabIndex = 31;
            // 
            // MainForm_Porter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(730, 445);
            Controls.Add(panel1);
            Controls.Add(label5);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(lbWhoLogged);
            Controls.Add(label6);
            Controls.Add(keyLbl);
            Name = "MainForm_Porter";
            Text = "Главная форма";
            Load += MainForm_Porter_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label lbWhoLogged;
        internal Label label6;
        internal Label keyLbl;
        private Label label5;
        private ComboBox comboBox1;
        private Label label1;
        private Panel panel1;
    }
}