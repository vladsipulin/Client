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
            UserAuth user = new UserAuth(0, тбЛогин.Text, тбПароль.Text);
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
                    if (Equals(element.Логин, user.Логин) & Equals(element.Пароль, user.Пароль))
                    {
                        IsUserExists = true;
                        userAddInfo = element.UserType;
                        user.НКл = element.НКл;
                        break;
                    }
                }

                if (IsUserExists) {
                    try
                    {
                        Convert.ToInt32(userAddInfo);
                        MessageBox.Show("Добро пожаловать в систему, сотрудник", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainForm mf = new MainForm();
                        mf.lbWhoLogged.Text = "Сотрудник:";
                        mf.keyLbl.Text = user.НКл.ToString();
                        mf.Show();
                    }
                    catch
                    {
                        MessageBox.Show("Добро пожаловать в систему, клиент", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainForm mf = new MainForm();
                        mf.lbWhoLogged.Text = "Клиент:";
                        mf.keyLbl.Text = user.НКл.ToString();
                        mf.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Введенный пользователь не существует/неверно набраны логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //users.ForEach(e => Equals(e.Логин, user.Логин) ? i++ : user.Логин = "sdd");
                //отображаем
                //_bsDbClients.DataSource = users;
                //_bsDbClients.MoveFirst();
            }
            else
            {
                MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}