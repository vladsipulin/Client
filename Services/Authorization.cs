using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Utils;

namespace Client.Services
{
    internal class Authorization : IUserAuth
    {
        public Authorization()
        { }

        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        public async Task<Result<List<UserAuth>>> GetUser()
        {
            var list = new List<UserAuth>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Клиент";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new UserAuth(reader.GetInt32(0));
                            user.НКл = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); 
                            user.Логин = reader.IsDBNull(4) ? "null" : reader.GetString(4);
                            user.Пароль = reader.IsDBNull(5) ? "null" : reader.GetString(5);
                            list.Add(user);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<UserAuth>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<UserAuth>>(ex.Message);
            }

            return new Result<List<UserAuth>>(list);
        }

        public async Task<Result<int>> AddUser(UserAuth UserAuth)
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

        public async Task<Result<int>> RemoveUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Клиент WHERE НКл =@НКл";

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = id });

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

        public async Task<Result<int>> UpdateUser(UserAuth UserAuth, int НКлОлд)
        {
            if (UserAuth is null)
                throw new ArgumentNullException(nameof(UserAuth));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Клиент" +
                        " SET НКл = @НКл, Логин = @Логин, Пароль = @Пароль" +
                        " WHERE НКл =@НКлОлд";

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = UserAuth.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@Логин", MySqlDbType.VarChar, 255)
                    { Value = UserAuth.ФИО ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@Пароль", MySqlDbType.VarChar, 255)
                    { Value = UserAuth.Пароль ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@НКлОлд", MySqlDbType.Int32)
                    { Value = НКлОлд });

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
