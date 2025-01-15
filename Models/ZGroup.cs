using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class ZGroup
    {
        public int НЗаселенияГруппы { get; set; }
        public int НДоговора { get; set; }
        public int НОрг { get; set; }
        public int НГр { get; set; }
        public int НС { get; set; }
        public string ДатаОплаты { get; set; }
        public string ДатаЗаселения { get; set; }
        public string ДатаВыезда { get; set; }
        public float СтоимостьОплаты { get; set; }
        public string Статус { get; set; }

        public ZGroup(int НЗаселенияГруппы = 0, int НДоговора = 0, int НОрг = 0, int НГр = 0, int НС = 0, 
                        DateTime ДатаОплаты = default, DateTime ДатаЗаселения = default,
                        DateTime ДатаВыезда = default, float СтоимостьОплаты = 0, string Статус = "<Статус>")
        {
            this.НЗаселенияГруппы = НЗаселенияГруппы;
            this.НДоговора = НДоговора;
            this.НОрг = НОрг;
            this.НГр = НГр;
            this.НС = НС;
            this.ДатаОплаты = ДатаОплаты.ToShortDateString();
            this.ДатаЗаселения = ДатаЗаселения.ToShortDateString();
            this.ДатаВыезда = ДатаВыезда.ToShortDateString();
            this.СтоимостьОплаты = СтоимостьОплаты;
            this.Статус = Статус;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="ZGroup">существующий экземпляр</param>
        /// <returns>клон существующего ZGroup</returns>
        public static ZGroup GetClone(ZGroup clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new ZGroup(clone.НЗаселенияГруппы)
            {
                НЗаселенияГруппы = clone.НЗаселенияГруппы,
                НДоговора = clone.НДоговора,
                НОрг = clone.НОрг,
                НГр = clone.НГр,
                НС = clone.НС,
                ДатаОплаты = clone.ДатаОплаты,
                ДатаЗаселения = clone.ДатаЗаселения,
                ДатаВыезда = clone.ДатаВыезда,
                СтоимостьОплаты = clone.СтоимостьОплаты,
                Статус = clone.Статус,
            };
        }

        public override string ToString()
        {
            return $"НЗаселенияГруппы: {НЗаселенияГруппы} НДоговора: {НДоговора} " +
                $"НОрг: {НОрг} НГр: {НГр} НС: {НС} ДатаОплаты: {ДатаОплаты} ДатаЗаселения: {ДатаЗаселения}" +
                $"ДатаВыезда: {ДатаВыезда} СтоимостьОплаты: {СтоимостьОплаты} Статус: {Статус}";
        }
    }
}
