using Client.Models;
using Client.Utils;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Services;
using Client.Interfaces;

namespace Client.Services
{
    internal class Recovery : IUserRecover
    {
        public Recovery()
        { }

        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        public UserRecover FindUser(string email)
        {
            var userWritten = new UserRecover(email);

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Клиент WHERE Email = @Email";

                    cmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, 255)
                    { Value = email });

                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var userInDB = new UserRecover(reader.GetString(6), reader.GetString(1));
                            userWritten.ЭлПочта = userInDB.ЭлПочта; 
                            userWritten.ФИО = userInDB.ФИО;
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new UserRecover(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new UserRecover(ex.Message);
            }

            return userWritten;
        }

        private string GetPasswordHash(string password)
        {
            string salt = "TfbcZEIwOHJokZyDIvOqjg==";
            //Console.WriteLine($"Salt: {salt}");

            string hashedPassword = PasswordHasher.HashPassword(password, salt);
            //Console.WriteLine($"Hashed Password: {hashedPassword}");
            return hashedPassword;
        }

        public Result<UserRecover> UpdatePasswordOfUser(UserRecover userRec, string newPassword)
        {
            newPassword = GetPasswordHash(newPassword);

            if (userRec is null)
                throw new ArgumentNullException(nameof(userRec));
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Клиент" +
                        " SET Пароль = @Пароль" +
                        " WHERE Email = @Email";

                    cmd.Parameters.Add(new MySqlParameter("@Пароль", MySqlDbType.VarChar, 255)
                    { Value = newPassword });

                    cmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, 255)
                    { Value = userRec.ЭлПочта });

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        return new Result<UserRecover>("Запись не найдена или не обновлена.");
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<UserRecover>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<UserRecover>(ex.Message);
            }

            return new Result<UserRecover>(userRec);
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
