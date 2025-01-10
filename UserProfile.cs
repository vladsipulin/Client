using Client.Interfaces;
using Client.Services;
using Client.Models;
using MySql.Data.MySqlClient;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class UserProfile : Form
    {
        MySqlDataAdapter adapter;
        DataTable table;

        public UserProfile()
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
                        string query = $"SELECT * FROM Клиент WHERE НКл = {keyLbl.Text};";
                        con.Open();
                        cmd.CommandText = query;
                        table = new DataTable();
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(table);
                        DataRow row = table.Rows[0];
                        тбНКл.Text = row["НКл"].ToString();
                        тбФИО.Text = row["ФИО"].ToString();
                        тбПол.Text = row["Пол"].ToString();
                        тбДатаРождения.Text = row["ДатаРождения"].ToString();
                        тбEmail.Text = row["Email"].ToString();
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

        private void UserProfile_Load(object sender, EventArgs e)
        {
            FillTextBoxes();
            тбНКл.ReadOnly = true;
            this.BackColor = System.Drawing.Color.White;
        }

        private void updateInfo()
        {
            string message = string.Empty;
            try
            {
                using (var con = GetConnection())
                {
                    using (var cmdSelect = con.CreateCommand())
                    {
                        // Формируем запрос для получения текущих данных
                        cmdSelect.CommandText = "SELECT НКл, ФИО, Пол, ДатаРождения, Email FROM Клиент WHERE НКл = @НКлОлд";
                        cmdSelect.Parameters.Add(new MySqlParameter("@НКлОлд", MySqlDbType.Int32)
                        { Value = Convert.ToInt32(тбНКл.Text) });

                        con.Open();

                        // Выполняем SELECT
                        DataTable currentData = new DataTable();
                        using (var adapter = new MySqlDataAdapter(cmdSelect))
                        {
                            adapter.Fill(currentData);
                        }

                        if (currentData.Rows.Count == 0)
                        {
                            MessageBox.Show("Запись с указанным НКл не найдена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Получаем текущие данные из базы
                        DataRow currentRow = currentData.Rows[0];
                        string currentФИО = currentRow["ФИО"].ToString();
                        string currentПол = currentRow["Пол"].ToString();
                        DateTime currentДатаРождения = Convert.ToDateTime(currentRow["ДатаРождения"]);
                        string currentEmail = currentRow["Email"].ToString();

                        // Проверяем, изменились ли значения
                        if (тбФИО.Text == currentФИО &&
                            тбПол.Text == currentПол &&
                            Convert.ToDateTime(тбДатаРождения.Text) == currentДатаРождения &&
                            тбEmail.Text == currentEmail)
                        {
                            MessageBox.Show("Данные не изменились. Обновление не требуется", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Если изменения есть, выполняем UPDATE
                        using (var cmdUpdate = con.CreateCommand())
                        {
                            cmdUpdate.CommandText = "UPDATE Клиент" +
                            " SET НКл = @НКл, ФИО = @ФИО, Пол = @Пол, ДатаРождения = @ДатаРождения, Email = @Email" +
                            " WHERE НКл = @НКлОлд";

                            cmdUpdate.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                            { Value = Convert.ToInt32(тбНКл.Text) });

                            cmdUpdate.Parameters.Add(new MySqlParameter("@ФИО", MySqlDbType.VarChar, 255)
                            { Value = тбФИО.Text ?? (object)System.DBNull.Value });

                            cmdUpdate.Parameters.Add(new MySqlParameter("@Пол", MySqlDbType.VarChar, 255)
                            { Value = тбПол.Text ?? (object)System.DBNull.Value });

                            cmdUpdate.Parameters.Add(new MySqlParameter("@ДатаРождения", MySqlDbType.Date)
                            { Value = Convert.ToDateTime(тбДатаРождения.Text) });

                            cmdUpdate.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, 255)
                            { Value = тбEmail.Text ?? (object)System.DBNull.Value });

                            cmdUpdate.Parameters.Add(new MySqlParameter("@НКлОлд", MySqlDbType.Int32)
                            { Value = Convert.ToInt32(тбНКл.Text) });

                            int rowsAffected = cmdUpdate.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                MessageBox.Show("Обновление не произошло, не найден клиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show($"Ваша информация была изменена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
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

        private void updateButton_Click(object sender, EventArgs e)
        {
            updateInfo();
        }
    }
}
