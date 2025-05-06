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

        public async Task<int> GetIdByRequest(int НС)
        {
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Портье WHERE НС = @НС";
                    cmd.Parameters.AddWithValue("@НС", НС);

                    await con.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result) > 0 ? НС : 0;
                }
            }
            catch (MySqlException)
            {
                return -2;
            }
            catch (Exception)
            {
                return -3;
            }
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

        public bool IsValidPassword(string password)
        {
            // Список предсказуемых паролей
            var predictablePasswords = new HashSet<string>
            {
                "123456", "123456789", "qwerty", "12345", "password",
                "12345678", "qwerty123", "1q2w3e", "111111", "1234567890"
            };

            // Проверка длины пароля
            if (password.Length < 16)
                return false;

            // Проверка на предсказуемые пароли
            if (predictablePasswords.Contains(password.ToLower()))
                return false;

            // Проверка на повторяющиеся или одинаковые символы
            if (HasRepeatingOrSequentialPatterns(password))
                return false;

            // Проверка наличия необходимых типов символов
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(c => "!@#$%^&*()-_=+[]{}|;:,.<>?".Contains(c));

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        // Вспомогательный метод для проверки повторяющихся или одинаковых символов
        public bool HasRepeatingOrSequentialPatterns(string password)
        {
            // Проверка на одинаковые символы (например, аааа или 1111)
            for (int i = 0; i < password.Length - 3; i++)
            {
                if (password[i] == password[i + 1] &&
                    password[i] == password[i + 2] &&
                    password[i] == password[i + 3])
                {
                    return true;
                }
            }

            // Проверка на повторяющиеся группы (например, 111222333)
            for (int groupSize = 2; groupSize <= password.Length / 2; groupSize++)
            {
                for (int i = 0; i <= password.Length - 2 * groupSize; i++)
                {
                    string group1 = password.Substring(i, groupSize);
                    string group2 = password.Substring(i + groupSize, groupSize);
                    if (group1 == group2)
                    {
                        return true;
                    }
                }
            }

            return false;
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
                    cmd.CommandText = "INSERT INTO Портье (НС, ФИО, Пол, ДатаРождения, Логин, Пароль, Email)" +
                        " VALUES(@НС, @ФИО, @Пол, @ДатаРождения, @Логин, @Пароль, @Email)";

                    cmd.Parameters.Add(new MySqlParameter("@НС", MySqlDbType.Int32)
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
