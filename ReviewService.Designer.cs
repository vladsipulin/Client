namespace Client
{
    partial class ReviewService
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
            кбНЗаявки = new ComboBox();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            тбОценка = new NumericUpDown();
            button1 = new Button();
            lbWhoLogged = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            button3 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)тбОценка).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // кбНЗаявки
            // 
            кбНЗаявки.FormattingEnabled = true;
            кбНЗаявки.Location = new Point(118, 25);
            кбНЗаявки.Name = "кбНЗаявки";
            кбНЗаявки.Size = new Size(143, 23);
            кбНЗаявки.TabIndex = 28;
            кбНЗаявки.SelectionChangeCommitted += кбНЗаявки_SelectionChangeCommitted;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(16, 28);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 27;
            label2.Text = "Номер заявки:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 213);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(623, 136);
            dataGridView1.TabIndex = 29;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(16, 82);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 30;
            label1.Text = "Оценка услуги:";
            // 
            // тбОценка
            // 
            тбОценка.Location = new Point(118, 80);
            тбОценка.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            тбОценка.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            тбОценка.Name = "тбОценка";
            тбОценка.Size = new Size(143, 23);
            тбОценка.TabIndex = 31;
            тбОценка.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // button1
            // 
            button1.Location = new Point(286, 25);
            button1.Name = "button1";
            button1.Size = new Size(113, 23);
            button1.TabIndex = 32;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(526, 27);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 40;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(16, 175);
            label3.Name = "label3";
            label3.Size = new Size(161, 20);
            label3.TabIndex = 41;
            label3.Text = "Подробности заявки:";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.LightSteelBlue;
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(тбОценка);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(кбНЗаявки);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(16, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 128);
            groupBox1.TabIndex = 42;
            groupBox1.TabStop = false;
            groupBox1.Text = "Оценка услуги по заявке";
            // 
            // button3
            // 
            button3.Location = new Point(286, 83);
            button3.Name = "button3";
            button3.Size = new Size(113, 23);
            button3.TabIndex = 34;
            button3.Text = "Удалить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(286, 54);
            button2.Name = "button2";
            button2.Size = new Size(113, 23);
            button2.TabIndex = 33;
            button2.Text = "Обновить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(651, 160);
            panel1.TabIndex = 49;
            // 
            // ReviewService
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(651, 370);
            Controls.Add(groupBox1);
            Controls.Add(lbWhoLogged);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ReviewService";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Отзыв на услугу";
            Load += ReviewService_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)тбОценка).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox кбНЗаявки;
        private Label label2;
        private DataGridView dataGridView1;
        private Label label1;
        private NumericUpDown тбОценка;
        private Button button1;
        internal Label lbWhoLogged;
        private Label label3;
        private GroupBox groupBox1;
        private Button button3;
        private Button button2;
        private Panel panel1;
    }
}