namespace Client
{
    partial class UserProfile
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
            тбНКл = new TextBox();
            lbWhoLogged = new Label();
            panel1 = new Panel();
            keyLbl = new Label();
            label12 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label1 = new Label();
            тбДатаРождения = new DateTimePicker();
            тбEmail = new TextBox();
            тбПол = new TextBox();
            тбФИО = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            updateButton = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // тбНКл
            // 
            тбНКл.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            тбНКл.Location = new Point(176, 99);
            тбНКл.Name = "тбНКл";
            тбНКл.Size = new Size(246, 23);
            тбНКл.TabIndex = 1;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(753, 29);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(59, 15);
            lbWhoLogged.TabIndex = 16;
            lbWhoLogged.Text = "username";
            lbWhoLogged.Visible = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.Controls.Add(keyLbl);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(lbWhoLogged);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(455, 78);
            panel1.TabIndex = 9;
            // 
            // keyLbl
            // 
            keyLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            keyLbl.AutoSize = true;
            keyLbl.Location = new Point(374, 33);
            keyLbl.Name = "keyLbl";
            keyLbl.Size = new Size(59, 15);
            keyLbl.TabIndex = 17;
            keyLbl.Text = "username";
            keyLbl.Visible = false;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Location = new Point(843, 29);
            label12.Name = "label12";
            label12.Size = new Size(79, 15);
            label12.TabIndex = 18;
            label12.Text = "Дата оплаты:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dateTimePicker1.Location = new Point(842, 47);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(143, 23);
            dateTimePicker1.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(30, 19);
            label1.Margin = new Padding(10);
            label1.Name = "label1";
            label1.Size = new Size(224, 32);
            label1.TabIndex = 0;
            label1.Text = "Профиль клиента";
            // 
            // тбДатаРождения
            // 
            тбДатаРождения.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            тбДатаРождения.Location = new Point(176, 185);
            тбДатаРождения.Name = "тбДатаРождения";
            тбДатаРождения.Size = new Size(246, 23);
            тбДатаРождения.TabIndex = 10;
            // 
            // тбEmail
            // 
            тбEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            тбEmail.Location = new Point(176, 215);
            тбEmail.Name = "тбEmail";
            тбEmail.Size = new Size(246, 23);
            тбEmail.TabIndex = 22;
            // 
            // тбПол
            // 
            тбПол.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            тбПол.Location = new Point(176, 156);
            тбПол.Name = "тбПол";
            тбПол.Size = new Size(246, 23);
            тбПол.TabIndex = 20;
            // 
            // тбФИО
            // 
            тбФИО.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            тбФИО.Location = new Point(176, 127);
            тбФИО.Name = "тбФИО";
            тбФИО.Size = new Size(246, 23);
            тбФИО.TabIndex = 19;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(29, 218);
            label6.Name = "label6";
            label6.Size = new Size(113, 15);
            label6.TabIndex = 9;
            label6.Text = "Электронная почта";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 189);
            label5.Name = "label5";
            label5.Size = new Size(90, 15);
            label5.TabIndex = 8;
            label5.Text = "Дата рождения";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 159);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 6;
            label4.Text = "Пол";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 130);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 3;
            label3.Text = "ФИО";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 102);
            label2.Name = "label2";
            label2.Size = new Size(92, 15);
            label2.TabIndex = 2;
            label2.Text = "Номер клиента";
            // 
            // updateButton
            // 
            updateButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            updateButton.Location = new Point(281, 263);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(141, 34);
            updateButton.TabIndex = 23;
            updateButton.Text = "Сохранить изменения";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += updateButton_Click;
            // 
            // UserProfile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 309);
            Controls.Add(updateButton);
            Controls.Add(тбДатаРождения);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(тбEmail);
            Controls.Add(label3);
            Controls.Add(тбПол);
            Controls.Add(label4);
            Controls.Add(тбФИО);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(тбНКл);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MinimumSize = new Size(471, 348);
            Name = "UserProfile";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Профиль клиента";
            Load += UserProfile_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox тбНКл;
        internal Label lbWhoLogged;
        private Panel panel1;
        private Label label12;
        private DateTimePicker dateTimePicker1;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox тбEmail;
        private TextBox тбПол;
        private TextBox тбФИО;
        internal Label keyLbl;
        private DateTimePicker тбДатаРождения;
        private Button updateButton;
    }
}