using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Employer
    {
        public int ID { get; set; }
        public string ФИО { get; set; }
        public string Пол { get; set; }
        public string ДатаРождения { get; set; }
        public string Логин { get; set; }
        public string Пароль { get; set; }
        public string Email { get; set; }


        public Employer(int ID = 0, string Логин = "<Логин>", string Пароль = "<Пароль>", string ФИО = "<ФИО>", string Пол = "<Пол>", DateTime ДатаРождения = default, string Email = "<Email>")
        {
            this.ID = ID;
            this.ФИО = ФИО;
            this.Пол = Пол;
            this.ДатаРождения = ДатаРождения.ToShortDateString();
            this.Логин = Логин;
            this.Пароль = Пароль;
            this.Email = Email;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="client">существующий экземпляр</param>
        /// <returns>клон существующего сотрудника</returns>
        public static Employer GetClone(Employer client)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));

            return new Employer(client.ID)
            {
                ФИО = client.ФИО,
                Пол = client.Пол,
                ДатаРождения = client.ДатаРождения,
                Логин = client.Логин,
                Пароль = client.Пароль,
                Email = client.Email,
            };
        }

        public override string ToString()
        {
            return $"{ID}: {ФИО} {Пол} {ДатаРождения} {Логин} {Пароль} {Email}";
        }
    }
}
