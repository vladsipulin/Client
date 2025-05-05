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
using System.Configuration;
using Client.Models;
using Client.Interfaces;
using Client.Services;
using Microsoft.VisualBasic.ApplicationServices;

namespace Client
{
    public partial class AuthorizeClient : Form
    {
        IUserRecover _rec;
        int code;
        UserRecover _ur;
        Services.Authorization _repo;
        string userEmail;

        public AuthorizeClient()
        {
            InitializeComponent();
            PanelCentered();
        }

        public static async void SendEmail(string clientmail, string subject, string body)
        {
            await Task.Run(() =>
            {
                try
                {
                    MailAddress from = new MailAddress("vladsipulin@mail.ru", "База отдыха «Обуховка»");
                    MailAddress to = new MailAddress(clientmail);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = subject;
                    m.Body = body;
                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUsername"],
                                                             ConfigurationManager.AppSettings["SmtpPassword"]);
                    //smtp.Credentials = new NetworkCredential("vladsipulin@mail.ru", "bPqbjmw61PcTD1NEw6nT");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                    MessageBox.Show("Код авторизации успешно отправлен по адресу: " + clientmail, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка при попытке отправки письма", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void PanelCentered()
        {
            int leftPadding = (ClientSize.Width - panel1.Width) / 2;
            int topPadding = (ClientSize.Height - panel1.Height) / 2;
            panel1.Location = new Point(leftPadding, topPadding);
        }

        private void AuthorizeClient_SizeChanged(object sender, EventArgs e)
        {

        }

        private void AuthorizeClient_Load(object sender, EventArgs e)
        {
            BTN_CANCEL.Visible = false;
            _rec = new Recovery();
        }

        private async void BTN_AUTHORIZE_Click(object sender, EventArgs e)
        {
            if (BTN_AUTHORIZE.Text.Equals("Получить код авторизации"))
            {
                if (!TB_EMAIL.Text.Equals(String.Empty))
                {
                    Random rnd = new Random();
                    code = rnd.Next(100000, 999999);
                    string mail = TB_EMAIL.Text;
                    var result = _rec.FindUser(mail);
                    if (result.ФИО != "<ФИО>")
                    {
                        string username = result.ФИО;
                        _ur = new UserRecover(mail, username, result.Id);
                        SendEmail(mail, "Код авторизации клиента в систему заказа услуг", "Здравствуйте, " + username + ".\nВы запросили код авторизации в систему: " + code + "\n");
                        BTN_CANCEL.Visible = true;
                        LBL_HEADER.Text = "Введите код авторизации:";
                        BTN_AUTHORIZE.Text = "Авторизоваться в систему";
                        userEmail = TB_EMAIL.Text;
                        TB_EMAIL.Text = String.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Клиента с такой почтой не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Введите значение в поле ввода электронной почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (TB_EMAIL.Text.Equals(code.ToString()))
                {
                    MessageBox.Show("Добро пожаловать!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Terminal_MainForm tmf = new Terminal_MainForm();
                    tmf.lbWhoLogged.Text = "Клиент:";
                    tmf.keyLbl.Text = _ur.Id.ToString();
                    tmf.ShowDialog();
                    BTN_CANCEL.Visible = false;
                    BTN_AUTHORIZE.Text = "Получить код авторизации";
                    LBL_HEADER.Text = "Email клиента:";
                    TB_EMAIL.Text = String.Empty;
                    //_repo = new Services.Authorization();
                    //int clientID = -1;

                    //var result = await _repo.GetClient();
                    //if (result)
                    //{

                    //    List<UserRecover> users = result.Value;
                    //    foreach (var element in users)
                    //    {
                    //        //почты у каждого клиента уникальные, поэтому ситуации
                    //        //когда у разных клиентов одинаковые почты – не будет
                    //        if (Equals(element.ЭлПочта, userEmail))
                    //        {
                    //            clientID = element.Id;
                    //            break;
                    //        }
                    //    }
                    //    if (clientID != -1)
                    //    {
                    //        MessageBox.Show("Добро пожаловать!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        Terminal_MainForm tmf = new Terminal_MainForm();
                    //        tmf.lbWhoLogged.Text = "Клиент:";
                    //        tmf.keyLbl.Text = clientID.ToString();
                    //        tmf.ShowDialog();
                    //        BTN_CANCEL.Visible = false;
                    //        BTN_AUTHORIZE.Text = "Получить код авторизации";
                    //        LBL_HEADER.Text = "Email клиента:";
                    //        TB_EMAIL.Text = String.Empty;
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Не найден клиент по email", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Ошибка в получении сведений о клиентах в базе данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                else
                {
                    MessageBox.Show("Неверный код авторизации, повторите попытку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            BTN_CANCEL.Visible = false;
            BTN_AUTHORIZE.Text = "Получить код авторизации";
            LBL_HEADER.Text = "Email клиента:";
            TB_EMAIL.Text = String.Empty;
        }

        private void AuthorizeClient_Resize(object sender, EventArgs e)
        {
            PanelCentered();
        }
    }
}
