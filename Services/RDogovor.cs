using Client.Interfaces;
using Client.Models;
using Client.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    internal class RDogovor : IDogovor
    {
        public RDogovor() { }

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

        public async Task<Result<int>> Add(Dogovor objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO ДоговорСОрганизацией (НДоговора, НОрг, НГ, НС, ДатаНачала, ДатаОкончания)" +
                        " VALUES(@НДоговора, @НОрг, @НГ, @НС, @ДатаНачала, @ДатаОкончания)";

                    cmd.Parameters.Add(new MySqlParameter("@НДоговора", MySqlDbType.Int32)
                    { Value = objOfTable.НДоговора });

                    cmd.Parameters.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32)
                    { Value = objOfTable.НОрг });

                    cmd.Parameters.Add(new MySqlParameter("@НГ", MySqlDbType.Int32)
                    { Value = objOfTable.НГ });

                    cmd.Parameters.Add(new MySqlParameter("@НС", MySqlDbType.Int32)
                    { Value = objOfTable.НС });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаНачала", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаНачала) });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаОкончания", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаОкончания) });

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

        public async Task<Result<List<Dogovor>>> Get()
        {
            var list = new List<Dogovor>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM ДоговорСОрганизацией";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new Dogovor(reader.GetInt32(0));
                            objOfTable.НДоговора = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.НОрг = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            objOfTable.НГ = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            objOfTable.НС = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            objOfTable.ДатаНачала = reader.IsDBNull(4) ? "null" : reader.GetDateTime(4).ToShortDateString();
                            objOfTable.ДатаОкончания = reader.IsDBNull(5) ? "null" : reader.GetDateTime(5).ToShortDateString();
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<Dogovor>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<Dogovor>>(ex.Message);
            }

            return new Result<List<Dogovor>>(list);
        }

        public async Task<DataTable> GetOrgDetails(int НОрг)
        {
            var table = new DataTable();

            using (var con = GetConnection())
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Организация WHERE НОрг = @НОрг";
                cmd.Parameters.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32) { Value = НОрг });
                con.Open();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    table.Load(reader);
                }
            }

            return table;
        }

        public async Task<int> FindExistingNumOfDogovor(int НДоговора)
        {
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM ДоговорСОрганизацией WHERE НДоговора = @key";
                    cmd.Parameters.Add(new MySqlParameter("@key", MySqlDbType.Int32) { Value = НДоговора });

                    con.Open();
                    var result = await cmd.ExecuteScalarAsync();
                    con.Close();

                    return Convert.ToInt32(result) > 0 ? 1 : 0; // Если найдено, возвращаем 1, иначе 0
                }
            }
            catch (MySqlException)
            {
                return -2; // Ошибка подключения
            }
            catch (Exception)
            {
                return -3; // Общая ошибка
            }
        }

        public async Task<Result<int>> Remove(int НДоговора, int НОргОлд)
        {
            if (НДоговора <= 0)
                throw new ArgumentException(nameof(НДоговора));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"CALL СделатьДоговорНедействительным({НДоговора}, {НОргОлд})";

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

        public async Task<Result<int>> Update(Dogovor objOfTable, int НДоговораОлд, int НОргОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE ДоговорСОрганизацией" +
                        " SET НДоговора = @val1, НОрг = @val2, НГ = @val3, НС = @val4, ДатаНачала=@val5, ДатаОкончания=@val6 " +
                        " WHERE НДоговора = @val7 AND НОрг = @val8";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = objOfTable.НДоговора });

                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32)
                    { Value = objOfTable.НОрг });

                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Int32)
                    { Value = objOfTable.НГ });

                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.Int32)
                    { Value = objOfTable.НС });

                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаНачала) });

                    cmd.Parameters.Add(new MySqlParameter("@val6", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаОкончания) });

                    cmd.Parameters.Add(new MySqlParameter("@val7", MySqlDbType.Int32)
                    { Value = НДоговораОлд });

                    cmd.Parameters.Add(new MySqlParameter("@val8", MySqlDbType.Int32)
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
    }
}