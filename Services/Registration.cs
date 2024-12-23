using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Utils;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Client.Services
{
    internal class Registration : IUserReg
    {
        public Registration() { }

        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        public void CheckIfUserExists(string login, string email)
        {
            string message = String.Empty;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Клиент WHERE Email = @Email OR Логин = @Логин";

                    cmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, 255)
                    { Value = email });
                    cmd.Parameters.Add(new MySqlParameter("@Логин", MySqlDbType.VarChar, 255)
                    { Value = login });

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Клиент с указанным логином или почтой уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public async Task<Result<int>> AddNewUser(UserReg UserAuth)
        {
            if (UserAuth is null)
                throw new ArgumentNullException(nameof(DbClient));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Клиент (НКл, ФИО, Пол, ДатаРождения, Логин, Пароль, Email)" +
                        " VALUES(@НКл, @ФИО, @Пол, @ДатаРождения, @Логин, @Пароль, @Email)";

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = UserAuth.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@ФИО", MySqlDbType.VarChar, 200)
                    { Value = UserAuth.ФИО ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@Пол", MySqlDbType.VarChar, 300)
                    { Value = UserAuth.Пол ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаРождения", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(UserAuth.ДатаРождения) });

                    cmd.Parameters.Add(new MySqlParameter("@Логин", MySqlDbType.VarChar, 255)
                    { Value = UserAuth.Логин ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@Пароль", MySqlDbType.VarChar, 255)
                    { Value = UserAuth.Пароль ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, 255)
                    { Value = UserAuth.Email ?? (object)System.DBNull.Value });

                    con.Open();
                    result = await cmd.ExecuteNonQueryAsync();
                }

            }
            catch (MySqlException ex)
            {
                return new Result<int>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<int>(ex.Message);
            }

            return new Result<int>(result);
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
    }
}
