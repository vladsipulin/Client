using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class ZaselenieClienta
    {
        public int НЗаявки { get; set; }
        public int НКл { get; set; }
        public int НС { get; set; }
        public string СтатусЗаявки { get; set; }

        public ZaselenieClienta(int НЗаявки = 0, int НКл = 0, int НС = 0, string СтатусЗаявки = "<Наименование>")
        {
            this.НЗаявки = НЗаявки;
            this.НКл = НКл;
            this.НС = НС;
            this.СтатусЗаявки = СтатусЗаявки;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="ZaselenieClienta">существующий экземпляр</param>
        /// <returns>клон существующего ZaselenieClienta</returns>
        public static ZaselenieClienta GetClone(ZaselenieClienta clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new ZaselenieClienta(clone.НЗаявки)
            {
                НЗаявки = clone.НЗаявки,
                НКл = clone.НКл,
                НС = clone.НС,
                СтатусЗаявки = clone.СтатусЗаявки,
            };
        }

        public override string ToString()
        {
            return $"НЗаявки: {НЗаявки} НКл: {НКл} НС: {НС} СтатусЗаявки: {СтатусЗаявки}";
        }
    }
}
