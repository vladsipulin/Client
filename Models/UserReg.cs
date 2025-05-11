using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class UserReg
    {
        //поля таблицы Клиент
        public int НКл { get; set; }
        public string ФИО { get; set; }
        public string Пол { get; set; }
        public string ДатаРождения { get; set; }
        public string Логин { get; set; }
        public string Пароль { get; set; }
        public string Email { get; set; }

        public UserReg(int НКл = 0, string ФИО = "<ФИО>", string Пол = "<Пол>", 
            DateTime ДатаРождения = default, string Логин = "<Логин>", string Пароль = "<Пароль>", string Email = "<Email>")
        {
            this.НКл = НКл;
            this.ФИО = ФИО;
            this.Пол = Пол;
            this.ДатаРождения = ДатаРождения.ToShortDateString();
            this.Логин = Логин;
            this.Пароль = PasswordHasher.HashPassword(Пароль, НКл);
            this.Email = Email;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="userAuth">существующий экземпляр</param>
        /// <returns>клон существующего сотрудника</returns>
        public static UserRecover GetClone(UserRecover userAuth)
        {
            if (userAuth is null)
                throw new ArgumentNullException(nameof(userAuth));

            return new UserRecover("","",userAuth.Id)
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
