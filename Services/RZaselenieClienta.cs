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
    internal class RZaselenieClienta : IZaselenieClienta
    {
        public RZaselenieClienta() { }

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

        public async Task<Result<int>> Add(ZaselenieClienta objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO ЗаселениеКлиента (НЗаявки, НКл, НС, СтатусЗаявки)" +
                    " VALUES(@НЗаявки, @НКл, @НС, @СтатусЗаявки)";

                    cmd.Parameters.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаявки });

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = objOfTable.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@НС", MySqlDbType.Int32)
                    { Value = objOfTable.НС });

                    cmd.Parameters.Add(new MySqlParameter("@СтатусЗаявки", MySqlDbType.VarChar, 255)
                    { Value = objOfTable.СтатусЗаявки });

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

        public async Task<Result<List<ZaselenieClienta>>> Get()
        {
            var list = new List<ZaselenieClienta>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM ЗаселениеКлиента";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new ZaselenieClienta(reader.GetInt32(0));
                            objOfTable.НЗаявки = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.НКл = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            objOfTable.НС = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            objOfTable.СтатусЗаявки = reader.IsDBNull(3) ? "null" : reader.GetString(3);
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<ZaselenieClienta>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<ZaselenieClienta>>(ex.Message);
            }

            return new Result<List<ZaselenieClienta>>(list);
        }

        public async Task<DataTable> GetZayavkaDetails(int НЗаявки, int НКл)
        {
            var table = new DataTable();

            using (var con = GetConnection())
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM ЗаявкаНаЗаселениеКлиента WHERE НЗаявки = @НЗаявки AND НКл = @НКл ";
                cmd.Parameters.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = НЗаявки });
                cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
                con.Open();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    table.Load(reader);
                }
            }

            return table;
        }

        public async Task<Result<int>> Update(ZaselenieClienta objOfTable, int НЗаявкиОлд, int НКлОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE ЗаселениеКлиента" +
                        " SET НЗаявки = @val1, НКл = @val2, НС = @val3, СтатусЗаявки = @val4" +
                        " WHERE НЗаявки = @val5 AND НКл = @val6";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаявки });

                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32)
                    { Value = objOfTable.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Int32)
                    { Value = objOfTable.НС });

                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.VarChar, 255)
                    { Value = objOfTable.СтатусЗаявки });

                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Int32)
                    { Value = НЗаявкиОлд });

                    cmd.Parameters.Add(new MySqlParameter("@val6", MySqlDbType.Int32)
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

        public Task<Result<int>> Remove(int НЗаявки, int НКл)
        {
            throw new NotImplementedException();
        }
    }
}
