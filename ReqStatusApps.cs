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
                            if (reader.Read())
                            {
                                // Заполнение текстовых полей значениями из результата
                                тбНазваниеГостиницы.Text = reader["НазваниеГостиницы"].ToString();
                                тбНомерКорпуса.Text = reader["НомерКорпуса"].ToString();
                                тбНомерЭтажа.Text = reader["НомерЭтажа"].ToString();
                                тбНомерКомнаты.Text = reader["НомерКомнаты"].ToString();
                                тбВместимость.Text = reader["ВместимостьКомнаты"].ToString();
                                тбДатаОплаты.Text = reader["ДатаОплаты"].ToString();
                                тбДатаЗаселения.Text = reader["ДатаЗаселения"].ToString();
                                тбДатаВыезда.Text = reader["ДатаВыезда"].ToString();
                                тбСтоимостьОплаты.Text = reader["СтоимостьОплаты"].ToString();
                                тбСтатусЗаявки.Text = reader["СтатусЗаявки"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Ваша заявка ещё на рассмотрении. Вернитесь позже", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ifDataNull = true;
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
    }
}
