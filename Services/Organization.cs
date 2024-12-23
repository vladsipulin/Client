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
    internal class Organization : IOrganization
    {
        public Organization()
        {
        }

        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        public async Task<Result<int>> AddOrganization(Models.Organization objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Организация (НОрг, Наименование, СфераДеятельности, ДатаРегистрации)" +
                        " VALUES(@НОрг, @Наименование, @СфераДеятельности, @ДатаРегистрации)";

                    cmd.Parameters.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32)
                    { Value = objOfTable.НОрг });

                    cmd.Parameters.Add(new MySqlParameter("@Наименование", MySqlDbType.VarChar, 200)
                    { Value = objOfTable.Наименование ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@СфераДеятельности", MySqlDbType.VarChar, 300)
                    { Value = objOfTable.СфераДеятельности ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаРегистрации", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаРегистрации) });

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

        public async Task<Result<List<Models.Organization>>> GetOrganization()
        {
            var list = new List<Models.Organization>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Организация";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new Models.Organization(reader.GetInt32(0));
                            objOfTable.НОрг = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.Наименование = reader.IsDBNull(1) ? "null" : reader.GetString(1);
                            objOfTable.СфераДеятельности = reader.IsDBNull(2) ? "null" : reader.GetString(2);
                            objOfTable.ДатаРегистрации = reader.IsDBNull(3) ? "null" : reader.GetDateTime(3).ToShortDateString();
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<Models.Organization>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<Models.Organization>>(ex.Message);
            }

            return new Result<List<Models.Organization>>(list);
        }

        public async Task<Result<int>> RemoveOrganization(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Организация WHERE НОрг =@НОрг";

                    cmd.Parameters.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32)
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

        public async Task<Result<int>> UpdateOrganization(Models.Organization objOfTable, int НОргОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Организация" +
                        " SET НОрг = @НОрг, Наименование = @Наименование, СфераДеятельности = @СфераДеятельности, ДатаРегистрации= @ДатаРегистрации" +
                        " WHERE НОрг =@НОргОлд";

                    cmd.Parameters.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32)
                    { Value = objOfTable.НОрг });

                    cmd.Parameters.Add(new MySqlParameter("@Наименование", MySqlDbType.VarChar, 200)
                    { Value = objOfTable.Наименование ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@СфераДеятельности", MySqlDbType.VarChar, 300)
                    { Value = objOfTable.СфераДеятельности ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаРегистрации", MySqlDbType.Date)
                    {
                        Value = Convert.ToDateTime(objOfTable.ДатаРегистрации)
                    });

                    cmd.Parameters.Add(new MySqlParameter("@НОргОлд", MySqlDbType.Int32)
                    { Value = НОргОлд });

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
