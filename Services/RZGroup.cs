using Client.Interfaces;
using Client.Models;
using Client.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    internal class RZGroup : IZGroup
    {
        public RZGroup() { }

        public async Task<Result<int>> Add(ZGroup objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand()) 
                {
                    cmd.CommandText = "INSERT INTO ЗаселениеГруппы (НЗаселенияГруппы, НДоговора, НОрг, НГр, НС, ДатаОплаты, ДатаЗаселения, ДатаВыезда, СтоимостьОплаты, Статус)" +
                    " VALUES(@НЗаселенияГруппы, @НДоговора, @НОрг, @НГр, @НС, @ДатаОплаты, @ДатаЗаселения, @ДатаВыезда, @СтоимостьОплаты, @Статус)";

                    cmd.Parameters.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаселенияГруппы });

                    cmd.Parameters.Add(new MySqlParameter("@НДоговора", MySqlDbType.Int32)
                    { Value = objOfTable.НДоговора });

                    cmd.Parameters.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32)
                    { Value = objOfTable.НОрг });

                    cmd.Parameters.Add(new MySqlParameter("@НГр", MySqlDbType.Int32)
                    { Value = objOfTable.НГр });

                    cmd.Parameters.Add(new MySqlParameter("@НС", MySqlDbType.Int32)
                    { Value = objOfTable.НС });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаОплаты", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаОплаты) });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаЗаселения", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаЗаселения) });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаВыезда", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаВыезда) });

                    cmd.Parameters.Add(new MySqlParameter("@СтоимостьОплаты", MySqlDbType.Float)
                    { Value = objOfTable.СтоимостьОплаты });

                    cmd.Parameters.Add(new MySqlParameter("@Статус", MySqlDbType.VarChar, 255)
                    { Value = objOfTable.Статус });


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

        public async Task<Result<List<ZGroup>>> Get()
        {
            var list = new List<ZGroup>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM ЗаселениеГруппы";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new ZGroup(reader.GetInt32(0));
                            objOfTable.НЗаселенияГруппы = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.НДоговора = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            objOfTable.НОрг = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            objOfTable.НГр = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            objOfTable.ДатаОплаты = reader.IsDBNull(4) ? "null" : reader.GetDateTime(4).ToShortDateString();
                            objOfTable.ДатаЗаселения = reader.IsDBNull(5) ? "null" : reader.GetDateTime(5).ToShortDateString();
                            objOfTable.ДатаВыезда = reader.IsDBNull(6) ? "null" : reader.GetDateTime(6).ToShortDateString();
                            objOfTable.СтоимостьОплаты = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
                            objOfTable.Статус = reader.IsDBNull(8) ? "null" : reader.GetString(8);
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<ZGroup>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<ZGroup>>(ex.Message);
            }

            return new Result<List<ZGroup>>(list);
        }

        public async Task<Result<int>> Remove(ZGroup objOfTable, int НЗаселенияГруппы)
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
                UPDATE ЗаселениеГруппы
                SET Статус = @val1
                WHERE НЗаселенияГруппы = @val01 ";

                    // Параметры для обновления
                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.VarChar, 255) { Value = objOfTable.Статус });

                    // Условие WHERE
                    cmd.Parameters.Add(new MySqlParameter("@val02", MySqlDbType.Int32) { Value = НЗаселенияГруппы });

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

        public async Task<Result<int>> Update(ZGroup objOfTable, int НЗаселенияГруппыОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {

                    cmd.CommandText = "UPDATE ЗаселениеГруппы" +
                        " SET НЗаселенияГруппы = @val1, НДоговора = @val2, НОрг = @val3, НГр = @val4," +
                        " НС = @val5, ДатаОплаты = @val6, ДатаЗаселения = @val7, ДатаВыезда = @val8," +
                        " СтоимостьОплаты = @val9, Статус = @val10" +
                        " WHERE НЗаселенияГруппы = @val11";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаселенияГруппы });

                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32)
                    { Value = objOfTable.НДоговора });

                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Int32)
                    { Value = objOfTable.НОрг });

                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.Int32)
                    { Value = objOfTable.НГр });

                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Int32)
                    { Value = objOfTable.НС });

                    cmd.Parameters.Add(new MySqlParameter("@val6", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаОплаты) });

                    cmd.Parameters.Add(new MySqlParameter("@val7", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаЗаселения) });

                    cmd.Parameters.Add(new MySqlParameter("@val8", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаВыезда) });

                    cmd.Parameters.Add(new MySqlParameter("@val9", MySqlDbType.Float)
                    { Value = objOfTable.СтоимостьОплаты });

                    cmd.Parameters.Add(new MySqlParameter("@val10", MySqlDbType.VarChar, 255)
                    { Value = objOfTable.Статус });

                    cmd.Parameters.Add(new MySqlParameter("@val11", MySqlDbType.Int32)
                    { Value = НЗаселенияГруппыОлд });

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

        public async Task<int> FindExistingNumZaselenie(int НЗаселенияГруппы)
        {
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM ЗаселениеГруппы WHERE НЗаселенияГруппы = @НЗаселенияГруппы";
                    cmd.Parameters.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32) { Value = НЗаселенияГруппы });

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
    }
}
