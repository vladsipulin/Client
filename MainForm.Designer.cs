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
            label3 = new Label();
            label4 = new Label();
            clientsButton = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            lbWhoLogged = new Label();
            keyLbl = new Label();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(199, 21);
            label1.Name = "label1";
            label1.Size = new Size(277, 32);
            label1.TabIndex = 0;
            label1.Text = "Гостиничный комплекс";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(51, 70);
            label2.Name = "label2";
            label2.Size = new Size(130, 25);
            label2.TabIndex = 1;
            label2.Text = "Справочники";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(343, 70);
            label3.Name = "label3";
            label3.Size = new Size(87, 25);
            label3.TabIndex = 2;
            label3.Text = "Запросы";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(343, 252);
            label4.Name = "label4";
            label4.Size = new Size(76, 25);
            label4.TabIndex = 3;
            label4.Text = "Отчеты";
            // 
            // clientsButton
            // 
            clientsButton.Location = new Point(38, 109);
            clientsButton.Name = "clientsButton";
            clientsButton.Size = new Size(143, 23);
            clientsButton.TabIndex = 4;
            clientsButton.Text = "Клиенты";
            clientsButton.UseVisualStyleBackColor = true;
            clientsButton.Click += button1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(38, 138);
            button1.Name = "button1";
            button1.Size = new Size(143, 23);
            button1.TabIndex = 5;
            button1.Text = "Организации";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.Location = new Point(38, 167);
            button2.Name = "button2";
            button2.Size = new Size(143, 23);
            button2.TabIndex = 6;
            button2.Text = "Должности";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(38, 196);
            button3.Name = "button3";
            button3.Size = new Size(143, 23);
            button3.TabIndex = 7;
            button3.Text = "Заявка на заселение";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(675, 9);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(113, 15);
            lbWhoLogged.TabIndex = 8;
            lbWhoLogged.Text = "Клиент/Сотрудник:";
            // 
            // keyLbl
            // 
            keyLbl.AutoSize = true;
            keyLbl.Location = new Point(675, 29);
            keyLbl.Name = "keyLbl";
            keyLbl.Size = new Size(34, 15);
            keyLbl.TabIndex = 9;
            keyLbl.Text = "login";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(keyLbl);
            Controls.Add(lbWhoLogged);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(clientsButton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "ГлавнаяФорма";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private BindingSource bindingSource1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button clientsButton;
        private Button button1;
        private Button button2;
        private Button button3;
        internal Label keyLbl;
        internal Label lbWhoLogged;
    }
}