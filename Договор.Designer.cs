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
            кбНДоговора = new ComboBox();
            тбДатаНачала = new DateTimePicker();
            button3 = new Button();
            panel1 = new Panel();
            lbWhoLogged = new Label();
            groupBox1 = new GroupBox();
            label10 = new Label();
            кбНС = new ComboBox();
            label8 = new Label();
            тбДатаОкончания = new DateTimePicker();
            label7 = new Label();
            label6 = new Label();
            CMBX_TEMP = new ComboBox();
            button2 = new Button();
            label2 = new Label();
            кбНОрг = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 329);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(623, 136);
            dataGridView1.TabIndex = 57;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(16, 291);
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
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(19, 39);
            label5.Name = "label5";
            label5.Size = new Size(86, 15);
            label5.TabIndex = 37;
            label5.Text = "Тип договора:";
            // 
            // кбНДоговора
            // 
            кбНДоговора.FormattingEnabled = true;
            кбНДоговора.Location = new Point(111, 36);
            кбНДоговора.Name = "кбНДоговора";
            кбНДоговора.Size = new Size(290, 23);
            кбНДоговора.TabIndex = 38;
            кбНДоговора.SelectionChangeCommitted += кбНДоговора_SelectionChangeCommitted;
            // 
            // тбДатаНачала
            // 
            тбДатаНачала.Location = new Point(111, 161);
            тбДатаНачала.Name = "тбДатаНачала";
            тбДатаНачала.Size = new Size(290, 23);
            тбДатаНачала.TabIndex = 35;
            тбДатаНачала.ValueChanged += тбДатаНачала_ValueChanged;
            // 
            // button3
            // 
            button3.Location = new Point(421, 130);
            button3.Name = "button3";
            button3.Size = new Size(165, 23);
            button3.TabIndex = 34;
            button3.Text = "Расторгнуть";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Moccasin;
            panel1.Controls.Add(lbWhoLogged);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(659, 276);
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
            groupBox1.BackColor = Color.Moccasin;
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(кбНС);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(тбДатаОкончания);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(CMBX_TEMP);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(кбНДоговора);
            groupBox1.Controls.Add(тбДатаНачала);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(кбНОрг);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(16, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(623, 242);
            groupBox1.TabIndex = 49;
            groupBox1.TabStop = false;
            groupBox1.Text = "Информация о договоре";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(37, 119);
            label10.Name = "label10";
            label10.Size = new Size(68, 15);
            label10.TabIndex = 61;
            label10.Text = "Сотрудник:";
            // 
            // кбНС
            // 
            кбНС.FormattingEnabled = true;
            кбНС.Location = new Point(111, 116);
            кбНС.Name = "кбНС";
            кбНС.Size = new Size(290, 23);
            кбНС.TabIndex = 62;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(558, 201);
            label8.Name = "label8";
            label8.Size = new Size(59, 15);
            label8.TabIndex = 58;
            label8.Text = "username";
            label8.Visible = false;
            // 
            // тбДатаОкончания
            // 
            тбДатаОкончания.Location = new Point(111, 201);
            тбДатаОкончания.Name = "тбДатаОкончания";
            тбДатаОкончания.Size = new Size(290, 23);
            тбДатаОкончания.TabIndex = 44;
            тбДатаОкончания.ValueChanged += тбДатаОкончания_ValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(6, 207);
            label7.Name = "label7";
            label7.Size = new Size(99, 15);
            label7.TabIndex = 43;
            label7.Text = "Дата окончания:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(411, 178);
            label6.Name = "label6";
            label6.Size = new Size(141, 15);
            label6.TabIndex = 40;
            label6.Text = "Гостиничный комплекс:";
            label6.Visible = false;
            // 
            // CMBX_TEMP
            // 
            CMBX_TEMP.FormattingEnabled = true;
            CMBX_TEMP.Location = new Point(558, 175);
            CMBX_TEMP.Name = "CMBX_TEMP";
            CMBX_TEMP.Size = new Size(28, 23);
            CMBX_TEMP.TabIndex = 41;
            CMBX_TEMP.Visible = false;
            // 
            // button2
            // 
            button2.Location = new Point(421, 101);
            button2.Name = "button2";
            button2.Size = new Size(165, 23);
            button2.TabIndex = 33;
            button2.Text = "Изменить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(22, 79);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 27;
            label2.Text = "Организация:";
            // 
            // кбНОрг
            // 
            кбНОрг.FormattingEnabled = true;
            кбНОрг.Location = new Point(111, 76);
            кбНОрг.Name = "кбНОрг";
            кбНОрг.Size = new Size(290, 23);
            кбНОрг.TabIndex = 28;
            кбНОрг.SelectionChangeCommitted += кбНОрг_SelectionChangeCommitted;
            // 
            // button1
            // 
            button1.Location = new Point(421, 72);
            button1.Name = "button1";
            button1.Size = new Size(165, 23);
            button1.TabIndex = 32;
            button1.Text = "Заключить новый";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(27, 167);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 30;
            label1.Text = "Дата начала:";
            // 
            // Договор
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(659, 481);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Договор";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Договора с организациями";
            Load += Договор_Load;
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
        private ComboBox кбНДоговора;
        private DateTimePicker тбДатаНачала;
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
        private ComboBox CMBX_TEMP;
        private DateTimePicker тбДатаОкончания;
        private Label label7;
        internal Label label8;
        private Label label10;
        private ComboBox кбНС;
    }
}