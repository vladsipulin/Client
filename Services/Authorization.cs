using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Utils;

namespace Client.Services
{
    internal class Authorization : IUserAuth
    {
        public Authorization()
        {
        }

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

        public async Task<Result<List<UserRecover>>> GetClient()
        {
            var list = new List<UserRecover>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT НКл, Email, ФИО
                        FROM Клиент";
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new UserRecover(
                                Id: reader.GetInt32(0),
                                ЭлПочта: reader.IsDBNull(1) ? null : reader.GetString(1),
                                ФИО: reader.IsDBNull(2) ? null : reader.GetString(2),
                                UserType: "Клиент"
                            );
                            list.Add(user);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Result<List<UserRecover>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<UserRecover>>(ex.Message);
            }

            return new Result<List<UserRecover>>(list);
        }

        public async Task<Result<List<Employer>>> GetEmployer()
        {
            var list = new List<Employer>();

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT НС, Логин, Пароль, Доступ
                        FROM Портье";
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new Employer(
                                ID: reader.GetInt32(0),
                                Логин: reader.IsDBNull(1) ? null : reader.GetString(1),
                                Пароль: reader.IsDBNull(2) ? null : reader.GetString(2),
                                UserType: reader.IsDBNull(3) ? "Базовый" : reader.GetString(3) 
                            );
                            list.Add(user);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Result<List<Employer>>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<List<Employer>>(ex.Message);
            }

            return new Result<List<Employer>>(list);
        }

        public async Task<Result<int>> AddUser(UserRecover userAuth)
        {
            if (userAuth is null)
                throw new ArgumentNullException(nameof(userAuth));

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    if (userAuth.UserType == "Клиент")
                    {
                        if (string.IsNullOrEmpty(userAuth.Логин) || string.IsNullOrEmpty(userAuth.Пароль))
                            return new Result<int>("Логин и Пароль обязательны для клиента.");

                        cmd.CommandText = @"
                            INSERT INTO Клиент (НКл, ФИО, Пол, ДатаРождения, Логин, Пароль, Email)
                            VALUES (@НКл, @ФИО, @Пол, @ДатаРождения, @Логин, @Пароль, @Email)";
                    }
                    else if (userAuth.UserType == "Портье")
                    {
                        cmd.CommandText = @"
                            INSERT INTO Портье (НС, ФИО, Пол, ДатаРождения, Логин, Пароль, Email)
                            VALUES (@НС, @ФИО, @Пол, @ДатаРождения, @Логин, @Пароль, @Email)";
                        cmd.Parameters.AddWithValue("@НС", userAuth.Id);
                    }
                    else
                    {
                        return new Result<int>("Не указан тип пользователя (Клиент или Портье).");
                    }

                    cmd.Parameters.AddWithValue("@НКл", userAuth.Id);
                    cmd.Parameters.AddWithValue("@ФИО", (object)userAuth.ФИО ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Пол", DBNull.Value); // Поле Пол отсутствует в UserRecover
                    cmd.Parameters.AddWithValue("@ДатаРождения", DBNull.Value); // Поле ДатаРождения отсутствует в UserRecover
                    cmd.Parameters.AddWithValue("@Логин", (object)userAuth.Логин ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Пароль", (object)userAuth.Пароль ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)userAuth.ЭлПочта ?? DBNull.Value);

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

        public async Task<Result<int>> RemoveUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id));

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Клиент WHERE НКл = @Id;
                        DELETE FROM Портье WHERE НС = @Id;";
                    cmd.Parameters.AddWithValue("@Id", id);

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

        public async Task<Result<int>> UpdateUser(UserRecover userAuth, int НКлОлд)
        {
            if (userAuth is null)
                throw new ArgumentNullException(nameof(userAuth));

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    if (userAuth.UserType == "Клиент")
                    {
                        if (string.IsNullOrEmpty(userAuth.Логин) || string.IsNullOrEmpty(userAuth.Пароль))
                            return new Result<int>("Логин и Пароль обязательны для клиента.");

                        cmd.CommandText = @"
                            UPDATE Клиент
                            SET НКл = @НКл, ФИО = @ФИО, Пол = @Пол, ДатаРождения = @ДатаРождения,
                                Логин = @Логин, Пароль = @Пароль, Email = @Email
                            WHERE НКл = @НКлОлд";
                    }
                    else if (userAuth.UserType == "Портье")
                    {
                        cmd.CommandText = @"
                            UPDATE Портье
                            SET НС = @НС, ФИО = @ФИО, Пол = @Пол, ДатаРождения = @ДатаРождения,
                                Логин = @Логин, Пароль = @Пароль, Email = @Email
                            WHERE НС = @НКлОлд";
                        cmd.Parameters.AddWithValue("@НС", userAuth.Id);
                    }
                    else
                    {
                        return new Result<int>("Не указан тип пользователя (Клиент или Портье).");
                    }

                    cmd.Parameters.AddWithValue("@НКл", userAuth.Id);
                    cmd.Parameters.AddWithValue("@ФИО", (object)userAuth.ФИО ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Пол", DBNull.Value); // Поле Пол отсутствует в UserRecover
                    cmd.Parameters.AddWithValue("@ДатаРождения", DBNull.Value); // Поле ДатаРождения отсутствует в UserRecover
                    cmd.Parameters.AddWithValue("@Логин", (object)userAuth.Логин ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Пароль", (object)userAuth.Пароль ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)userAuth.ЭлПочта ?? DBNull.Value);
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

        public UserAuth FindUser(string email)
        {
            var userWritten = new UserAuth(email);

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Клиент WHERE Email = @Email";

                    cmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, 255)
                    { Value = email });

                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var userInDB = new UserRecover(reader.GetString(5), reader.GetString(1), reader.GetInt32(0));
                            userWritten.Email = userInDB.ЭлПочта;
                            userWritten.ФИО = userInDB.ФИО;
                            userWritten.ID = userInDB.Id;
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                return new UserAuth(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new UserAuth(ex.Message);
            }

            return userWritten;
        }

        public Result<UserRecover> UpdatePasswordOfUser(UserRecover userRec, string newPassword)
        {
            if (userRec is null)
                return new Result<UserRecover>("Пользователь не указан.");

            if (string.IsNullOrEmpty(newPassword))
                return new Result<UserRecover>("Новый пароль не может быть пустым.");

            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    if (userRec.UserType == "Клиент")
                    {
                        cmd.CommandText = @"
                            UPDATE Клиент
                            SET Пароль = @Пароль
                            WHERE НКл = @Id";
                    }
                    else if (userRec.UserType == "Портье duel")
                    {
                        cmd.CommandText = @"
                            UPDATE Портье
                            SET Пароль = @Пароль
                            WHERE НС = @Id";
                    }
                    else
                    {
                        return new Result<UserRecover>("Не указан тип пользователя (Клиент или Портье).");
                    }

                    string hashedPassword = PasswordHasher.HashPassword(newPassword, userRec.Id);
                    cmd.Parameters.AddWithValue("@Пароль", hashedPassword);
                    cmd.Parameters.AddWithValue("@Id", userRec.Id);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        userRec.Пароль = hashedPassword;
                        return new Result<UserRecover>(userRec);
                    }
                    return new Result<UserRecover>("Пользователь не найден.");
                }
            }
            catch (MySqlException ex)
            {
                return new Result<UserRecover>(GetUserFriendlyErrorMessage(ex));
            }
            catch (Exception ex)
            {
                return new Result<UserRecover>(ex.Message);
            }
        }
    }
}