namespace Client
{
    partial class Группа
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
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            checkBox1 = new CheckBox();
            label5 = new Label();
            кбНГр = new ComboBox();
            label4 = new Label();
            тбДатаРегистрации = new DateTimePicker();
            button3 = new Button();
            button2 = new Button();
            тбЧисленностьГруппы = new NumericUpDown();
            label2 = new Label();
            кбНОрг = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)тбЧисленностьГруппы).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(600, 28);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 51;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Controls.Add(lbWhoLogged);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(671, 257);
            panel1.TabIndex = 52;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.LightSteelBlue;
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(кбНГр);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(тбДатаРегистрации);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(тбЧисленностьГруппы);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(кбНОрг);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(16, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(559, 229);
            groupBox1.TabIndex = 49;
            groupBox1.TabStop = false;
            groupBox1.Text = "Информация о группе";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(380, 40);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(158, 19);
            checkBox1.TabIndex = 39;
            checkBox1.Text = "Добавить новую группу";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(13, 39);
            label5.Name = "label5";
            label5.Size = new Size(92, 15);
            label5.TabIndex = 37;
            label5.Text = "Номер группы:";
            // 
            // кбНГр
            // 
            кбНГр.FormattingEnabled = true;
            кбНГр.Location = new Point(142, 36);
            кбНГр.Name = "кбНГр";
            кбНГр.Size = new Size(202, 23);
            кбНГр.TabIndex = 38;
            кбНГр.SelectionChangeCommitted += кбНГр_SelectionChangeCommitted;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(13, 186);
            label4.Name = "label4";
            label4.Size = new Size(126, 15);
            label4.TabIndex = 36;
            label4.Text = "Численность группы:";
            // 
            // тбДатаРегистрации
            // 
            тбДатаРегистрации.Location = new Point(142, 133);
            тбДатаРегистрации.Name = "тбДатаРегистрации";
            тбДатаРегистрации.Size = new Size(202, 23);
            тбДатаРегистрации.TabIndex = 35;
            // 
            // button3
            // 
            button3.Location = new Point(380, 154);
            button3.Name = "button3";
            button3.Size = new Size(158, 23);
            button3.TabIndex = 34;
            button3.Text = "Удалить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(380, 125);
            button2.Name = "button2";
            button2.Size = new Size(158, 23);
            button2.TabIndex = 33;
            button2.Text = "Обновить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // тбЧисленностьГруппы
            // 
            тбЧисленностьГруппы.Location = new Point(142, 184);
            тбЧисленностьГруппы.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            тбЧисленностьГруппы.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            тбЧисленностьГруппы.Name = "тбЧисленностьГруппы";
            тбЧисленностьГруппы.Size = new Size(202, 23);
            тбЧисленностьГруппы.TabIndex = 31;
            тбЧисленностьГруппы.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(13, 87);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 27;
            label2.Text = "Организация:";
            // 
            // кбНОрг
            // 
            кбНОрг.FormattingEnabled = true;
            кбНОрг.Location = new Point(142, 84);
            кбНОрг.Name = "кбНОрг";
            кбНОрг.Size = new Size(202, 23);
            кбНОрг.TabIndex = 28;
            кбНОрг.SelectionChangeCommitted += кбНОрг_SelectionChangeCommitted;
            // 
            // button1
            // 
            button1.Location = new Point(380, 96);
            button1.Name = "button1";
            button1.Size = new Size(158, 23);
            button1.TabIndex = 32;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(13, 139);
            label1.Name = "label1";
            label1.Size = new Size(109, 15);
            label1.TabIndex = 30;
            label1.Text = "Дата регистрации:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 311);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(623, 136);
            dataGridView1.TabIndex = 54;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(16, 273);
            label3.Name = "label3";
            label3.Size = new Size(205, 20);
            label3.TabIndex = 53;
            label3.Text = "Подробности организации:";
            // 
            // Группа
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(671, 475);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Группа";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Создать группу для организации";
            Load += Группа_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)тбЧисленностьГруппы).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label lbWhoLogged;
        private Panel panel1;
        private GroupBox groupBox1;
        private Button button3;
        private Button button2;
        private NumericUpDown тбЧисленностьГруппы;
        private Label label2;
        private ComboBox кбНОрг;
        private Button button1;
        private Label label1;
        private DataGridView dataGridView1;
        private Label label3;
        private DateTimePicker тбДатаРегистрации;
        private Label label4;
        private Label label5;
        private ComboBox кбНГр;
        private CheckBox checkBox1;
    }
}