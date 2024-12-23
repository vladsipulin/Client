using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Services;

namespace Client.Models
{
    public class UserAuth : DbClient
    {
        //поля таблицы Клиент
        public string Логин { get; set; }
        public string Пароль { get; set; }
        public string Email { get; set; }

        public UserAuth(int НКл = 0, string Логин = "<Логин>", string Пароль = "<Пароль>")
        {
            this.НКл = НКл;
            this.Логин = Логин;
            this.Пароль = GetPasswordHash(Пароль);
        }

        private string GetPasswordHash(string password)
        {
            string salt = "TfbcZEIwOHJokZyDIvOqjg==";
            //Console.WriteLine($"Salt: {salt}");

            string hashedPassword = PasswordHasher.HashPassword(password, salt);
            //Console.WriteLine($"Hashed Password: {hashedPassword}");
            return hashedPassword;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="userAuth">существующий экземпляр</param>
        /// <returns>клон существующего сотрудника</returns>
        public static UserAuth GetClone(UserAuth userAuth)
        {
            if (userAuth is null)
                throw new ArgumentNullException(nameof(userAuth));

            return new UserAuth(userAuth.НКл)
            {
                Логин = userAuth.Логин,
                Пароль = userAuth.Пароль
            };
        }

        public override string ToString()
        {
            return $"{НКл}: login: {Логин} password: {Пароль}";
        }
    }
}
