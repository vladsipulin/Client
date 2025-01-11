using Client.Interfaces;
using Client.Models;
using Client.Utils;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    internal class RRequestClientApparts : IReqClientApparts
    {
        public RRequestClientApparts() { }

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

        public async Task<Result<int>> Add(RequestClientAppartmnts objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO ЗаявкаНаЗаселениеКлиента (НКомнаты, НК, НЭ, НГ, НКл, ДатаОплаты, ДатаЗаселения, ДатаВыезда, СтоимостьОплаты)" +
                        " VALUES(@НКомнаты, @НК, @НЭ, @НГ, @НКл, @ДатаОплаты, @ДатаЗаселения, @ДатаВыезда, @СтоимостьОплаты)";


                    cmd.Parameters.Add(new MySqlParameter("@НКомнаты", MySqlDbType.Int32)
                    { Value = objOfTable.НКомнаты });

                    cmd.Parameters.Add(new MySqlParameter("@НК", MySqlDbType.Int32)
                    { Value = objOfTable.НК });

                    cmd.Parameters.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32)
                    { Value = objOfTable.НЭ });

                    cmd.Parameters.Add(new MySqlParameter("@НГ", MySqlDbType.Int32)
                    { Value = objOfTable.НГ });

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = objOfTable.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаОплаты", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаОплаты) });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаЗаселения", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаЗаселения) });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаВыезда", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаВыезда) });

                    cmd.Parameters.Add(new MySqlParameter("@СтоимостьОплаты", MySqlDbType.Float)
                    { Value = objOfTable.СтоимостьОплаты });

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

        public async Task<Result<List<RequestClientAppartmnts>>> Get()
        {
            var list = new List<RequestClientAppartmnts>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM ЗаявкаНаЗаселениеКлиента";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new RequestClientAppartmnts(reader.GetInt32(0));
                            objOfTable.НКомнаты = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.НК = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            objOfTable.НЭ = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            objOfTable.НГ = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            objOfTable.НКл = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                            objOfTable.ДатаОплаты = reader.IsDBNull(5) ? "null" : reader.GetDateTime(5).ToShortDateString();
                            objOfTable.ДатаЗаселения = reader.IsDBNull(6) ? "null" : reader.GetDateTime(6).ToShortDateString();
                            objOfTable.ДатаВыезда = reader.IsDBNull(7) ? "null" : reader.GetDateTime(7).ToShortDateString();
                            objOfTable.СтоимостьОплаты = reader.IsDBNull(8) ? 0 : reader.GetFloat(8);
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<RequestClientAppartmnts>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<RequestClientAppartmnts>>(ex.Message);
            }

            return new Result<List<RequestClientAppartmnts>>(list);
        }

        public async Task<Result<int>> Remove(RequestClientAppartmnts objOfTable, string Статус, int НКомнатыОлд, int НКОлд, int НЭОлд, int НГОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                UPDATE ЗаявкаНаЗаселениеКлиента
                SET Статус = @val2
                WHERE НКомнаты = @val02 
                        AND НК = @val03 
                        AND НЭ = @val04 
                        AND НГ = @val05 
                        AND НКл = @val06";

                    // Параметры для обновления
                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.VarChar, 255) { Value = Статус });

                    // Условие WHERE
                    cmd.Parameters.Add(new MySqlParameter("@val02", MySqlDbType.Int32) { Value = НКомнатыОлд });
                    cmd.Parameters.Add(new MySqlParameter("@val03", MySqlDbType.Int32) { Value = НКОлд });
                    cmd.Parameters.Add(new MySqlParameter("@val04", MySqlDbType.Int32) { Value = НЭОлд });
                    cmd.Parameters.Add(new MySqlParameter("@val05", MySqlDbType.Int32) { Value = НГОлд });
                    cmd.Parameters.Add(new MySqlParameter("@val06", MySqlDbType.Int32) { Value = objOfTable.НКл });

                    await con.OpenAsync();
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


        public async Task<Result<int>> Update(RequestClientAppartmnts objOfTable, int НКомнатыОлд, int НКОлд, int НЭОлд, int НГОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                UPDATE ЗаявкаНаЗаселениеКлиента
                SET НКомнаты = @val2, НК = @val3, НЭ = @val4, НГ = @val5, НКл = @val6, 
                    ДатаОплаты = @val7, ДатаЗаселения = @val8, ДатаВыезда = @val9, СтоимостьОплаты = @val10
                WHERE НКомнаты = @val02 AND НК = @val03 AND НЭ = @val04 AND НГ = @val05 AND НКл = @val06";

                    // Параметры для обновления
                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32) { Value = objOfTable.НКомнаты });
                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Int32) { Value = objOfTable.НК });
                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.Int32) { Value = objOfTable.НЭ });
                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Int32) { Value = objOfTable.НГ });
                    cmd.Parameters.Add(new MySqlParameter("@val6", MySqlDbType.Int32) { Value = objOfTable.НКл });
                    cmd.Parameters.Add(new MySqlParameter("@val7", MySqlDbType.Date) { Value = objOfTable.ДатаОплаты != null ? Convert.ToDateTime(objOfTable.ДатаОплаты) : (object)DBNull.Value });
                    cmd.Parameters.Add(new MySqlParameter("@val8", MySqlDbType.Date) { Value = objOfTable.ДатаЗаселения != null ? Convert.ToDateTime(objOfTable.ДатаЗаселения) : (object)DBNull.Value });
                    cmd.Parameters.Add(new MySqlParameter("@val9", MySqlDbType.Date) { Value = objOfTable.ДатаВыезда != null ? Convert.ToDateTime(objOfTable.ДатаВыезда) : (object)DBNull.Value });
                    cmd.Parameters.Add(new MySqlParameter("@val10", MySqlDbType.Float) { Value = objOfTable.СтоимостьОплаты });

                    // Условие WHERE
                    cmd.Parameters.Add(new MySqlParameter("@val02", MySqlDbType.Int32) { Value = НКомнатыОлд });
                    cmd.Parameters.Add(new MySqlParameter("@val03", MySqlDbType.Int32) { Value = НКОлд });
                    cmd.Parameters.Add(new MySqlParameter("@val04", MySqlDbType.Int32) { Value = НЭОлд });
                    cmd.Parameters.Add(new MySqlParameter("@val05", MySqlDbType.Int32) { Value = НГОлд });
                    cmd.Parameters.Add(new MySqlParameter("@val06", MySqlDbType.Int32) { Value = objOfTable.НКл });

                    await con.OpenAsync();
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
