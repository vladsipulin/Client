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
            //UserAuth user = new UserAuth(0, "admin", "admin");
            string userAddInfo = String.Empty;

            _repo = new Authorization();

            var result = await _repo.GetUser();
            if (result)
            {
                //извлекаем
                List<UserAuth> users = result.Value;
                //пронумеровываем
                bool IsUserExists = false;
                foreach (var element in users)
                {
                    if (Equals(element.Ћогин, user.Ћогин) & Equals(element.ѕароль, user.ѕароль))
                    {
                        IsUserExists = true;
                        userAddInfo = element.UserType;
                        user.Ќ л = element.Ќ л;
                        break;
                    }
                }

                if (IsUserExists)
                {
                    try
                    {
                        Convert.ToInt32(userAddInfo);
                        MessageBox.Show("ƒобро пожаловать в систему, сотрудник", "»нформаци€", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainForm mf = new MainForm();
                        mf.lbWhoLogged.Text = "—отрудник:";
                        mf.keyLbl.Text = user.Ќ л.ToString();
                        mf.Show();
                    }
                    catch
                    {
                        MessageBox.Show("ƒобро пожаловать в систему, клиент", "»нформаци€", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainForm mf = new MainForm();
                        mf.lbWhoLogged.Text = " лиент:";
                        mf.keyLbl.Text = user.Ќ л.ToString();
                        mf.Show();
                    }
                }
                else
                {
                    MessageBox.Show("¬веденный пользователь не существует/неверно набраны логин или пароль", "ќшибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //users.ForEach(e => Equals(e.Ћогин, user.Ћогин) ? i++ : user.Ћогин = "sdd");
                //отображаем
                //_bsDbClients.DataSource = users;
                //_bsDbClients.MoveFirst();
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
    }
}