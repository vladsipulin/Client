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
    internal class RZGroupRoom : IZGroupRoom
    {
        public RZGroupRoom() { }

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

        public async Task<Result<int>> Add(ZGroupRoom objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO КомнатыВЗаселенииГруппы (НЗаселенияГруппы, НГ, НК, НЭ, НКомнаты)" +
                    " VALUES(@НЗаселенияГруппы, @НГ, @НК, @НЭ, @НКомнаты)";

                    cmd.Parameters.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаселенияГруппы });

                    cmd.Parameters.Add(new MySqlParameter("@НГ", MySqlDbType.Int32)
                    { Value = objOfTable.НГ });

                    cmd.Parameters.Add(new MySqlParameter("@НК", MySqlDbType.Int32)
                    { Value = objOfTable.НК });

                    cmd.Parameters.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32)
                    { Value = objOfTable.НЭ });

                    cmd.Parameters.Add(new MySqlParameter("@НКомнаты", MySqlDbType.Int32)
                    { Value = objOfTable.НКомнаты });

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

        public async Task<Result<List<ZGroupRoom>>> Get()
        {
            var list = new List<ZGroupRoom>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM КомнатыВЗаселенииГруппы";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new ZGroupRoom(reader.GetInt32(0));
                            objOfTable.НЗаселенияГруппы = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.НГ = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            objOfTable.НК = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            objOfTable.НЭ = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            objOfTable.НКомнаты = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<ZGroupRoom>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<ZGroupRoom>>(ex.Message);
            }

            return new Result<List<ZGroupRoom>>(list);
        }

        public async Task<Result<int>> Remove(ZGroupRoom objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM КомнатыВЗаселенииГруппы " +
                        "WHERE НЗаселенияГруппы = @val1 AND НГ = @val2 AND НК = @val3 AND НЭ = @val4 AND НКомнаты = @val5";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаселенияГруппы });
                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32)
                    { Value = objOfTable.НГ });
                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Int32)
                    { Value = objOfTable.НК });
                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.Int32)
                    { Value = objOfTable.НЭ });
                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Int32)
                    { Value = objOfTable.НКомнаты });

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

        public async Task<Result<int>> Update(ZGroupRoom objOfTable, int НЗГОлд, int НГОлд, int НКОлд, int НЭОлд, int НКомОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {

                    cmd.CommandText = "UPDATE КомнатыВЗаселенииГруппы" +
                        " SET НГ = @val2, НК = @val3, НЭ = @val4, НКомнаты = @val5" +
                        " WHERE НЗаселенияГруппы = @val01 AND НГ = @val02 AND НК = @val03 AND НЭ = @val04 AND НКомнаты = @val05";

                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32)
                    { Value = objOfTable.НГ });

                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Int32)
                    { Value = objOfTable.НК });

                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.Int32)
                    { Value = objOfTable.НЭ });

                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Int32)
                    { Value = objOfTable.НКомнаты });

                    cmd.Parameters.Add(new MySqlParameter("@val01", MySqlDbType.Int32)
                    { Value = НЗГОлд });

                    cmd.Parameters.Add(new MySqlParameter("@val02", MySqlDbType.Int32)
                    { Value = НГОлд });

                    cmd.Parameters.Add(new MySqlParameter("@val03", MySqlDbType.Int32)
                    { Value = НКОлд });

                    cmd.Parameters.Add(new MySqlParameter("@val04", MySqlDbType.Int32)
                    { Value = НЭОлд });

                    cmd.Parameters.Add(new MySqlParameter("@val05", MySqlDbType.Int32)
                    { Value = НКомОлд });

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

        public async Task<DataTable> GetByRequest(int НЗаселенияГруппы)
        {

            var table = new DataTable();

            using (var con = GetConnection())
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM `комнатывзаселениигруппы` WHERE НЗаселенияГруппы = @НЗаселенияГруппы";
                cmd.Parameters.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32) { Value = НЗаселенияГруппы });
                con.Open();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    table.Load(reader);
                }
            }

            return table;

        }
    }
}
