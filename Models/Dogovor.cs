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
        public int НГ { get; set; }
        public int НОрг { get; set; }
        public int НС { get; set; }
        public string Наименование { get; set; }
        public int НВелСкидки { get; set; }
        public string ДатаНачала { get; set; }
        public string ДатаОкончания { get; set; }

        public Dogovor(int НДоговора = 0, int НГ = 0, int НОрг = 0, int НС = 0, string Наименование = "<Наименование>", int НВелСкидки = 0, DateTime ДатаНачала = default, DateTime ДатаОкончания = default)
        {
            this.НДоговора = НДоговора;
            this.НГ = НГ;
            this.НОрг = НОрг;
            this.НС = НС;
            this.Наименование = Наименование;
            this.НВелСкидки = НВелСкидки;
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
                НГ = clone.НГ,
                НОрг = clone.НОрг,
                НС = clone.НС,
                Наименование = clone.Наименование,
                НВелСкидки = clone.НВелСкидки,
                ДатаНачала = clone.ДатаНачала,
                ДатаОкончания = clone.ДатаОкончания,
            };
        }

        public override string ToString()
        {
            return $"НДоговора: {НДоговора} НГ: {НГ} НОрг: {НОрг} НС: {НС} Наименование: {Наименование} НВелСкидки: {НВелСкидки} ДатаНачала: {ДатаНачала} ДатаОкончания: {ДатаОкончания}";
        }
    }
}
