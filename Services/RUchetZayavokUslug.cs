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
    internal class RUchetZayavokUslug : IUchetZayavokUslug
    {
        public RUchetZayavokUslug() { }

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

        public async Task<Result<int>> Add(UchetZayavokUslug objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO УчетПокупокУслуг (НЗаявки, НКл, НС, ДатаОплаты, РазмерШтрафа, СуммаКОплате)" +
                    " VALUES(@НЗаявки, @НКл, @НС, @ДатаОплаты, @РазмерШтрафа, @СуммаКОплате)";

                    cmd.Parameters.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаявки });

                    cmd.Parameters.Add(new MySqlParameter("@НКл", MySqlDbType.Int32)
                    { Value = objOfTable.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@НС", MySqlDbType.Int32)
                    { Value = objOfTable.НС });

                    cmd.Parameters.Add(new MySqlParameter("@ДатаОплаты", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаОплаты) });

                    cmd.Parameters.Add(new MySqlParameter("@РазмерШтрафа", MySqlDbType.Float)
                    { Value = objOfTable.РазмерШтрафа });

                    cmd.Parameters.Add(new MySqlParameter("@СуммаКОплате", MySqlDbType.Float)
                    { Value = objOfTable.СуммаКОплате });

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

        public async Task<Result<List<UchetZayavokUslug>>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<DataTable> GetByRequest(int НЗаявки, int НКл)
        {
            var table = new DataTable();

            using (var con = GetConnection())
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM `заявканауслугу` WHERE НЗаявки = @НЗаявки AND НКл = @НКл ";
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

   

        public async Task<Result<int>> Update(UchetZayavokUslug objOfTable)
        {
            if (objOfTable is null)
                throw new ArgumentNullException(nameof(objOfTable));

            int result = 0;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE ЗаявкаНаУслугу" +
                        " SET НЗаявки = @val1, НКл = @val2, НС = @val3, ДатаОплаты = @val4, РазмерШтрафа = @val5, СуммаКОплате = @val6, ПокупкаСовершена = @val7" +
                        " WHERE НЗаявки = @val1 AND НКл = @val2";

                    cmd.Parameters.Add(new MySqlParameter("@val1", MySqlDbType.Int32)
                    { Value = objOfTable.НЗаявки });

                    cmd.Parameters.Add(new MySqlParameter("@val2", MySqlDbType.Int32)
                    { Value = objOfTable.НКл });

                    cmd.Parameters.Add(new MySqlParameter("@val3", MySqlDbType.Int32)
                    { Value = objOfTable.НС });

                    cmd.Parameters.Add(new MySqlParameter("@val4", MySqlDbType.Date)
                    { Value = Convert.ToDateTime(objOfTable.ДатаОплаты) });

                    cmd.Parameters.Add(new MySqlParameter("@val5", MySqlDbType.Float)
                    { Value = objOfTable.РазмерШтрафа });

                    cmd.Parameters.Add(new MySqlParameter("@val6", MySqlDbType.Float)
                    { Value = objOfTable.СуммаКОплате });

                    cmd.Parameters.Add(new MySqlParameter("@val7", DbType.Boolean)
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

        public Task<Result<int>> Remove(UchetZayavokUslug objOfTable)
        {
            throw new NotImplementedException();
        }
    }
}
