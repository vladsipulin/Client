using Client.Models;
using Client.Utils;
using Client.Interfaces;
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
    internal class RReviewRoom : IReview
    {
        public RReviewRoom() { }

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

        public async Task<Result<int>> Add(Review objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO ОтзывКлиентаНаЗаселение (НЗаявки, НКл, Оценка)" +
                        " VALUES(@НЗаявки, @НКл, @Оценка)";

                    cmd.Parameters.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаявки });

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = objOfTable.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@Оценка", MySqlDbType.Int32)
                    { Value = objOfTable.Оценка });

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

        public async Task<Result<List<Review>>> Get()
        {
            var list = new List<Review>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM ОтзывКлиентаНаЗаселение";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new Review(reader.GetInt32(0));
                            objOfTable.НЗаявки = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.НКл = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            objOfTable.Оценка = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<Review>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<Review>>(ex.Message);
            }

            return new Result<List<Review>>(list);
        }

        public async Task<DataTable> GetByRequest(int НЗаявки, int НКл)
        {
            var table = new DataTable();

            using (var con = GetConnection())
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM ЗаявкаНаЗаселениеКлиента WHERE НЗаявки = @НЗаявки AND НКл = @НКл";
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

        public async Task<Result<int>> Remove(int НЗаявки, int НКл)
        {
            if (НЗаявки <= 0)
                throw new ArgumentException(nameof(НЗаявки));

            if (НКл <= 0)
                throw new ArgumentException(nameof(НКл));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM ОтзывКлиентаНаЗаселение WHERE НЗаявки =@val1 AND НКл = @val2";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = НЗаявки });

                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32)
                    { Value = НКл });

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

        public async Task<Result<int>> Update(Review objOfTable, int НЗаявкиОлд, int НКлОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE ОтзывКлиентаНаЗаселение" +
                        " SET НКл = @val1, НЗаявки = @val2, Оценка = @val3 " +
                        " WHERE НЗаявки = @val4 AND НКл = @val5";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = objOfTable.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаявки });

                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Int32)
                    { Value = objOfTable.Оценка });

                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.Int32)
                    { Value = НЗаявкиОлд });

                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Int32)
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
    }
}
