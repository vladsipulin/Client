namespace Client
{
    partial class ReviewRoom
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
            button3 = new Button();
            button2 = new Button();
            тбОценка = new NumericUpDown();
            label2 = new Label();
            кбНЗаявки = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)тбОценка).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(508, 18);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 47;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(651, 158);
            panel1.TabIndex = 48;
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
            groupBox1.TabIndex = 49;
            groupBox1.TabStop = false;
            groupBox1.Text = "Оценка заселения по заявке";
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
            // тбОценка
            // 
            тбОценка.Location = new Point(132, 80);
            тбОценка.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            тбОценка.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            тбОценка.Name = "тбОценка";
            тбОценка.Size = new Size(129, 23);
            тбОценка.TabIndex = 31;
            тбОценка.Value = new decimal(new int[] { 1, 0, 0, 0 });
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
            // кбНЗаявки
            // 
            кбНЗаявки.FormattingEnabled = true;
            кбНЗаявки.Location = new Point(132, 25);
            кбНЗаявки.Name = "кбНЗаявки";
            кбНЗаявки.Size = new Size(129, 23);
            кбНЗаявки.TabIndex = 28;
            кбНЗаявки.SelectionChangeCommitted += кбНЗаявки_SelectionChangeCommitted;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(16, 82);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 30;
            label1.Text = "Оценка заселения:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(16, 175);
            label3.Name = "label3";
            label3.Size = new Size(161, 20);
            label3.TabIndex = 49;
            label3.Text = "Подробности заявки:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 213);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(623, 136);
            dataGridView1.TabIndex = 50;
            // 
            // ReviewRoom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(651, 370);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(lbWhoLogged);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ReviewRoom";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Отзыв на заселение";
            Load += ReviewRoom_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)тбОценка).EndInit();
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
        private NumericUpDown тбОценка;
        private Label label2;
        private ComboBox кбНЗаявки;
        private Button button1;
        private Label label1;
        private Label label3;
        private DataGridView dataGridView1;
    }
}