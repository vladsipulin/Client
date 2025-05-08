using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class RegistrationForm : Form
    {
        IUserReg _reg;
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private async void regButton_Click(object sender, EventArgs e)
        {
            bool emptyDataFinded = false;
            foreach (System.Windows.Forms.TextBox textBox in this.Controls.OfType<System.Windows.Forms.TextBox>())
            {
                if (textBox.Text.Equals(String.Empty) && textBox.Name != "тбID")
                {
                    emptyDataFinded = true;
                    break;
                }
            }
            if (!emptyDataFinded)
            {
                _reg = new Registration();

                Random rand = new Random();
                int id = 0;
                bool isUnique = false;

                while (!isUnique)
                {
                    id = rand.Next(10000, 99999);
                    var existing = await _reg.GetIdByRequest(id);
                    if (existing == 0)
                    {
                        isUnique = true;
                    }
                }

                string логин = тбЛогин.Text;
                string пароль = тбПароль.Text;
                string почта = тбПочта.Text;
                string фио = тбФИО.Text;
                string пол = кбПол.Text;
                DateTime др = полеДР.Value;
                Result<int> result;

                bool isValid = PasswordValidation.IsValidPassword(пароль);

                if (isValid)
                {
                    UserReg regNewUser = new UserReg(id, фио, пол, др, логин, пароль, почта);
                    result = await _reg.AddNewUser(regNewUser);

                    if (!result)
                    {
                        MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Успешное добавление нового портье", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Пароль не соответствует требованиям:\n" +
                                    "- Длина не менее 16 символов\n" +
                                    "- Не должен быть предсказуемым\n" +
                                    "- Не должен содержать повторяющиеся символы или группы\n" +
                                    "- Должен содержать прописные и строчные буквы, цифры и спецсимволы",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля для регистрации", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            тбID.Enabled = false;
            тбID.Visible = false;
            label7.Visible = false;
        }
    }
}
