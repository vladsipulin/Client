namespace Client
{
    partial class Клиенты
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.clientsDGV = new System.Windows.Forms.DataGridView();
            this.НКл = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ФИО = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Пол = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ДатаРождения = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Логин = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Пароль = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRem = new System.Windows.Forms.Button();
            this.btnUpd = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.тбДатаРождения = new System.Windows.Forms.TextBox();
            this.тбПол = new System.Windows.Forms.TextBox();
            this.тбФИО = new System.Windows.Forms.TextBox();
            this.тбНКл = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientsDGV)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(275, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Список всех клиентов";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(979, 76);
            this.panel1.TabIndex = 1;
            // 
            // clientsDGV
            // 
            this.clientsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.НКл,
            this.ФИО,
            this.Пол,
            this.ДатаРождения,
            this.Логин,
            this.Пароль,
            this.Email});
            this.clientsDGV.Location = new System.Drawing.Point(413, 94);
            this.clientsDGV.Name = "clientsDGV";
            this.clientsDGV.RowTemplate.Height = 25;
            this.clientsDGV.Size = new System.Drawing.Size(537, 237);
            this.clientsDGV.TabIndex = 2;
            // 
            // НКл
            // 
            this.НКл.HeaderText = "Номер клиента";
            this.НКл.Name = "НКл";
            // 
            // ФИО
            // 
            this.ФИО.HeaderText = "ФИО";
            this.ФИО.Name = "ФИО";
            // 
            // Пол
            // 
            this.Пол.HeaderText = "Пол";
            this.Пол.Name = "Пол";
            // 
            // ДатаРождения
            // 
            this.ДатаРождения.HeaderText = "Дата рождения";
            this.ДатаРождения.Name = "ДатаРождения";
            // 
            // Логин
            // 
            this.Логин.HeaderText = "Логин";
            this.Логин.Name = "Логин";
            // 
            // Пароль
            // 
            this.Пароль.HeaderText = "Пароль";
            this.Пароль.Name = "Пароль";
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnForward);
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Controls.Add(this.btnRem);
            this.groupBox1.Controls.Add(this.btnUpd);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.тбДатаРождения);
            this.groupBox1.Controls.Add(this.тбПол);
            this.groupBox1.Controls.Add(this.тбФИО);
            this.groupBox1.Controls.Add(this.тбНКл);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 237);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Клиент";
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
            // тбДатаРождения
            // 
            this.тбДатаРождения.Location = new System.Drawing.Point(121, 113);
            this.тбДатаРождения.Name = "тбДатаРождения";
            this.тбДатаРождения.Size = new System.Drawing.Size(247, 23);
            this.тбДатаРождения.TabIndex = 7;
            // 
            // тбПол
            // 
            this.тбПол.Location = new System.Drawing.Point(121, 84);
            this.тбПол.Name = "тбПол";
            this.тбПол.Size = new System.Drawing.Size(247, 23);
            this.тбПол.TabIndex = 6;
            // 
            // тбФИО
            // 
            this.тбФИО.Location = new System.Drawing.Point(121, 55);
            this.тбФИО.Name = "тбФИО";
            this.тбФИО.Size = new System.Drawing.Size(247, 23);
            this.тбФИО.TabIndex = 5;
            // 
            // тбНКл
            // 
            this.тбНКл.Location = new System.Drawing.Point(121, 26);
            this.тбНКл.Name = "тбНКл";
            this.тбНКл.Size = new System.Drawing.Size(247, 23);
            this.тбНКл.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Дата рождения";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Номер клиента";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Пол";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "ФИО";
            // 
            // Клиенты
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 357);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.clientsDGV);
            this.Controls.Add(this.panel1);
            this.Name = "Клиенты";
            this.Text = "Клиенты";
            this.Load += new System.EventHandler(this.Клиенты_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientsDGV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private Panel panel1;
        private DataGridView clientsDGV;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private TextBox тбДатаРождения;
        private TextBox тбПол;
        private TextBox тбФИО;
        private TextBox тбНКл;
        private Label label5;
        private Label label4;
        private Button btnRem;
        private Button btnUpd;
        private Button btnAdd;
        private Button btnForward;
        private Button btnBack;
        private DataGridViewTextBoxColumn НКл;
        private DataGridViewTextBoxColumn ФИО;
        private DataGridViewTextBoxColumn Пол;
        private DataGridViewTextBoxColumn ДатаРождения;
        private DataGridViewTextBoxColumn Логин;
        private DataGridViewTextBoxColumn Пароль;
        private DataGridViewTextBoxColumn Email;
    }
}