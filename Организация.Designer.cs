namespace Client
{
    partial class Организация
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
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRem = new System.Windows.Forms.Button();
            this.btnUpd = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.тбДР = new System.Windows.Forms.TextBox();
            this.тбСД = new System.Windows.Forms.TextBox();
            this.тбНаименование = new System.Windows.Forms.TextBox();
            this.тбНОрг = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.orgsDGV = new System.Windows.Forms.DataGridView();
            this.НОрг = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Наименование = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.СфераДеятельности = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ДатаРегистрации = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orgsDGV)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(191, 196);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(83, 23);
            this.btnForward.TabIndex = 12;
            this.btnForward.Text = "-->";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(100, 196);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(83, 23);
            this.btnBack.TabIndex = 11;
            this.btnBack.Text = "<--";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRem
            // 
            this.btnRem.Location = new System.Drawing.Point(242, 157);
            this.btnRem.Name = "btnRem";
            this.btnRem.Size = new System.Drawing.Size(75, 23);
            this.btnRem.TabIndex = 10;
            this.btnRem.Text = "Удалить";
            this.btnRem.UseVisualStyleBackColor = true;
            this.btnRem.Click += new System.EventHandler(this.btnRem_Click);
            // 
            // btnUpd
            // 
            this.btnUpd.Location = new System.Drawing.Point(153, 157);
            this.btnUpd.Name = "btnUpd";
            this.btnUpd.Size = new System.Drawing.Size(83, 23);
            this.btnUpd.TabIndex = 9;
            this.btnUpd.Text = "Обновить";
            this.btnUpd.UseVisualStyleBackColor = true;
            this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(64, 157);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(83, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // тбДР
            // 
            this.тбДР.Location = new System.Drawing.Point(148, 113);
            this.тбДР.Name = "тбДР";
            this.тбДР.Size = new System.Drawing.Size(220, 23);
            this.тбДР.TabIndex = 7;
            // 
            // тбСД
            // 
            this.тбСД.Location = new System.Drawing.Point(148, 84);
            this.тбСД.Name = "тбСД";
            this.тбСД.Size = new System.Drawing.Size(220, 23);
            this.тбСД.TabIndex = 6;
            // 
            // тбНаименование
            // 
            this.тбНаименование.Location = new System.Drawing.Point(148, 55);
            this.тбНаименование.Name = "тбНаименование";
            this.тбНаименование.Size = new System.Drawing.Size(220, 23);
            this.тбНаименование.TabIndex = 5;
            // 
            // тбНОрг
            // 
            this.тбНОрг.Location = new System.Drawing.Point(148, 26);
            this.тбНОрг.Name = "тбНОрг";
            this.тбНОрг.Size = new System.Drawing.Size(220, 23);
            this.тбНОрг.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Дата регистрации";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Номер организации";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Сфера деят-сти";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Наименование";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnForward);
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Controls.Add(this.btnRem);
            this.groupBox1.Controls.Add(this.btnUpd);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.тбДР);
            this.groupBox1.Controls.Add(this.тбСД);
            this.groupBox1.Controls.Add(this.тбНаименование);
            this.groupBox1.Controls.Add(this.тбНОрг);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(22, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 237);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Организация";
            // 
            // orgsDGV
            // 
            this.orgsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orgsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.НОрг,
            this.Наименование,
            this.СфераДеятельности,
            this.ДатаРегистрации});
            this.orgsDGV.Location = new System.Drawing.Point(423, 107);
            this.orgsDGV.Name = "orgsDGV";
            this.orgsDGV.RowTemplate.Height = 25;
            this.orgsDGV.Size = new System.Drawing.Size(537, 237);
            this.orgsDGV.TabIndex = 5;
            // 
            // НОрг
            // 
            this.НОрг.HeaderText = "Номер";
            this.НОрг.Name = "НОрг";
            // 
            // Наименование
            // 
            this.Наименование.HeaderText = "Наименование";
            this.Наименование.Name = "Наименование";
            // 
            // СфераДеятельности
            // 
            this.СфераДеятельности.HeaderText = "Сфера деятельности";
            this.СфераДеятельности.Name = "СфераДеятельности";
            // 
            // ДатаРегистрации
            // 
            this.ДатаРегистрации.HeaderText = "Дата регистрации";
            this.ДатаРегистрации.Name = "ДатаРегистрации";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(997, 76);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(30, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Организации";
            // 
            // Организация
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 370);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.orgsDGV);
            this.Controls.Add(this.panel1);
            this.Name = "Организация";
            this.Text = "Организация";
            this.Load += new System.EventHandler(this.Организация_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orgsDGV)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnForward;
        private Button btnBack;
        private Button btnRem;
        private Button btnUpd;
        private Button btnAdd;
        private TextBox тбДР;
        private TextBox тбСД;
        private TextBox тбНаименование;
        private TextBox тбНОрг;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private GroupBox groupBox1;
        private DataGridView orgsDGV;
        private Panel panel1;
        private Label label1;
        private DataGridViewTextBoxColumn НОрг;
        private DataGridViewTextBoxColumn Наименование;
        private DataGridViewTextBoxColumn СфераДеятельности;
        private DataGridViewTextBoxColumn ДатаРегистрации;
    }
}