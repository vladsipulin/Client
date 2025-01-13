namespace Client
{
    partial class Договор
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
            checkBox1 = new CheckBox();
            label5 = new Label();
            кбНГр = new ComboBox();
            label4 = new Label();
            тбДатаРегистрации = new DateTimePicker();
            button3 = new Button();
            panel1 = new Panel();
            lbWhoLogged = new Label();
            groupBox1 = new GroupBox();
            button2 = new Button();
            label2 = new Label();
            кбНОрг = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            label6 = new Label();
            comboBox1 = new ComboBox();
            кбРазмерСкидки = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            label7 = new Label();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 387);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(623, 136);
            dataGridView1.TabIndex = 57;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(16, 349);
            label3.Name = "label3";
            label3.Size = new Size(205, 20);
            label3.TabIndex = 56;
            label3.Text = "Подробности организации:";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(421, 37);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(165, 19);
            checkBox1.TabIndex = 39;
            checkBox1.Text = "Добавить новый договор";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(13, 39);
            label5.Name = "label5";
            label5.Size = new Size(84, 15);
            label5.TabIndex = 37;
            label5.Text = "Код договора";
            // 
            // кбНГр
            // 
            кбНГр.FormattingEnabled = true;
            кбНГр.Location = new Point(164, 36);
            кбНГр.Name = "кбНГр";
            кбНГр.Size = new Size(202, 23);
            кбНГр.TabIndex = 38;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(13, 160);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.No;
            label4.Size = new Size(109, 15);
            label4.TabIndex = 36;
            label4.Text = "Размер скидки, %:";
            // 
            // тбДатаРегистрации
            // 
            тбДатаРегистрации.Location = new Point(164, 195);
            тбДатаРегистрации.Name = "тбДатаРегистрации";
            тбДатаРегистрации.Size = new Size(202, 23);
            тбДатаРегистрации.TabIndex = 35;
            // 
            // button3
            // 
            button3.Location = new Point(421, 171);
            button3.Name = "button3";
            button3.Size = new Size(165, 23);
            button3.TabIndex = 34;
            button3.Text = "Удалить";
            button3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Controls.Add(lbWhoLogged);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(659, 327);
            panel1.TabIndex = 55;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(1059, 28);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 51;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.LightSteelBlue;
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(кбРазмерСкидки);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(кбНГр);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(тбДатаРегистрации);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(кбНОрг);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(16, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(623, 284);
            groupBox1.TabIndex = 49;
            groupBox1.TabStop = false;
            groupBox1.Text = "Информация о договоре";
            // 
            // button2
            // 
            button2.Location = new Point(421, 142);
            button2.Name = "button2";
            button2.Size = new Size(165, 23);
            button2.TabIndex = 33;
            button2.Text = "Обновить";
            button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(13, 79);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 27;
            label2.Text = "Организация";
            // 
            // кбНОрг
            // 
            кбНОрг.FormattingEnabled = true;
            кбНОрг.Location = new Point(164, 76);
            кбНОрг.Name = "кбНОрг";
            кбНОрг.Size = new Size(202, 23);
            кбНОрг.TabIndex = 28;
            // 
            // button1
            // 
            button1.Location = new Point(421, 113);
            button1.Name = "button1";
            button1.Size = new Size(165, 23);
            button1.TabIndex = 32;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(13, 201);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 30;
            label1.Text = "Дата начала:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(13, 118);
            label6.Name = "label6";
            label6.Size = new Size(138, 15);
            label6.TabIndex = 40;
            label6.Text = "Гостиничный комплекс";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(164, 115);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(202, 23);
            comboBox1.TabIndex = 41;
            // 
            // кбРазмерСкидки
            // 
            кбРазмерСкидки.FormattingEnabled = true;
            кбРазмерСкидки.Location = new Point(164, 157);
            кбРазмерСкидки.Name = "кбРазмерСкидки";
            кбРазмерСкидки.Size = new Size(202, 23);
            кбРазмерСкидки.TabIndex = 42;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(164, 235);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(202, 23);
            dateTimePicker1.TabIndex = 44;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(13, 241);
            label7.Name = "label7";
            label7.Size = new Size(99, 15);
            label7.TabIndex = 43;
            label7.Text = "Дата окончания:";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(558, 257);
            label8.Name = "label8";
            label8.Size = new Size(59, 15);
            label8.TabIndex = 58;
            label8.Text = "username";
            label8.Visible = false;
            // 
            // Договор
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 547);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(panel1);
            Name = "Договор";
            Text = "Договор";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label3;
        private CheckBox checkBox1;
        private Label label5;
        private ComboBox кбНГр;
        private Label label4;
        private DateTimePicker тбДатаРегистрации;
        private Button button3;
        private Panel panel1;
        internal Label lbWhoLogged;
        private GroupBox groupBox1;
        private Button button2;
        private Label label2;
        private ComboBox кбНОрг;
        private Button button1;
        private Label label1;
        private Label label6;
        private ComboBox comboBox1;
        private ComboBox кбРазмерСкидки;
        private DateTimePicker dateTimePicker1;
        private Label label7;
        internal Label label8;
    }
}