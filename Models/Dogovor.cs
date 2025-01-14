using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class Dogovor
    {
        public int НДоговора { get; set; }
        public int НОрг { get; set; }
        public int НГ { get; set; }
        public int НС { get; set; }
        public string ДатаНачала { get; set; }
        public string ДатаОкончания { get; set; }

        public Dogovor(int НДоговора = 0, int НОрг = 0, int НГ = 0, int НС = 0, DateTime ДатаНачала = default, DateTime ДатаОкончания = default)
        {
            this.НДоговора = НДоговора;
            this.НГ = НГ;
            this.НОрг = НОрг;
            this.НС = НС;
            this.ДатаНачала = ДатаНачала.ToShortDateString();
            this.ДатаОкончания = ДатаОкончания.ToShortDateString();
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="Dogovor">существующий экземпляр</param>
        /// <returns>клон существующего Dogovor</returns>
        public static Dogovor GetClone(Dogovor clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new Dogovor(clone.НДоговора)
            {
                НДоговора = clone.НДоговора,
                НОрг = clone.НОрг,
                НГ = clone.НГ,
                НС = clone.НС,
                ДатаНачала = clone.ДатаНачала,
                ДатаОкончания = clone.ДатаОкончания,
            };
        }

        public override string ToString()
        {
            return $"НДоговора: {НДоговора} НОрг: {НОрг} НГ: {НГ} НС: {НС} ДатаНачала: {ДатаНачала} ДатаОкончания: {ДатаОкончания}";
        }
    }
}
