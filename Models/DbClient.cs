using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Utils;

namespace Client.Models
{
    public class DbClient
    {
        //поля таблицы Клиент
        public int НКл { get; set; }
        public string ФИО { get; set; }
        public string Пол { get; set; }
        public string ДатаРождения { get; set; }

        //порядковый номер для отображения в DGV
        public int OrderNumber { get; set; }
        //для обновления НКл ищем его по старому НКл - НКлОлд
        public int НКлОлд { get; set; }
        public string Логин { get; set; }
        public string Пароль { get; set; }
        public string Email { get; set; }

        public DbClient(int НКл = 0, string ФИО = "<ФИО>", string Пол = "<Пол>", DateTime ДатаРождения = default)
        {
            this.НКл = НКл;
            this.ФИО = ФИО;
            this.Пол = Пол;
            this.ДатаРождения = ДатаРождения.ToShortDateString();
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="client">существующий экземпляр</param>
        /// <returns>клон существующего сотрудника</returns>
        public static DbClient GetClone(DbClient client)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));

            return new DbClient(client.НКл)
            {
                ФИО = client.ФИО,
                Пол = client.Пол,
                ДатаРождения = client.ДатаРождения,
            };
        }

        public override string ToString()
        {
            return $"{НКл}: {ФИО} {Пол} {ДатаРождения}";
        }
    }
}
