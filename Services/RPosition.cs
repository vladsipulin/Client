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
    internal class RPosition : IPosition
    {
        public RPosition() { }

        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        public async Task<Result<int>> AddPosition(Position objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Должность (НД, Название, Оклад, Занятость)" +
                        " VALUES(@НД, @Название, @Оклад, @Занятость)";

                    cmd.Parameters.Add(new MySqlParameter("@НД", MySqlDbType.Int32)
                    { Value = objOfTable.НД });

                    cmd.Parameters.Add(new MySqlParameter("@Название", MySqlDbType.VarChar, 50)
                    { Value = objOfTable.Название ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@Оклад", MySqlDbType.Float)
                    { Value = objOfTable.Оклад});

                    cmd.Parameters.Add(new MySqlParameter("@Занятость", MySqlDbType.Int32)
                    { Value = objOfTable.Занятость });

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

        public async Task<Result<List<Position>>> GetPosition()
        {
            var list = new List<Position>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Должность";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new Position(reader.GetInt32(0));
                            objOfTable.НД = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.Название = reader.IsDBNull(1) ? "null" : reader.GetString(1);
                            objOfTable.Оклад = reader.IsDBNull(2) ? 0 : reader.GetFloat(2);
                            objOfTable.Занятость = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<Position>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<Position>>(ex.Message);
            }

            return new Result<List<Position>>(list);
        }

        public async Task<Result<int>> RemovePosition(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Должность WHERE НД =@НД";

                    cmd.Parameters.Add(new MySqlParameter("@НД", MySqlDbType.Int32)
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

        public async Task<Result<int>> UpdatePosition(Position objOfTable, int НОргОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Должность" +
                        " SET НД = @val1, Название = @val2, Оклад = @val3, Занятость = @val4" +
                        " WHERE НД =@val5";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = objOfTable.НД });

                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.VarChar, 50)
                    { Value = objOfTable.Название ?? (object)System.DBNull.Value });

                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Float)
                    { Value = objOfTable.Оклад });

                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.Int32)
                    {
                        Value = objOfTable.Занятость
                    });

                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Int32)
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
