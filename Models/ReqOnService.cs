using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class ReqOnService
    {
        //поля таблицы Клиент
        public int НЗаявки { get; set; }
        public int НСл { get; set; }
        public string СрокОплаты { get; set; }
        public int НКл { get; set; }
        public int Количество_Ед { get; set; }
        public float Сумма { get; set; }
        public string ДатаЗаявки { get; set; }
        public int RowNumber { get; set; }
        public int НЗаявкиОлд { get; set; }
        public int НКлОлд { get; set; }

        public ReqOnService(int НЗаявки = 0, int НСл = 0, DateTime СрокОплаты = default, int НКл = 0, int Количество_Ед = 0, float Сумма = 0, DateTime ДатаЗаявки = default)
        {
            this.НЗаявки = НЗаявки;
            this.НСл = НСл;
            this.СрокОплаты = СрокОплаты.ToShortDateString(); ;
            this.НКл = НКл;
            this.Количество_Ед = Количество_Ед;
            this.Сумма = Сумма;
            this.ДатаЗаявки = ДатаЗаявки.ToShortDateString();
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="ReqOnService">существующий экземпляр</param>
        /// <returns>клон существующего сотрудника</returns>
        public static ReqOnService GetClone(ReqOnService clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new ReqOnService(clone.НЗаявки)
            {
                НЗаявки = clone.НЗаявки,
                НСл = clone.НСл,
                СрокОплаты = clone.СрокОплаты,
                НКл = clone.НКл,
                Количество_Ед = clone.Количество_Ед,
                Сумма = clone.Сумма,
                ДатаЗаявки = clone.ДатаЗаявки,
            };
        }

        public override string ToString()
        {
            return $"НЗаявки: {НЗаявки} НСл: {НСл} СрокОплаты: {СрокОплаты}  НКл: " +
                   $"{НКл} Количество_ед: {Количество_Ед} Сумма: {Сумма} ДатаЗаявки: {ДатаЗаявки} ";
        }
    }
}
