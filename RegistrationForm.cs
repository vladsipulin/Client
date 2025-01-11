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
            foreach (Control control in this.Controls)
            {
                //устанавливаем для всех объектов типа textBox свойство - только для чтения
                if (control is System.Windows.Forms.TextBox)
                {
                    System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)control;
                    if (textBox.Text.Equals(String.Empty))
                    {
                        emptyDataFinded = true;
                        break;
                    };
                }
            }
            if (!emptyDataFinded)
            {
                _reg = new Registration();
                int нкл = Convert.ToInt32(тбНКл.Text);
                string логин = тбЛогин.Text;
                string пароль = тбПароль.Text;
                string почта = тбПочта.Text;
                string фио = тбФИО.Text;
                string пол = кбПол.Text;
                DateTime др = полеДР.Value;
                Result<int> result;

                UserReg regNewUser = new UserReg(нкл, фио, пол, др, логин, пароль, почта);
                // _reg.CheckIfUserExists(логин, почта)
                result = await _reg.AddNewUser(regNewUser);

                if (!result)
                {
                    MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Вы зарегистрировались как новый клиент!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля для регистрации", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
