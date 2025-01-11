using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;
using MySql.Data.MySqlClient;
using Mysqlx.Resultset;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ReqStatusApps : Form
    {
        MySqlDataAdapter adapter;
        DataTable table;
        List<UserRequest> ur = new List<UserRequest>();
        bool ifDataNull = false;
        int index = 0;
        int idOfHotel = 0;
        float Цена1Ночь = 0;
        private IReqClientApparts _repo;
        bool forUpdateChangeValueInDateTimePickersOnly = true;

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
            public string НГ { get; set; }
            public string НомерКорпуса { get; set; }
            public string НомерЭтажа { get; set; }
            public string НомерКомнаты { get; set; }
            public string ВместимостьКомнаты { get; set; }
            public string Цена1Ночь { get; set; }
            public string ДатаОплаты { get; set; }
            public string ДатаЗаселения { get; set; }
            public string ДатаВыезда { get; set; }
            public string СтоимостьОплаты { get; set; }
            public string Статус { get; set; }
            public string СтатусЗаявки { get; set; }
        }



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
                                    НГ = reader["НГ"].ToString(),
                                    НомерКорпуса = reader["НомерКорпуса"].ToString(),
                                    НомерЭтажа = reader["НомерЭтажа"].ToString(),
                                    НомерКомнаты = reader["НомерКомнаты"].ToString(),
                                    Цена1Ночь = reader["Цена1Ночь"].ToString(),
                                    ВместимостьКомнаты = reader["ВместимостьКомнаты"].ToString(),
                                    ДатаОплаты = reader["ДатаОплаты"].ToString(),
                                    ДатаЗаселения = reader["ДатаЗаселения"].ToString(),
                                    ДатаВыезда = reader["ДатаВыезда"].ToString(),
                                    СтоимостьОплаты = reader["СтоимостьОплаты"].ToString(),
                                    Статус = reader["Статус"].ToString(),
                                    СтатусЗаявки = reader["СтатусЗаявки"].ToString()
                                };
                                ur.Add(roomDetails);
                            }

                            if (ur.Count != 0)
                            {
                                // Заполнение текстовых полей значениями из результата
                                тбНазваниеГостиницы.Text = ur[0].НазваниеГостиницы;
                                idOfHotel = Convert.ToInt16(ur[0].НГ);
                                тбНомерКорпуса.Text = ur[0].НомерКорпуса;
                                тбНомерЭтажа.Text = ur[0].НомерЭтажа;
                                тбНомерКомнаты.Text = ur[0].НомерКомнаты;
                                Цена1Ночь = float.Parse(ur[0].Цена1Ночь);
                                тбВместимость.Text = ur[0].ВместимостьКомнаты;
                                тбДатаОплаты.Text = ur[0].ДатаОплаты;
                                тбДатаЗаселения.Text = ur[0].ДатаЗаселения;
                                тбДатаВыезда.Text = ur[0].ДатаВыезда;
                                тбСтоимостьОплаты.Text = ur[0].СтоимостьОплаты;
                                тбСтатус.Text = ur[0].Статус;
                                тбСтатусЗаявки.Text = ur[0].СтатусЗаявки;
                                if (тбСтатусЗаявки.Text.Equals(string.Empty))
                                {
                                    тбСтатусЗаявки.Text = "Не рассмотренно";
                                    ifDataNull = true;
                                }
                            }
                        }
                        con.Close();
                        forUpdateChangeValueInDateTimePickersOnly = false;
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

            _repo = new RRequestClientApparts();

            foreach (UserRequest row in ur)
            {
                int? drНКоманты = Convert.ToInt32(row.НомерКомнаты);
                int? drНК = Convert.ToInt32(row.НомерКорпуса);
                int? drНЭ = Convert.ToInt32(row.НомерЭтажа);
                float currentPrice = float.Parse(row.Цена1Ночь);
                if (drНКоманты == Convert.ToInt32(тбНомерКомнаты.Text)
                    & drНК == Convert.ToInt32(тбНомерКорпуса.Text)
                    & drНЭ == Convert.ToInt32(тбНомерЭтажа.Text))
                {
                    if (currentPrice != 0)
                    {
                        //pricePerNight = currentPrice;
                        Цена1Ночь = currentPrice;
                        break;
                    }
                    else
                        Цена1Ночь = 0;
                }

            }

            if (ifDataNull)
            {
                тбСтатусЗаявки.ForeColor = Color.DarkRed;
            }
            else
            {
                тбСтатусЗаявки.ForeColor = тбСтатусЗаявки.Text.Equals("Заселен") ? System.Drawing.Color.ForestGreen : System.Drawing.Color.DarkRed;
            }
        }

        private void UpdateTextBoxes()
        {
            // Заполнение текстовых полей текущими данными из списка
            if (ur.Count > 0 && index >= 0 && index < ur.Count)
            {
                тбНазваниеГостиницы.Text = ur[index].НазваниеГостиницы;
                idOfHotel = Convert.ToInt16(ur[index].НГ);
                тбНомерКорпуса.Text = ur[index].НомерКорпуса;
                тбНомерЭтажа.Text = ur[index].НомерЭтажа;
                тбНомерКомнаты.Text = ur[index].НомерКомнаты;
                Цена1Ночь = float.Parse(ur[index].Цена1Ночь);
                тбВместимость.Text = ur[index].ВместимостьКомнаты;
                тбДатаОплаты.Text = ur[index].ДатаОплаты;
                тбДатаЗаселения.Text = ur[index].ДатаЗаселения;
                тбДатаВыезда.Text = ur[index].ДатаВыезда;
                тбСтоимостьОплаты.Text = ur[index].СтоимостьОплаты;
                тбСтатус.Text = ur[index].Статус;
                тбСтатусЗаявки.Text = ur[index].СтатусЗаявки;

                if (string.IsNullOrEmpty(тбСтатусЗаявки.Text))
                {
                    тбСтатусЗаявки.Text = "Не рассмотренно";
                    тбСтатусЗаявки.ForeColor = Color.DarkRed;
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
                forUpdateChangeValueInDateTimePickersOnly = true;
                UpdateTextBoxes(); // Обновляем текстовые поля
                forUpdateChangeValueInDateTimePickersOnly = false;
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
                forUpdateChangeValueInDateTimePickersOnly = true;
                UpdateTextBoxes(); // Обновляем текстовые поля
                forUpdateChangeValueInDateTimePickersOnly = false;
            }
            else
            {
                MessageBox.Show("Вы уже на последней записи.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int НКомнаты = Convert.ToInt16(тбНомерКомнаты.Text);
                int НК = Convert.ToInt16(тбНомерКорпуса.Text);
                int НЭ = Convert.ToInt16(тбНомерЭтажа.Text);
                int НГ = Convert.ToInt16(idOfHotel);
                int clientId = Convert.ToInt16(keyLbl.Text);
                DateTime ДатаОплаты = Convert.ToDateTime(тбДатаОплаты.Text);
                DateTime ДатаЗаселения = Convert.ToDateTime(тбДатаЗаселения.Text);
                DateTime ДатаВыезда = Convert.ToDateTime(тбДатаВыезда.Text);
                float СтоимостьОплаты = float.Parse(тбСтоимостьОплаты.Text);

                RequestClientAppartmnts current = new RequestClientAppartmnts(НКомнаты, НК, НЭ, НГ, clientId, ДатаОплаты, ДатаЗаселения, ДатаВыезда, СтоимостьОплаты);

                Result<int> result;
                result = await _repo.Update(current, НКомнаты, НК, НЭ, НГ);

                if (result)
                {
                    MessageBox.Show($"Содержание заявки успешно изменено!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ur[index].НазваниеГостиницы = тбНазваниеГостиницы.Text;
                    ur[index].НГ = idOfHotel.ToString();
                    ur[index].НомерКорпуса = тбНомерКорпуса.Text;
                    ur[index].НомерЭтажа = тбНомерЭтажа.Text;
                    ur[index].НомерКомнаты = тбНомерКомнаты.Text;
                    ur[index].Цена1Ночь = Цена1Ночь.ToString();
                    ur[index].ВместимостьКомнаты = тбВместимость.Text;
                    ur[index].ДатаОплаты = тбДатаОплаты.Text;
                    ur[index].ДатаЗаселения = тбДатаЗаселения.Text;
                    ur[index].ДатаВыезда = тбДатаВыезда.Text;
                    ur[index].СтоимостьОплаты = тбСтоимостьОплаты.Text;
                    ur[index].Статус = тбСтатус.Text;
                    ur[index].СтатусЗаявки = тбСтатусЗаявки.Text;
                }
                if (!result)
                {
                    MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void тбДатаОплаты_ValueChanged(object sender, EventArgs e)
        {
            if (!forUpdateChangeValueInDateTimePickersOnly)
                тбДатаОплаты.Value = DateTime.Now;
        }

        private void тбДатаВыезда_ValueChanged(object sender, EventArgs e)
        {
            if (!forUpdateChangeValueInDateTimePickersOnly)
            {
                DateTime date1 = тбДатаЗаселения.Value.Date;
                DateTime date2 = тбДатаВыезда.Value.Date;
                if (date2 < date1)
                {
                    MessageBox.Show("Ошибка: дата выезда раньше даты заселения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int nights = (date2 - date1).Days;
                    float roomPrice = Цена1Ночь;
                    float cost = nights * roomPrice;
                    тбСтоимостьОплаты.Text = Convert.ToString(cost);
                }
            }

        }

        private void тбДатаЗаселения_ValueChanged(object sender, EventArgs e)
        {
            if (!forUpdateChangeValueInDateTimePickersOnly)
            {
                DateTime date1 = тбДатаЗаселения.Value.Date;
                DateTime date2 = тбДатаВыезда.Value.Date;
                if (date2 < date1)
                {
                    MessageBox.Show("Ошибка: дата заселения позже даты выезда", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int nights = (date2 - date1).Days;
                    float roomPrice = Цена1Ночь;
                    float cost = nights * roomPrice;
                    тбСтоимостьОплаты.Text = Convert.ToString(cost);
                }
            }

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int НКомнаты = Convert.ToInt16(тбНомерКомнаты.Text);
                int НК = Convert.ToInt16(тбНомерКорпуса.Text);
                int НЭ = Convert.ToInt16(тбНомерЭтажа.Text);
                int НГ = Convert.ToInt16(idOfHotel);
                int clientId = Convert.ToInt16(keyLbl.Text);
                DateTime ДатаОплаты = Convert.ToDateTime(тбДатаОплаты.Text);
                DateTime ДатаЗаселения = Convert.ToDateTime(тбДатаЗаселения.Text);
                DateTime ДатаВыезда = Convert.ToDateTime(тбДатаВыезда.Text);
                float СтоимостьОплаты = float.Parse(тбСтоимостьОплаты.Text);

                RequestClientAppartmnts current = new RequestClientAppartmnts(НКомнаты, НК, НЭ, НГ, clientId, ДатаОплаты, ДатаЗаселения, ДатаВыезда, СтоимостьОплаты);

                if (тбСтатусЗаявки.Text.Equals("Не расмотренно"))
                {
                    тбСтатус.Text = "Отменено клиентом";
                    тбСтатус.ForeColor = Color.DarkRed;
                    Result<int> result;
                    result = await _repo.Remove(current, "Отменено клиентом", НКомнаты, НК, НЭ, НГ);

                    if (result)
                    {
                        MessageBox.Show($"Заявка успешно отменена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ur[index].НазваниеГостиницы = тбНазваниеГостиницы.Text;
                        ur[index].НГ = idOfHotel.ToString();
                        ur[index].НомерКорпуса = тбНомерКорпуса.Text;
                        ur[index].НомерЭтажа = тбНомерЭтажа.Text;
                        ur[index].НомерКомнаты = тбНомерКомнаты.Text;
                        ur[index].Цена1Ночь = Цена1Ночь.ToString();
                        ur[index].ВместимостьКомнаты = тбВместимость.Text;
                        ur[index].ДатаОплаты = тбДатаОплаты.Text;
                        ur[index].ДатаЗаселения = тбДатаЗаселения.Text;
                        ur[index].ДатаВыезда = тбДатаВыезда.Text;
                        ur[index].СтоимостьОплаты = тбСтоимостьОплаты.Text;
                        ur[index].Статус = тбСтатус.Text;
                    }
                    if (!result)
                    {
                        MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Отмена заявки невозможна после её рассмотрения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
