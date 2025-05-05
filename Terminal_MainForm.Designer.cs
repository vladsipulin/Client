using System.Windows.Forms;

namespace Client
{
    partial class Terminal_MainForm
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
            mainPanel = new Panel();
            BTN_LEAVE = new Button();
            BTN_OPEN_SERVICEBOOK = new Button();
            BTN_OPEN_RETURNPAYMENT = new Button();
            label6 = new Label();
            keyLbl = new Label();
            lbWhoLogged = new Label();
            BTN_OPEN_CART = new Button();
            BTN_RETURN = new Button();
            panel1 = new Panel();
            BTN_OPEN_CALLBACK_FORM = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.AutoScroll = true;
            mainPanel.BackColor = Color.Transparent;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(977, 462);
            mainPanel.TabIndex = 2;
            mainPanel.Visible = false;
            // 
            // BTN_LEAVE
            // 
            BTN_LEAVE.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN_LEAVE.BackColor = Color.White;
            BTN_LEAVE.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BTN_LEAVE.Location = new Point(638, 0);
            BTN_LEAVE.Name = "BTN_LEAVE";
            BTN_LEAVE.Size = new Size(184, 44);
            BTN_LEAVE.TabIndex = 16;
            BTN_LEAVE.Text = "Выйти из аккаунта";
            BTN_LEAVE.UseVisualStyleBackColor = false;
            BTN_LEAVE.Click += BTN_LEAVE_Click;
            // 
            // BTN_OPEN_SERVICEBOOK
            // 
            BTN_OPEN_SERVICEBOOK.BackColor = Color.White;
            BTN_OPEN_SERVICEBOOK.FlatAppearance.BorderColor = SystemColors.Control;
            BTN_OPEN_SERVICEBOOK.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            BTN_OPEN_SERVICEBOOK.Location = new Point(3, 98);
            BTN_OPEN_SERVICEBOOK.Name = "BTN_OPEN_SERVICEBOOK";
            BTN_OPEN_SERVICEBOOK.Size = new Size(260, 123);
            BTN_OPEN_SERVICEBOOK.TabIndex = 0;
            BTN_OPEN_SERVICEBOOK.Text = "Заказать услуги";
            BTN_OPEN_SERVICEBOOK.UseVisualStyleBackColor = false;
            BTN_OPEN_SERVICEBOOK.Click += BTN_OPEN_SERVICEBOOK_Click;
            // 
            // BTN_OPEN_RETURNPAYMENT
            // 
            BTN_OPEN_RETURNPAYMENT.BackColor = Color.White;
            BTN_OPEN_RETURNPAYMENT.FlatAppearance.BorderColor = SystemColors.Control;
            BTN_OPEN_RETURNPAYMENT.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            BTN_OPEN_RETURNPAYMENT.Location = new Point(561, 98);
            BTN_OPEN_RETURNPAYMENT.Name = "BTN_OPEN_RETURNPAYMENT";
            BTN_OPEN_RETURNPAYMENT.Size = new Size(261, 123);
            BTN_OPEN_RETURNPAYMENT.TabIndex = 1;
            BTN_OPEN_RETURNPAYMENT.Text = "Возврат средств за услуги";
            BTN_OPEN_RETURNPAYMENT.UseVisualStyleBackColor = false;
            BTN_OPEN_RETURNPAYMENT.Click += BTN_OPEN_RETURNPAYMENT_Click;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.BackColor = Color.LightSteelBlue;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Location = new Point(803, 36);
            label6.Name = "label6";
            label6.Size = new Size(108, 15);
            label6.TabIndex = 10;
            label6.Text = "ID пользователя:";
            label6.Visible = false;
            // 
            // keyLbl
            // 
            keyLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            keyLbl.AutoSize = true;
            keyLbl.BackColor = Color.LightSteelBlue;
            keyLbl.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            keyLbl.ForeColor = SystemColors.ActiveCaptionText;
            keyLbl.Location = new Point(917, 36);
            keyLbl.Name = "keyLbl";
            keyLbl.Size = new Size(44, 15);
            keyLbl.TabIndex = 11;
            keyLbl.Text = "123456";
            keyLbl.Visible = false;
            // 
            // lbWhoLogged
            // 
            lbWhoLogged.AutoSize = true;
            lbWhoLogged.Location = new Point(626, 9);
            lbWhoLogged.Name = "lbWhoLogged";
            lbWhoLogged.Size = new Size(38, 15);
            lbWhoLogged.TabIndex = 12;
            lbWhoLogged.Text = "label5";
            lbWhoLogged.Visible = false;
            // 
            // BTN_OPEN_CART
            // 
            BTN_OPEN_CART.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN_OPEN_CART.BackColor = Color.White;
            BTN_OPEN_CART.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BTN_OPEN_CART.Location = new Point(822, 25);
            BTN_OPEN_CART.Name = "BTN_OPEN_CART";
            BTN_OPEN_CART.Size = new Size(120, 55);
            BTN_OPEN_CART.TabIndex = 13;
            BTN_OPEN_CART.Text = "Подтвердить заказ";
            BTN_OPEN_CART.UseVisualStyleBackColor = false;
            BTN_OPEN_CART.Click += BTN_OPEN_CART_Click;
            // 
            // BTN_RETURN
            // 
            BTN_RETURN.BackColor = Color.White;
            BTN_RETURN.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BTN_RETURN.Location = new Point(25, 25);
            BTN_RETURN.Name = "BTN_RETURN";
            BTN_RETURN.Size = new Size(90, 55);
            BTN_RETURN.TabIndex = 14;
            BTN_RETURN.Text = "Назад";
            BTN_RETURN.UseVisualStyleBackColor = false;
            BTN_RETURN.Click += BTN_RETURN_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.Controls.Add(BTN_LEAVE);
            panel1.Controls.Add(BTN_OPEN_CALLBACK_FORM);
            panel1.Controls.Add(BTN_OPEN_SERVICEBOOK);
            panel1.Controls.Add(BTN_OPEN_RETURNPAYMENT);
            panel1.Location = new Point(98, 114);
            panel1.Name = "panel1";
            panel1.Size = new Size(822, 236);
            panel1.TabIndex = 15;
            // 
            // BTN_OPEN_CALLBACK_FORM
            // 
            BTN_OPEN_CALLBACK_FORM.BackColor = Color.White;
            BTN_OPEN_CALLBACK_FORM.FlatAppearance.BorderColor = SystemColors.Control;
            BTN_OPEN_CALLBACK_FORM.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            BTN_OPEN_CALLBACK_FORM.Location = new Point(284, 98);
            BTN_OPEN_CALLBACK_FORM.Name = "BTN_OPEN_CALLBACK_FORM";
            BTN_OPEN_CALLBACK_FORM.Size = new Size(260, 123);
            BTN_OPEN_CALLBACK_FORM.TabIndex = 2;
            BTN_OPEN_CALLBACK_FORM.Text = "Обратная связь по качеству обслуживания";
            BTN_OPEN_CALLBACK_FORM.UseVisualStyleBackColor = false;
            BTN_OPEN_CALLBACK_FORM.Click += BTN_OPEN_CALLBACK_FORM_Click;
            // 
            // Terminal_MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 462);
            Controls.Add(panel1);
            Controls.Add(BTN_OPEN_CART);
            Controls.Add(BTN_RETURN);
            Controls.Add(mainPanel);
            Controls.Add(lbWhoLogged);
            Controls.Add(label6);
            Controls.Add(keyLbl);
            MinimumSize = new Size(650, 350);
            Name = "Terminal_MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Главная форма терминала";
            Load += Terminal_MainForm_Load;
            SizeChanged += Terminal_MainForm_SizeChanged;
            Resize += Terminal_MainForm_Resize;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel mainPanel;
        private Button BTN_OPEN_SERVICEBOOK;
        private Button BTN_OPEN_RETURNPAYMENT;
        internal Label label6;
        internal Label keyLbl;
        internal Label lbWhoLogged;
        private Button BTN_OPEN_CART;
        private Button BTN_RETURN;
        private Panel panel1;
        private Button BTN_OPEN_CALLBACK_FORM;
        private Button BTN_LEAVE;
    }
}