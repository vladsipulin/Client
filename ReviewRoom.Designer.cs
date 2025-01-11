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
            button1 = new Button();
            тбОценка = new NumericUpDown();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            кбНЗаявки = new ComboBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)тбОценка).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(460, 18);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 47;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(460, 133);
            button1.Name = "button1";
            button1.Size = new Size(113, 23);
            button1.TabIndex = 46;
            button1.Text = "Добавить отзыв";
            button1.UseVisualStyleBackColor = true;
            // 
            // тбОценка
            // 
            тбОценка.Location = new Point(127, 73);
            тбОценка.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            тбОценка.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            тбОценка.Name = "тбОценка";
            тбОценка.Size = new Size(143, 23);
            тбОценка.TabIndex = 45;
            тбОценка.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 75);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 44;
            label1.Text = "Оценка услуги:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(25, 184);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(548, 77);
            dataGridView1.TabIndex = 43;
            // 
            // кбНЗаявки
            // 
            кбНЗаявки.FormattingEnabled = true;
            кбНЗаявки.Location = new Point(127, 18);
            кбНЗаявки.Name = "кбНЗаявки";
            кбНЗаявки.Size = new Size(143, 23);
            кбНЗаявки.TabIndex = 42;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 21);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 41;
            label2.Text = "Номер заявки:";
            // 
            // ReviewRoom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(603, 283);
            Controls.Add(lbWhoLogged);
            Controls.Add(button1);
            Controls.Add(тбОценка);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(кбНЗаявки);
            Controls.Add(label2);
            Name = "ReviewRoom";
            Text = "Отзыв на заселение";
            Load += ReviewRoom_Load;
            ((System.ComponentModel.ISupportInitialize)тбОценка).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label lbWhoLogged;
        private Button button1;
        private NumericUpDown тбОценка;
        private Label label1;
        private DataGridView dataGridView1;
        private ComboBox кбНЗаявки;
        private Label label2;
    }
}