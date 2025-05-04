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
    internal class RReqOnService : IReqOnService
    {
        public RReqOnService() { }

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

        public async Task<Result<int>> Add(ReqOnService objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO ЗаявкаНаУслугу (НЗаявки, НСл, СрокОплаты, НКл, Количество_Ед, Сумма, ДатаЗаявки, ПокупкаСовершена)" +
                        " VALUES(@НЗаявки, @НСл, @СрокОплаты, @НКл, @Количество_Ед, @Сумма, @ДатаЗаявки, @ПокупкаСовершена)";

                    cmd.Parameters.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаявки });

                    cmd.Parameters.Add(new MySqlParameter("@НСл", MySqlDbType.Int32)
                    { Value = objOfTable.НСл });

                    cmd.Parameters.Add(new MySqlParameter("@СрокОплаты", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.СрокОплаты) });

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = objOfTable.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@Количество_Ед", MySqlDbType.Int32)
                    { Value = objOfTable.Количество_Ед });

                    cmd.Parameters.Add(new MySqlParameter("@Сумма", MySqlDbType.Float)
                    { Value = objOfTable.Сумма });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаЗаявки", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаЗаявки) });

                    cmd.Parameters.Add(new MySqlParameter("@ПокупкаСовершена", DbType.Boolean)
                    { Value = objOfTable.ПокупкаСовершена });

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

        public async Task<Result<List<ReqOnService>>> Get()
        {
            var list = new List<ReqOnService>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM ЗаявкаНаУслугу";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new ReqOnService(reader.GetInt32(0));
                            objOfTable.НЗаявки = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.НСл = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            objOfTable.СрокОплаты = reader.IsDBNull(2) ? "null" : reader.GetDateTime(2).ToShortDateString();
                            objOfTable.НКл = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            objOfTable.Количество_Ед = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                            objOfTable.Сумма = reader.IsDBNull(5) ? 0 : reader.GetFloat(5);
                            objOfTable.ДатаЗаявки = reader.IsDBNull(6) ? "null" : reader.GetDateTime(6).ToShortDateString();     
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<ReqOnService>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<ReqOnService>>(ex.Message);
            }

            return new Result<List<ReqOnService>>(list);
        }

        public async Task<int> GetIdByRequest(int НЗаявки)
        {
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM ЗаявкаНаУслугу WHERE НЗаявки = @НЗаявки";
                    cmd.Parameters.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = НЗаявки });

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


        public Task<Result<int>> Remove(int НЗаявки, int НКл)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> Update(ReqOnService objOfTable, int НЗаявкиОлд, int НКлОлд)
        {
            throw new NotImplementedException();
        }
    }
}
