namespace Client
{
    partial class Должность
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRem = new System.Windows.Forms.Button();
            this.btnUpd = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.тб4 = new System.Windows.Forms.TextBox();
            this.тб3 = new System.Windows.Forms.TextBox();
            this.тб2 = new System.Windows.Forms.TextBox();
            this.тб1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataTable = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.НД = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Название = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Оклад = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Занятость = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(150, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Должности";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnForward);
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Controls.Add(this.btnRem);
            this.groupBox1.Controls.Add(this.btnUpd);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.тб4);
            this.groupBox1.Controls.Add(this.тб3);
            this.groupBox1.Controls.Add(this.тб2);
            this.groupBox1.Controls.Add(this.тб1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 237);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Должность";
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
            // тб4
            // 
            this.тб4.Location = new System.Drawing.Point(148, 113);
            this.тб4.Name = "тб4";
            this.тб4.Size = new System.Drawing.Size(220, 23);
            this.тб4.TabIndex = 7;
            // 
            // тб3
            // 
            this.тб3.Location = new System.Drawing.Point(148, 84);
            this.тб3.Name = "тб3";
            this.тб3.Size = new System.Drawing.Size(220, 23);
            this.тб3.TabIndex = 6;
            // 
            // тб2
            // 
            this.тб2.Location = new System.Drawing.Point(148, 55);
            this.тб2.Name = "тб2";
            this.тб2.Size = new System.Drawing.Size(220, 23);
            this.тб2.TabIndex = 5;
            // 
            // тб1
            // 
            this.тб1.Location = new System.Drawing.Point(148, 26);
            this.тб1.Name = "тб1";
            this.тб1.Size = new System.Drawing.Size(220, 23);
            this.тб1.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Занятость";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Номер должности";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Оклад";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Название";
            // 
            // dataTable
            // 
            this.dataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.НД,
            this.Название,
            this.Оклад,
            this.Занятость});
            this.dataTable.Location = new System.Drawing.Point(416, 97);
            this.dataTable.Name = "dataTable";
            this.dataTable.RowTemplate.Height = 25;
            this.dataTable.Size = new System.Drawing.Size(537, 237);
            this.dataTable.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 76);
            this.panel1.TabIndex = 7;
            // 
            // НД
            // 
            this.НД.HeaderText = "Номер";
            this.НД.Name = "НД";
            // 
            // Название
            // 
            this.Название.HeaderText = "Название";
            this.Название.Name = "Название";
            // 
            // Оклад
            // 
            this.Оклад.HeaderText = "Оклад";
            this.Оклад.Name = "Оклад";
            // 
            // Занятость
            // 
            this.Занятость.HeaderText = "Занятость";
            this.Занятость.Name = "Занятость";
            // 
            // Должность
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 362);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataTable);
            this.Controls.Add(this.panel1);
            this.Name = "Должность";
            this.Text = "Должность";
            this.Load += new System.EventHandler(this.Должность_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Button btnForward;
        private Button btnBack;
        private Button btnRem;
        private Button btnUpd;
        private Button btnAdd;
        private TextBox тб4;
        private TextBox тб3;
        private TextBox тб2;
        private TextBox тб1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private DataGridView dataTable;
        private DataGridViewTextBoxColumn НД;
        private DataGridViewTextBoxColumn Название;
        private DataGridViewTextBoxColumn Оклад;
        private DataGridViewTextBoxColumn Занятость;
        private Panel panel1;
    }
}