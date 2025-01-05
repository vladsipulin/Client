using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class RequestClientAppartmnts
    {
        //поля таблицы Клиент
        public int НЗаявки { get; set; }
        public int НКомнаты { get; set; }
        public int НК { get; set; }
        public int НЭ { get; set; }
        public int НГ { get; set; }
        public int НКл { get; set; }
        public string ДатаОплаты { get; set; }
        public string ДатаЗаселения { get; set; }
        public string ДатаВыезда { get; set; }
        public float СтоимостьОплаты { get; set; }
        public int RowNumber { get; set; }
        public int НЗаявкиОлд { get; set; }
        public int НКомнатыОлд { get; set; }
        public int НКОлд { get; set; }
        public int НЭОлд { get; set; }
        public int НГОлд { get; set; }

        public RequestClientAppartmnts(int НЗаявки = 0, int НКомнаты = 0, int НК = 0, int НЭ = 0, int НГ = 0, int НКл = 0, 
                                       DateTime ДатаОплаты = default, DateTime ДатаЗаселения = default, 
                                       DateTime ДатаВыезда = default, float СтоимостьОплаты = 0)
        {
            this.НЗаявки = НЗаявки;
            this.НКомнаты = НКомнаты;
            this.НК = НК;
            this.НЭ = НЭ;
            this.НГ = НГ;
            this.НКл = НКл;
            this.ДатаОплаты = ДатаОплаты.ToShortDateString();
            this.ДатаЗаселения = ДатаЗаселения.ToShortDateString();
            this.ДатаВыезда = ДатаВыезда.ToShortDateString();
            this.СтоимостьОплаты = СтоимостьОплаты;
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
        /// <param name="RequestClientAppartmnts">существующий экземпляр</param>
        /// <returns>клон существующего сотрудника</returns>
        public static RequestClientAppartmnts GetClone(RequestClientAppartmnts rca)
        {
            if (rca is null)
                throw new ArgumentNullException(nameof(rca));

            return new RequestClientAppartmnts(rca.НЗаявки)
            {
                НКомнаты = rca.НКомнаты,
                НК = rca.НК,
                НЭ = rca.НЭ,
                НГ = rca.НГ,
                НКл = rca.НКл,
                ДатаОплаты = rca.ДатаОплаты,
                ДатаЗаселения = rca.ДатаЗаселения,
                ДатаВыезда = rca.ДатаВыезда,
                СтоимостьОплаты = rca.СтоимостьОплаты,
            };
        }

        public override string ToString()
        {
            return $"НЗаявки: {НЗаявки} НКомнаты: {НКомнаты} НК: {НК} НЭ: {НЭ} НГ: {НГ} НКл: " +
                   $"{НКл} ДатаОплаты: {ДатаОплаты} ДатаЗаселения: {ДатаЗаселения} ДатаВыезда: {ДатаВыезда} СтоимостьОплаты: {СтоимостьОплаты}";
        }
    }
}
