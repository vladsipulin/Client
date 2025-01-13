namespace Client
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            bindingSource1 = new BindingSource(components);
            label1 = new Label();
            label2 = new Label();
            clientsButton = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label6 = new Label();
            keyLbl = new Label();
            показатьГостиничныеКомплексы = new Button();
            dataGridView1 = new DataGridView();
            показатьЗаявкиКлиентов = new Button();
            показатьЗаселениеКлиента = new Button();
            updateButton = new Button();
            кбТаблицыБД = new ComboBox();
            label3 = new Label();
            tbSearch = new TextBox();
            btnSearch = new Button();
            кбСтолбцыТаблицы = new ComboBox();
            label4 = new Label();
            panel1 = new Panel();
            label5 = new Label();
            comboBox1 = new ComboBox();
            button8 = new Button();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            lbWhoLogged = new Label();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.LightSteelBlue;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.DarkBlue;
            label1.Location = new Point(182, 21);
            label1.Name = "label1";
            label1.Size = new Size(334, 32);
            label1.TabIndex = 0;
            label1.Text = "ИС гостиничного комплекса";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.DarkBlue;
            label2.Location = new Point(27, 77);
            label2.Name = "label2";
            label2.Size = new Size(71, 20);
            label2.TabIndex = 1;
            label2.Text = "Таблица:";
            // 
            // clientsButton
            // 
            clientsButton.Location = new Point(309, 537);
            clientsButton.Name = "clientsButton";
            clientsButton.Size = new Size(143, 23);
            clientsButton.TabIndex = 4;
            clientsButton.Text = "Клиенты";
            clientsButton.UseVisualStyleBackColor = true;
            clientsButton.Click += button1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(11, 537);
            button1.Name = "button1";
            button1.Size = new Size(143, 23);
            button1.TabIndex = 5;
            button1.Text = "Организации";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.Location = new Point(160, 537);
            button2.Name = "button2";
            button2.Size = new Size(143, 23);
            button2.TabIndex = 6;
            button2.Text = "Должности";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.Location = new Point(604, 75);
            button3.Name = "button3";
            button3.Size = new Size(143, 26);
            button3.TabIndex = 7;
            button3.Text = "Забронировать номер";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.BackColor = Color.LightSteelBlue;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Location = new Point(598, 35);
            label6.Name = "label6";
            label6.Size = new Size(108, 15);
            label6.TabIndex = 8;
            label6.Text = "ID пользователя:";
            // 
            // keyLbl
            // 
            keyLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            keyLbl.AutoSize = true;
            keyLbl.BackColor = Color.LightSteelBlue;
            keyLbl.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            keyLbl.ForeColor = SystemColors.ActiveCaptionText;
            keyLbl.Location = new Point(713, 35);
            keyLbl.Name = "keyLbl";
            keyLbl.Size = new Size(44, 15);
            keyLbl.TabIndex = 9;
            keyLbl.Text = "123456";
            // 
            // показатьГостиничныеКомплексы
            // 
            показатьГостиничныеКомплексы.Location = new Point(458, 537);
            показатьГостиничныеКомплексы.Name = "показатьГостиничныеКомплексы";
            показатьГостиничныеКомплексы.Size = new Size(87, 23);
            показатьГостиничныеКомплексы.TabIndex = 10;
            показатьГостиничныеКомплексы.Text = "Гостиничный комплекс";
            показатьГостиничныеКомплексы.UseVisualStyleBackColor = true;
            показатьГостиничныеКомплексы.Click += показатьГостиничныеКомплексы_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(25, 307);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(722, 224);
            dataGridView1.TabIndex = 11;
            // 
            // показатьЗаявкиКлиентов
            // 
            показатьЗаявкиКлиентов.Location = new Point(551, 537);
            показатьЗаявкиКлиентов.Name = "показатьЗаявкиКлиентов";
            показатьЗаявкиКлиентов.Size = new Size(99, 23);
            показатьЗаявкиКлиентов.TabIndex = 12;
            показатьЗаявкиКлиентов.Text = "Заявки на заселение клиентов";
            показатьЗаявкиКлиентов.UseVisualStyleBackColor = true;
            показатьЗаявкиКлиентов.Click += показатьЗаявкиКлиентов_Click;
            // 
            // показатьЗаселениеКлиента
            // 
            показатьЗаселениеКлиента.Location = new Point(656, 537);
            показатьЗаселениеКлиента.Name = "показатьЗаселениеКлиента";
            показатьЗаселениеКлиента.Size = new Size(102, 23);
            показатьЗаселениеКлиента.TabIndex = 13;
            показатьЗаселениеКлиента.Text = "Заселение клиента";
            показатьЗаселениеКлиента.UseVisualStyleBackColor = true;
            показатьЗаселениеКлиента.Click += показатьЗаселениеКлиента_Click;
            // 
            // updateButton
            // 
            updateButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            updateButton.Location = new Point(604, 252);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(143, 39);
            updateButton.TabIndex = 14;
            updateButton.Text = "Сохранить изменения в таблице";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += updateButton_Click;
            // 
            // кбТаблицыБД
            // 
            кбТаблицыБД.FormattingEnabled = true;
            кбТаблицыБД.Location = new Point(147, 76);
            кбТаблицыБД.Name = "кбТаблицыБД";
            кбТаблицыБД.Size = new Size(200, 23);
            кбТаблицыБД.TabIndex = 15;
            кбТаблицыБД.SelectionChangeCommitted += кбТаблицыБД_SelectionChangeCommitted;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(27, 261);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 16;
            label3.Text = "Поиск:";
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(107, 261);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(200, 23);
            tbSearch.TabIndex = 17;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(326, 261);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 18;
            btnSearch.Text = "Найти";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += button4_Click;
            // 
            // кбСтолбцыТаблицы
            // 
            кбСтолбцыТаблицы.FormattingEnabled = true;
            кбСтолбцыТаблицы.Location = new Point(147, 121);
            кбСтолбцыТаблицы.Name = "кбСтолбцыТаблицы";
            кбСтолбцыТаблицы.Size = new Size(200, 23);
            кбСтолбцыТаблицы.TabIndex = 20;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.LightSteelBlue;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.DarkBlue;
            label4.Location = new Point(27, 124);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 19;
            label4.Text = "Столбец:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Controls.Add(кбСтолбцыТаблицы);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(кбТаблицыБД);
            panel1.Controls.Add(button8);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(lbWhoLogged);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(keyLbl);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(769, 246);
            panel1.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.LightSteelBlue;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.DarkBlue;
            label5.Location = new Point(27, 172);
            label5.Name = "label5";
            label5.Size = new Size(114, 20);
            label5.TabIndex = 22;
            label5.Text = "Бизнес-форма:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Создать группу для организации", "Создать договор с организацией", "Заселить группу организации", "Заселить клиента по заявке", "Провести покупку услуги по заявке клиента" });
            comboBox1.Location = new Point(147, 173);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(200, 23);
            comboBox1.TabIndex = 27;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button8
            // 
            button8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button8.Location = new Point(604, 138);
            button8.Name = "button8";
            button8.Size = new Size(143, 26);
            button8.TabIndex = 26;
            button8.Text = "Отзыв на услугу";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button7
            // 
            button7.Location = new Point(427, 140);
            button7.Name = "button7";
            button7.Size = new Size(171, 24);
            button7.TabIndex = 25;
            button7.Text = "Отзыв на заселение";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button6.Location = new Point(427, 109);
            button6.Name = "button6";
            button6.Size = new Size(171, 23);
            button6.TabIndex = 24;
            button6.Text = "Заявка на службу быта";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button5.Location = new Point(427, 76);
            button5.Name = "button5";
            button5.Size = new Size(171, 25);
            button5.TabIndex = 23;
            button5.Text = "Статус заявки на заселение";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button4.Location = new Point(604, 107);
            button4.Name = "button4";
            button4.Size = new Size(143, 26);
            button4.TabIndex = 22;
            button4.Text = "Профиль клиента";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click_1;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(598, 9);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(38, 15);
            lbWhoLogged.TabIndex = 10;
            lbWhoLogged.Text = "label5";
            lbWhoLogged.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(769, 574);
            Controls.Add(label4);
            Controls.Add(btnSearch);
            Controls.Add(tbSearch);
            Controls.Add(label3);
            Controls.Add(updateButton);
            Controls.Add(показатьЗаселениеКлиента);
            Controls.Add(показатьЗаявкиКлиентов);
            Controls.Add(dataGridView1);
            Controls.Add(показатьГостиничныеКомплексы);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(clientsButton);
            Controls.Add(label2);
            Controls.Add(panel1);
            MinimumSize = new Size(785, 604);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ИС гостиничного комплекса";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private BindingSource bindingSource1;
        private Label label1;
        private Label label2;
        private Button clientsButton;
        private Button button1;
        private Button button2;
        private Button button3;
        internal Label keyLbl;
        internal Label label6;
        private Button показатьГостиничныеКомплексы;
        private DataGridView dataGridView1;
        private Button показатьЗаявкиКлиентов;
        private Button показатьЗаселениеКлиента;
        private Button updateButton;
        private ComboBox кбТаблицыБД;
        private Label label3;
        private TextBox tbSearch;
        private Button btnSearch;
        private ComboBox кбСтолбцыТаблицы;
        private Label label4;
        private Panel panel1;
        internal Label lbWhoLogged;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Label label5;
        private ComboBox comboBox1;
    }
}