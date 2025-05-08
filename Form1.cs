using System;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;

namespace Client
{
    public partial class AuthorizationForm : Form
    {
        private IUserAuth _repo;

        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            UserAuth user = new UserAuth(0, тбЋогин.Text, тбѕароль.Text);
            string userAddInfo = String.Empty;

            _repo = new Authorization();

            var result = await _repo.GetEmployer();
            if (result)
            {
                List<Employer> employers = result.Value;
                bool IsUserExists = false;
                foreach (var element in employers)
                {
                    if (Equals(element.Ћогин, user.Ћогин) & Equals(element.ѕароль, user.ѕароль))
                    {
                        IsUserExists = true;
                        user.ID = element.ID;
                        break;
                    }
                }

                if (IsUserExists)
                {
                    //MessageBox.Show("ƒобро пожаловать", "»нформаци€", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm mf = new MainForm();
                    mf.lbWhoLogged.Text = "—отрудник:";
                    mf.keyLbl.Text = user.ID.ToString();
                    mf.Show();
                }
                else
                {
                    MessageBox.Show("¬веденный пользователь не существует/неверно набраны логин или пароль", "ќшибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(result.Error, "ќшибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void recoverButton_Click(object sender, EventArgs e)
        {
            RecoverForm recf = new RecoverForm();
            recf.ShowDialog();
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            RegistrationForm regf = new RegistrationForm();
            regf.ShowDialog();
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
            тбЋогин.Text = "admin";
            тбѕароль.Text = "admin";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Terminal_MainForm ac = new Terminal_MainForm();
            AuthorizeClient ac = new AuthorizeClient();
            ac.Show();
        }
    }
}