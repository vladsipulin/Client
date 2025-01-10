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

namespace Client.Services
{
    internal class MySqlRepository : IDbClientRepository
    {
        public MySqlRepository()
        { }

        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        public async Task<Result<List<DbClient>>> GetDbClients()
        {
            var list = new List<DbClient>();

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
                            var client = new DbClient(reader.GetInt32(0));
                            var clientPlus = new UserAuth(reader.GetInt32(0));
                            client.НКл = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); 
                            client.ФИО = reader.IsDBNull(1) ? "null" : reader.GetString(1);
                            client.Пол = reader.IsDBNull(2) ? "null" : reader.GetString(2);
                            client.ДатаРождения = reader.IsDBNull(3) ? "null" : reader.GetDateTime(3).ToShortDateString();
                            client.Логин = reader.IsDBNull(4) ? "null" : reader.GetString(4);
                            client.Пароль = reader.IsDBNull(5) ? "null" : reader.GetString(5);
                            client.Email = reader.IsDBNull(6) ? "null" : reader.GetString(6);
                            list.Add(client);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<DbClient>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<DbClient>>(ex.Message);
            }

            return new Result<List<DbClient>>(list);
        }

        public async Task<Result<int>> AddDbClient(DbClient DbClient)
        {
            if (DbClient is null)
                throw new ArgumentNullException(nameof(DbClient));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Клиент (НКл, ФИО, Пол, ДатаРождения, Email)" +
                        " VALUES(@НКл, @ФИО, @Пол, @ДатаРождения, @Email)";

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = DbClient.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@ФИО", MySqlDbType.VarChar, 255)
                    { Value = DbClient.ФИО ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@Пол", MySqlDbType.VarChar, 255)
                    { Value = DbClient.Пол ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаРождения", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(DbClient.ДатаРождения)});

                    cmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, 255)
                    { Value = Convert.ToDateTime(DbClient.Email) });

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

        public async Task<Result<int>> RemoveDbClient(int id)
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

        public async Task<Result<int>> UpdateDbClient(DbClient DbClient, int НКлОлд)
        {
            if (DbClient is null)
                throw new ArgumentNullException(nameof(DbClient));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Клиент" +
                        " SET НКл = @НКл, ФИО = @ФИО, Пол = @Пол, ДатаРождения = @ДатаРождения, Email = @Email" +
                        " WHERE НКл =@НКлОлд";

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = DbClient.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@ФИО", MySqlDbType.VarChar, 255)
                    { Value = DbClient.ФИО ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@Пол", MySqlDbType.VarChar, 255)
                    { Value = DbClient.Пол ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаРождения", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(DbClient.ДатаРождения) });

                    cmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, 255)
                    { Value = DbClient.Email ?? (object)System.DBNull.Value });

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
