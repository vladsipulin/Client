using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class UchetZayavokUslug
    {
        public int НЗаявки { get; set; }
        public int НКл { get; set; }
        public int НС { get; set; }
        public string ДатаОплаты { get; set; }
        public float РазмерШтрафа { get; set; }
        public float СуммаКОплате { get; set; }

        public UchetZayavokUslug(int НЗаявки = 0, int НКл = 0, int НС = 0, DateTime ДатаОплаты = default, float РазмерШтрафа = 0, float Сумма = 0)
        {
            this.НЗаявки = НЗаявки;
            this.НКл = НКл;
            this.НС = НС;
            this.ДатаОплаты = ДатаОплаты.ToShortDateString();
            this.РазмерШтрафа = РазмерШтрафа;
            this.СуммаКОплате = СуммаКОплате;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="UchetZayavokUslug">существующий экземпляр</param>
        /// <returns>клон существующего UchetZayavokUslug</returns>
        public static UchetZayavokUslug GetClone(UchetZayavokUslug clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new UchetZayavokUslug(clone.НЗаявки)
            {
                НЗаявки = clone.НЗаявки,
                НКл = clone.НКл,
                НС = clone.НС,
                ДатаОплаты = clone.ДатаОплаты,
                РазмерШтрафа = clone.РазмерШтрафа,
                СуммаКОплате = clone.СуммаКОплате,
            };
        }

        public override string ToString()
        {
            return $"НЗаявки: {НЗаявки} НКл: {НКл} НС: {НС}  ДатаОплаты: " +
                   $"{ДатаОплаты} РазмерШтрафа: {РазмерШтрафа} СуммаКОплате: {СуммаКОплате}";
        }
    }
}
