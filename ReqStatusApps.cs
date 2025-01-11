using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ReqStatusApps : Form
    {
        MySqlDataAdapter adapter;
        DataTable table;
        bool ifDataNull = false;
        public ReqStatusApps()
        {
            InitializeComponent();
        }

        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        private string GetUserFriendlyErrorMessage(MySqlException ex)
        {
            var message = String.Empty;
            switch (ex.Number)
            {
                case 0:
                    if (ex.InnerException.Message.Contains("Unknown"))
                    {
                        message = "Неверное название схемы или таблицы.";
                    }
                    else if (ex.InnerException.Message.Contains("Access"))
                    {
                        message = "Неверное имя или пароль доступа.";
                    }
                    else
                    {
                        message = ex.Message;
                    }
                    break;
                case 1042:
                    message = "Сервер по указанному адресу не доступен." +
                        "\nОшибка ожидания.";
                    break;
                case 1045:
                    message = "Неверное имя пользователя или пароль, " +
                        "\nпожалуйста, попробуйте еще раз.";
                    break;
                default:
                    message = ex.Message;
                    break;
            }
            return message;
        }

        public class UserRequest
        {
            public string НазваниеГостиницы { get; set; }
            public string НомерКорпуса { get; set; }
            public string НомерЭтажа { get; set; }
            public string НомерКомнаты { get; set; }
            public string ВместимостьКомнаты { get; set; }
            public string ДатаОплаты { get; set; }
            public string ДатаЗаселения { get; set; }
            public string ДатаВыезда { get; set; }
            public string СтоимостьОплаты { get; set; }
            public string СтатусЗаявки { get; set; }
        }

        List<UserRequest> ur = new List<UserRequest>();

        private void FillTextBoxes()
        {
            string message = string.Empty;
            try
            {
                using (var con = GetConnection())
                {
                    using (var cmd = con.CreateCommand())
                    {
                        // Настройка вызова хранимой процедуры
                        cmd.CommandText = "GetRoomDetailsByClient";
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Добавляем параметры для процедуры
                        cmd.Parameters.Add(new MySqlParameter("p_НКл", MySqlDbType.Int32) { Value = Convert.ToInt32(keyLbl.Text) });

                        con.Open();

                        // Выполнение команды и получение результата
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var roomDetails = new UserRequest
                                {
                                    НазваниеГостиницы = reader["НазваниеГостиницы"].ToString(),
                                    НомерКорпуса = reader["НомерКорпуса"].ToString(),
                                    НомерЭтажа = reader["НомерЭтажа"].ToString(),
                                    НомерКомнаты = reader["НомерКомнаты"].ToString(),
                                    ВместимостьКомнаты = reader["ВместимостьКомнаты"].ToString(),
                                    ДатаОплаты = reader["ДатаОплаты"].ToString(),
                                    ДатаЗаселения = reader["ДатаЗаселения"].ToString(),
                                    ДатаВыезда = reader["ДатаВыезда"].ToString(),
                                    СтоимостьОплаты = reader["СтоимостьОплаты"].ToString(),
                                    СтатусЗаявки = reader["СтатусЗаявки"].ToString()
                                };
                                ur.Add(roomDetails);
                            }

                            if (ur.Count != 0)
                            {
                                // Заполнение текстовых полей значениями из результата
                                тбНазваниеГостиницы.Text = ur[0].НазваниеГостиницы;
                                тбНомерКорпуса.Text = ur[0].НомерКорпуса;
                                тбНомерЭтажа.Text = ur[0].НомерЭтажа;
                                тбНомерКомнаты.Text = ur[0].НомерКомнаты;
                                тбВместимость.Text = ur[0].ВместимостьКомнаты;
                                тбДатаОплаты.Text = ur[0].ДатаОплаты;
                                тбДатаЗаселения.Text = ur[0].ДатаЗаселения;
                                тбДатаВыезда.Text = ur[0].ДатаВыезда;
                                тбСтоимостьОплаты.Text = ur[0].СтоимостьОплаты;
                                тбСтатусЗаявки.Text = ur[0].СтатусЗаявки;
                                if (тбСтатусЗаявки.Text.Equals(string.Empty))
                                {
                                    MessageBox.Show("Ваша заявка ещё на рассмотрении. Вернитесь позже", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ifDataNull = true;
                                }
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                message = GetUserFriendlyErrorMessage(ex);
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReqStatusApps_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
            FillTextBoxes();
            if (ifDataNull)
            {
                this.Close();
            }
            else
            {
                foreach (Control control in this.Controls)
                {
                    //устанавливаем для всех объектов типа textBox свойство - только для чтения
                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;
                        textBox.ReadOnly = true;
                    }
                    //устанавливаем для всех объектов типа DateTimePicker свойство - недоступность для изменения значения объекта
                    if (control is DateTimePicker)
                    {
                        DateTimePicker dtp = (DateTimePicker)control;
                        dtp.Enabled = false;
                    }
                }
                тбСтатусЗаявки.ForeColor = тбСтатусЗаявки.Text.Equals("Заселен") ? System.Drawing.Color.ForestGreen : System.Drawing.Color.DarkRed;
            }
        }

        int index = 0;

        private void UpdateTextBoxes()
        {
            // Заполнение текстовых полей текущими данными из списка
            if (ur.Count > 0 && index >= 0 && index < ur.Count)
            {
                тбНазваниеГостиницы.Text = ur[index].НазваниеГостиницы;
                тбНомерКорпуса.Text = ur[index].НомерКорпуса;
                тбНомерЭтажа.Text = ur[index].НомерЭтажа;
                тбНомерКомнаты.Text = ur[index].НомерКомнаты;
                тбВместимость.Text = ur[index].ВместимостьКомнаты;
                тбДатаОплаты.Text = ur[index].ДатаОплаты;
                тбДатаЗаселения.Text = ur[index].ДатаЗаселения;
                тбДатаВыезда.Text = ur[index].ДатаВыезда;
                тбСтоимостьОплаты.Text = ur[index].СтоимостьОплаты;
                тбСтатусЗаявки.Text = ur[index].СтатусЗаявки;

                if (string.IsNullOrEmpty(тбСтатусЗаявки.Text))
                {
                    тбСтатусЗаявки.Text = "Ожидает рассмотрения";
                    тбСтатусЗаявки.ForeColor = Color.Orange;
                    //MessageBox.Show("Ваша заявка ещё на рассмотрении. Вернитесь позже", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ifDataNull = true;
                }
                else if (тбСтатусЗаявки.Text.Equals("Заселен"))
                    тбСтатусЗаявки.ForeColor = Color.ForestGreen;
                else
                    тбСтатусЗаявки.ForeColor = Color.DarkRed;
            }
            else
            {
                MessageBox.Show("Нет данных для отображения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (index > 0) // Проверка, чтобы не выйти за пределы списка
            {
                index--;
                UpdateTextBoxes(); // Обновляем текстовые поля
            }
            else
            {
                MessageBox.Show("Вы уже на первой записи.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (index < ur.Count - 1) // Проверка, чтобы не выйти за пределы списка
            {
                index++;
                UpdateTextBoxes(); // Обновляем текстовые поля
            }
            else
            {
                MessageBox.Show("Вы уже на последней записи.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
