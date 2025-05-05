using Client.Interfaces;
using Client.Models;
using Client.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace Client.Services
{
    internal class RReqOnService : IReqOnService
    {
        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        private string GetUserFriendlyErrorMessage(MySqlException ex)
        {
            var message = String.Empty;
            switch (ex.Number)
            {
                case 0:
                    if (ex.InnerException?.Message.Contains("Unknown") == true)
                    {
                        message = "Неверное название схемы или таблицы.";
                    }
                    else if (ex.InnerException?.Message.Contains("Access") == true)
                    {
                        message = "Неверное имя или пароль доступа.";
                    }
                    else
                    {
                        message = ex.Message;
                    }
                    break;
                case 1042:
                    message = "Сервер по указанному адресу не доступен.\nОшибка ожидания.";
                    break;
                case 1045:
                    message = "Неверное имя пользователя или пароль, \nпожалуйста, попробуйте еще раз.";
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

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO ЗаявкаНаУслугу (
                            НЗаявки, НУслуги, СрокОплаты, НКл, НТипаДоговора, НОрг,
                            Количество_Ед, Сумма, ДатаЗаявки, ПокупкаСовершена, НС,
                            ДатаОплаты, РазмерШтрафа, СуммаКОплате
                        ) VALUES (
                            @НЗаявки, @НУслуги, @СрокОплаты, @НКл, @НТипаДоговора, @НОрг,
                            @Количество_Ед, @Сумма, @ДатаЗаявки, @ПокупкаСовершена, @НС,
                            @ДатаОплаты, @РазмерШтрафа, @СуммаКОплате
                        )";

                    cmd.Parameters.AddWithValue("@НЗаявки", objOfTable.НЗаявки);
                    cmd.Parameters.AddWithValue("@НУслуги", objOfTable.НУслуги);
                    cmd.Parameters.AddWithValue("@СрокОплаты", objOfTable.СрокОплаты);
                    cmd.Parameters.AddWithValue("@НКл", objOfTable.НКл);
                    cmd.Parameters.AddWithValue("@НТипаДоговора", (object)objOfTable.НТипаДоговора ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@НОрг", (object)objOfTable.НОрг ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Количество_Ед", objOfTable.Количество_Ед);
                    cmd.Parameters.AddWithValue("@Сумма", objOfTable.Сумма);
                    cmd.Parameters.AddWithValue("@ДатаЗаявки", objOfTable.ДатаЗаявки);
                    cmd.Parameters.AddWithValue("@ПокупкаСовершена", objOfTable.ПокупкаСовершена);
                    cmd.Parameters.AddWithValue("@НС", objOfTable.НС);
                    cmd.Parameters.AddWithValue("@ДатаОплаты", (object)objOfTable.ДатаОплаты ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@РазмерШтрафа", objOfTable.РазмерШтрафа);
                    cmd.Parameters.AddWithValue("@СуммаКОплате", objOfTable.СуммаКОплате);

                    await con.OpenAsync();
                    int result = await cmd.ExecuteNonQueryAsync();
                    return new Result<int>(result);
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
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new ReqOnService
                            {
                                НЗаявки = reader.GetInt32("НЗаявки"),
                                НУслуги = reader.GetInt32("НУслуги"),
                                СрокОплаты = reader.GetDateTime("СрокОплаты"),
                                НКл = reader.GetInt32("НКл"),
                                НТипаДоговора = reader.IsDBNull(reader.GetOrdinal("НТипаДоговора")) ? (int?)null : reader.GetInt32("НТипаДоговора"),
                                НОрг = reader.IsDBNull(reader.GetOrdinal("НОрг")) ? (int?)null : reader.GetInt32("НОрг"),
                                Количество_Ед = reader.GetInt32("Количество_Ед"),
                                Сумма = reader.GetFloat("Сумма"),
                                ДатаЗаявки = reader.GetDateTime("ДатаЗаявки"),
                                ПокупкаСовершена = reader.GetBoolean("ПокупкаСовершена"),
                                НС = reader.GetInt32("НС"),
                                ДатаОплаты = reader.IsDBNull(reader.GetOrdinal("ДатаОплаты")) ? (DateTime?)null : reader.GetDateTime("ДатаОплаты"),
                                РазмерШтрафа = reader.GetFloat("РазмерШтрафа"),
                                СуммаКОплате = reader.GetFloat("СуммаКОплате")
                            };
                            list.Add(objOfTable);
                        }
                    }
                }
                return new Result<List<ReqOnService>>(list);
            }
            catch (MySqlException ex)
            {
                return new Result<List<ReqOnService>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<ReqOnService>>(ex.Message);
            }
        }

        public async Task<int> GetIdByRequest(int НЗаявки)
        {
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM ЗаявкаНаУслугу WHERE НЗаявки = @НЗаявки";
                    cmd.Parameters.AddWithValue("@НЗаявки", НЗаявки);

                    await con.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result) > 0 ? НЗаявки : 0;
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

        public async Task<Result<int>> Remove(int НЗаявки, int НУслуги, int НКл)
        {
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM ЗаявкаНаУслугу WHERE НЗаявки = @НЗаявки AND НУслуги = @НУслуги AND НКл = @НКл";
                    cmd.Parameters.AddWithValue("@НЗаявки", НЗаявки);
                    cmd.Parameters.AddWithValue("@НУслуги", НУслуги);
                    cmd.Parameters.AddWithValue("@НКл", НКл);

                    await con.OpenAsync();
                    int result = await cmd.ExecuteNonQueryAsync();
                    return new Result<int>(result);
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
        }

        public async Task<Result<int>> Update(ReqOnService objOfTable, int НЗаявкиОлд, int НУслугиОлд, int НКлОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE ЗаявкаНаУслугу SET
                            НЗаявки = @НЗаявки, НУслуги = @НУслуги, СрокОплаты = @СрокОплаты, НКл = @НКл,
                            НТипаДоговора = @НТипаДоговора, НОрг = @НОрг, Количество_Ед = @Количество_Ед,
                            Сумма = @Сумма, ДатаЗаявки = @ДатаЗаявки, ПокупкаСовершена = @ПокупкаСовершена,
                            НС = @НС, ДатаОплаты = @ДатаОплаты, РазмерШтрафа = @РазмерШтрафа, СуммаКОплате = @СуммаКОплате
                        WHERE НЗаявки = @НЗаявкиОлд AND НУслуги = @НУслугиОлд AND НКл = @НКлОлд";

                    cmd.Parameters.AddWithValue("@НЗаявки", objOfTable.НЗаявки);
                    cmd.Parameters.AddWithValue("@НУслуги", objOfTable.НУслуги);
                    cmd.Parameters.AddWithValue("@СрокОплаты", objOfTable.СрокОплаты);
                    cmd.Parameters.AddWithValue("@НКл", objOfTable.НКл);
                    cmd.Parameters.AddWithValue("@НТипаДоговора", (object)objOfTable.НТипаДоговора ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@НОрг", (object)objOfTable.НОрг ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Количество_Ед", objOfTable.Количество_Ед);
                    cmd.Parameters.AddWithValue("@Сумма", objOfTable.Сумма);
                    cmd.Parameters.AddWithValue("@ДатаЗаявки", objOfTable.ДатаЗаявки);
                    cmd.Parameters.AddWithValue("@ПокупкаСовершена", objOfTable.ПокупкаСовершена);
                    cmd.Parameters.AddWithValue("@НС", objOfTable.НС);
                    cmd.Parameters.AddWithValue("@ДатаОплаты", (object)objOfTable.ДатаОплаты ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@РазмерШтрафа", objOfTable.РазмерШтрафа);
                    cmd.Parameters.AddWithValue("@СуммаКОплате", objOfTable.СуммаКОплате);
                    cmd.Parameters.AddWithValue("@НЗаявкиОлд", НЗаявкиОлд);
                    cmd.Parameters.AddWithValue("@НУслугиОлд", НУслугиОлд);
                    cmd.Parameters.AddWithValue("@НКлОлд", НКлОлд);

                    await con.OpenAsync();
                    int result = await cmd.ExecuteNonQueryAsync();
                    return new Result<int>(result);
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
        }
    }
}