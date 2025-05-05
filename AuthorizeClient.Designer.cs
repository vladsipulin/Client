namespace Client
{
    partial class AuthorizeClient
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
            BTN_AUTHORIZE = new Button();
            BTN_CANCEL = new Button();
            TB_EMAIL = new TextBox();
            LBL_HEADER = new Label();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // BTN_AUTHORIZE
            // 
            BTN_AUTHORIZE.BackColor = Color.White;
            BTN_AUTHORIZE.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BTN_AUTHORIZE.Location = new Point(16, 118);
            BTN_AUTHORIZE.Name = "BTN_AUTHORIZE";
            BTN_AUTHORIZE.Size = new Size(388, 55);
            BTN_AUTHORIZE.TabIndex = 15;
            BTN_AUTHORIZE.Text = "Получить код авторизации";
            BTN_AUTHORIZE.UseVisualStyleBackColor = false;
            BTN_AUTHORIZE.Click += BTN_AUTHORIZE_Click;
            // 
            // BTN_CANCEL
            // 
            BTN_CANCEL.BackColor = Color.White;
            BTN_CANCEL.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BTN_CANCEL.Location = new Point(16, 195);
            BTN_CANCEL.Name = "BTN_CANCEL";
            BTN_CANCEL.Size = new Size(388, 55);
            BTN_CANCEL.TabIndex = 16;
            BTN_CANCEL.Text = "Отмена";
            BTN_CANCEL.UseVisualStyleBackColor = false;
            BTN_CANCEL.Click += BTN_CANCEL_Click;
            // 
            // TB_EMAIL
            // 
            TB_EMAIL.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TB_EMAIL.Location = new Point(16, 55);
            TB_EMAIL.Name = "TB_EMAIL";
            TB_EMAIL.Size = new Size(388, 35);
            TB_EMAIL.TabIndex = 17;
            // 
            // LBL_HEADER
            // 
            LBL_HEADER.AutoSize = true;
            LBL_HEADER.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            LBL_HEADER.Location = new Point(16, 7);
            LBL_HEADER.Name = "LBL_HEADER";
            LBL_HEADER.Size = new Size(150, 30);
            LBL_HEADER.TabIndex = 18;
            LBL_HEADER.Text = "Email клиента:";
            // 
            // panel1
            // 
            panel1.Controls.Add(BTN_CANCEL);
            panel1.Controls.Add(LBL_HEADER);
            panel1.Controls.Add(BTN_AUTHORIZE);
            panel1.Controls.Add(TB_EMAIL);
            panel1.Location = new Point(193, 49);
            panel1.Name = "panel1";
            panel1.Size = new Size(427, 261);
            panel1.TabIndex = 19;
            // 
            // AuthorizeClient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 353);
            Controls.Add(panel1);
            Name = "AuthorizeClient";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация клиента";
            Load += AuthorizeClient_Load;
            Resize += AuthorizeClient_Resize;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button BTN_AUTHORIZE;
        private Button BTN_CANCEL;
        private TextBox TB_EMAIL;
        private Label LBL_HEADER;
        private Panel panel1;
    }
}