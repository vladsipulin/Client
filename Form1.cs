using System;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;

namespace Client
{
    public partial class AuthorizationForm : Form
    {
        //Источник данных для DGV
        private BindingSource _bsDbClients;
        //редактируемый сотрудник
        private BindingSource _bsCurrentDbClient;
        //работа с БД
        private IUserAuth _repo;
        private bool IsAddMethodCalled = false;
        MainForm mf;
        RecoverForm recf;
        RegistrationForm regf;
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            UserAuth user = new UserAuth(0, тбЛогин.Text, тбПароль.Text);

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
                        break;
                    }
                }

                if (IsUserExists) {
                    MessageBox.Show("Добро пожаловать в систему", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mf.Show();
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
            recf.Show();
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
            mf = new MainForm();
            recf = new RecoverForm();
            regf = new RegistrationForm();
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            regf.Show();
        }
    }
}