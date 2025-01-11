using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Client.Services;
using Client.Models;
using Client.Interfaces;
using MySqlX.XDevAPI.Common;
using Client.Utils;

namespace Client
{
    public partial class RecoverForm : Form
    {
        private IUserRecover _rec;
        private int code;
        private UserRecover _ur;
        public RecoverForm()
        {
            InitializeComponent();
        }

        public static async void SendEmail(string clientmail, string subject, string body)
        {
            await Task.Run(() =>
            {
                try
                {
                    MailAddress from = new MailAddress("vladsipulin@mail.ru", "Гостиничный комплекс");
                    MailAddress to = new MailAddress(clientmail);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = subject;
                    m.Body = body;
                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    smtp.Credentials = new NetworkCredential("vladsipulin@mail.ru", "bPqbjmw61PcTD1NEw6nT");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
                catch
                {
                    MessageBox.Show("Ошибка при попытке отправки письма", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            if (!тбПочта.Text.Equals(String.Empty))
            {
                Random rnd = new Random();
                code = rnd.Next(100000, 999999);
                string mail = тбПочта.Text;
                var result = _rec.FindUser(mail);
                if (result.ФИО != "<ФИО>")
                {
                    string username = result.ФИО;
                    _ur = new UserRecover(mail, username);
                    SendEmail(mail, "Код для восстановления пароля", "Здравствуйте, " + username + ".\nВаш код для восстановления пароля: " + code + "");
                    MessageBox.Show("Код восстановления успешно отправлен по адресу: " + mail, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    тбКВ.Enabled = true;
                    тбНП.Enabled = true;
                    changePassBtn.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Пользователя с такой почтой не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Введите значение в поле ввода электронной почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RecoverForm_Load(object sender, EventArgs e)
        {
            _rec = new Recovery();
            тбКВ.Enabled = true;
            тбНП.Enabled = true;
            changePassBtn.Enabled = true;
        }

        private void changePassBtn_Click(object sender, EventArgs e)
        {
            if (Equals(code.ToString(), тбКВ.Text))
            {
                var result = _rec.UpdatePasswordOfUser(_ur,тбНП.Text);
                if (!result)
                {
                    MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Ваш пароль был успешно изменен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Код восстановления неверный", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}
