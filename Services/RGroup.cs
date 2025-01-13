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
    internal class RGroup : IGroup
    {
        public RGroup() { }

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

        public async Task<Result<int>> Add(Group objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Группа (НГр, НОрг, ДатаРегистрации, ЧисленностьГруппы)" +
                        " VALUES(@НГр, @НОрг, @ДатаРегистрации, @ЧисленностьГруппы)";

                    cmd.Parameters.Add(new MySqlParameter("@НГр", MySqlDbType.Int32)
                    { Value = objOfTable.НГр });

                    cmd.Parameters.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32)
                    { Value = objOfTable.НОрг });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаРегистрации", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаРегистрации) });

                    cmd.Parameters.Add(new MySqlParameter("@ЧисленностьГруппы", MySqlDbType.Int32)
                    { Value = objOfTable.ЧисленностьГруппы });

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

        public async Task<Result<List<Group>>> Get()
        {
            var list = new List<Group>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Группа";
                    con.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var objOfTable = new Group(reader.GetInt32(0));
                            objOfTable.НГр = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            objOfTable.НОрг = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            objOfTable.ДатаРегистрации = reader.IsDBNull(2) ? "null" : reader.GetDateTime(2).ToShortDateString();
                            objOfTable.ЧисленностьГруппы = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            list.Add(objOfTable);
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new Result<List<Group>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<Group>>(ex.Message);
            }

            return new Result<List<Group>>(list);
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

        public async Task<int> FindExistingNumOfGroup(int НГр)
        {
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Группа WHERE НГр = @key";
                    cmd.Parameters.Add(new MySqlParameter("@key", MySqlDbType.Int32) { Value = НГр });

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

        public async Task<Result<int>> Remove(int НГр)
        {
            if (НГр <= 0)
                throw new ArgumentException(nameof(НГр));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Группа WHERE НГр = @val1";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = НГр });

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

        public async Task<Result<int>> Update(Group objOfTable, int НГрОлд)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Группа" +
                        " SET НГр = @val1, НОрг = @val2, ДатаРегистрации = @val3, ЧисленностьГруппы = @val4 " +
                        " WHERE НГр = @val5";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = objOfTable.НГр });

                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32)
                    { Value = objOfTable.НОрг });

                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаРегистрации) });

                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.Int32)
                    { Value = objOfTable.ЧисленностьГруппы });

                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Int32)
                    { Value = НГрОлд });

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
