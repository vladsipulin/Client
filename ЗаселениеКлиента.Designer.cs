namespace Client
{
    partial class ЗаселениеКлиента
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
            dataGridView1 = new DataGridView();
            label3 = new Label();
            label5 = new Label();
            кбНЗаявки = new ComboBox();
            label4 = new Label();
            button3 = new Button();
            panel1 = new Panel();
            lbWhoLogged = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            кбСтатусЗаявки = new ComboBox();
            label1 = new Label();
            кбНС = new ComboBox();
            button2 = new Button();
            label2 = new Label();
            кбНКл = new ComboBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 313);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(623, 136);
            dataGridView1.TabIndex = 57;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(16, 275);
            label3.Name = "label3";
            label3.Size = new Size(161, 20);
            label3.TabIndex = 56;
            label3.Text = "Подробности заявки:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(13, 39);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 37;
            label5.Text = "Номер заявки:";
            // 
            // кбНЗаявки
            // 
            кбНЗаявки.FormattingEnabled = true;
            кбНЗаявки.Location = new Point(142, 36);
            кбНЗаявки.Name = "кбНЗаявки";
            кбНЗаявки.Size = new Size(244, 23);
            кбНЗаявки.TabIndex = 38;
            кбНЗаявки.SelectionChangeCommitted += кбНЗаявки_SelectionChangeCommitted;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(13, 186);
            label4.Name = "label4";
            label4.Size = new Size(116, 15);
            label4.TabIndex = 36;
            label4.Text = "Решение по заявке:";
            // 
            // button3
            // 
            button3.Location = new Point(418, 200);
            button3.Name = "button3";
            button3.Size = new Size(167, 23);
            button3.TabIndex = 34;
            button3.Text = "Удалить";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Moccasin;
            panel1.Controls.Add(lbWhoLogged);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(660, 257);
            panel1.TabIndex = 55;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(1060, 28);
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
            groupBox1.Controls.Add(кбСтатусЗаявки);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(кбНС);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(кбНЗаявки);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(кбНКл);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new Point(16, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(623, 229);
            groupBox1.TabIndex = 49;
            groupBox1.TabStop = false;
            groupBox1.Text = "Информация о заявке на заселение клиента";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radioButton2);
            groupBox2.Controls.Add(radioButton1);
            groupBox2.Location = new Point(418, 22);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(167, 80);
            groupBox2.TabIndex = 44;
            groupBox2.TabStop = false;
            groupBox2.Text = "Выбор заявок со статусом";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(6, 47);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(150, 19);
            radioButton2.TabIndex = 44;
            radioButton2.TabStop = true;
            radioButton2.Text = "Только \"Рассмотрено\"";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(6, 22);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(134, 19);
            radioButton1.TabIndex = 43;
            radioButton1.TabStop = true;
            radioButton1.Text = "Только \"Ожидание\"";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // кбСтатусЗаявки
            // 
            кбСтатусЗаявки.FormattingEnabled = true;
            кбСтатусЗаявки.Items.AddRange(new object[] { "Заселен", "Выселен" });
            кбСтатусЗаявки.Location = new Point(142, 183);
            кбСтатусЗаявки.Name = "кбСтатусЗаявки";
            кбСтатусЗаявки.Size = new Size(244, 23);
            кбСтатусЗаявки.TabIndex = 42;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(13, 139);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 40;
            label1.Text = "Сотрудник:";
            // 
            // кбНС
            // 
            кбНС.FormattingEnabled = true;
            кбНС.Location = new Point(142, 136);
            кбНС.Name = "кбНС";
            кбНС.Size = new Size(244, 23);
            кбНС.TabIndex = 41;
            // 
            // button2
            // 
            button2.Location = new Point(418, 139);
            button2.Name = "button2";
            button2.Size = new Size(167, 23);
            button2.TabIndex = 33;
            button2.Text = "Обновить запись";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(13, 87);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 27;
            label2.Text = "Клиент в заявке:";
            // 
            // кбНКл
            // 
            кбНКл.FormattingEnabled = true;
            кбНКл.Location = new Point(142, 84);
            кбНКл.Name = "кбНКл";
            кбНКл.Size = new Size(244, 23);
            кбНКл.TabIndex = 28;
            // 
            // button1
            // 
            button1.Location = new Point(418, 110);
            button1.Name = "button1";
            button1.Size = new Size(167, 23);
            button1.TabIndex = 32;
            button1.Text = "Добавить запись";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ЗаселениеКлиента
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(660, 467);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ЗаселениеКлиента";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Управление заявками клиентов на заселение";
            Load += ЗаселениеКлиента_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label3;
        private Label label5;
        private ComboBox кбНЗаявки;
        private Label label4;
        private Button button3;
        private Panel panel1;
        internal Label lbWhoLogged;
        private GroupBox groupBox1;
        private Button button2;
        private Label label2;
        private ComboBox кбНКл;
        private Button button1;
        private ComboBox кбСтатусЗаявки;
        private Label label1;
        private ComboBox кбНС;
        private GroupBox groupBox2;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
    }
}