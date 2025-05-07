using System;

namespace Client.Models
{
    internal class UchetZayavokUslug
    {
        public int НЗаявки { get; set; }
        public int НКл { get; set; }
        public int НС { get; set; }
        public DateTime? ДатаОплаты { get; set; }
        public float РазмерШтрафа { get; set; }
        public float СуммаКОплате { get; set; }
        public int НУслуги { get; set; }
        public int Количество_Ед { get; set; }
        public float Сумма { get; set; }
        public DateTime СрокОплаты { get; set; }
        public DateTime ДатаЗаявки { get; set; }
        public bool ПокупкаСовершена { get; set; }
        public int? НТипаДоговора { get; set; }
        public int? НОрг { get; set; }

        // Конструктор по умолчанию
        public UchetZayavokUslug()
        {
        }

        // Конструктор для обратной совместимости со старым кодом
        public UchetZayavokUslug(int нЗаявки = 0, int нКл = 0, int нС = 0, DateTime датаОплаты = default, float размерШтрафа = 0, float суммаКОплате = 0, bool покупкаСовершена = false)
        {
            НЗаявки = нЗаявки;
            НКл = нКл;
            НС = нС;
            ДатаОплаты = датаОплаты == default ? null : датаОплаты;
            РазмерШтрафа = размерШтрафа;
            СуммаКОплате = суммаКОплате;
            ПокупкаСовершена = покупкаСовершена;
        }

        // Полный конструктор для нового функционала
        public UchetZayavokUslug(
            int нЗаявки,
            int нКл,
            int нС,
            DateTime? датаОплаты,
            float размерШтрафа,
            float суммаКОплате,
            int нУслуги,
            int количество_Ед,
            float сумма,
            DateTime срокОплаты,
            DateTime датаЗаявки,
            bool покупкаСовершена,
            int? нТипаДоговора = null,
            int? нОрг = null)
        {
            НЗаявки = нЗаявки;
            НКл = нКл;
            НС = нС;
            ДатаОплаты = датаОплаты;
            РазмерШтрафа = размерШтрафа;
            СуммаКОплате = суммаКОплате;
            НУслуги = нУслуги;
            Количество_Ед = количество_Ед;
            Сумма = сумма;
            СрокОплаты = срокОплаты;
            ДатаЗаявки = датаЗаявки;
            ПокупкаСовершена = покупкаСовершена;
            НТипаДоговора = нТипаДоговора;
            НОрг = нОрг;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="clone">существующий экземпляр</param>
        /// <returns>клон существующего UchetZayavokUslug</returns>
        public static UchetZayavokUslug GetClone(UchetZayavokUslug clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new UchetZayavokUslug
            {
                НЗаявки = clone.НЗаявки,
                НКл = clone.НКл,
                НС = clone.НС,
                ДатаОплаты = clone.ДатаОплаты,
                РазмерШтрафа = clone.РазмерШтрафа,
                СуммаКОплате = clone.СуммаКОплате,
                НУслуги = clone.НУслуги,
                Количество_Ед = clone.Количество_Ед,
                Сумма = clone.Сумма,
                СрокОплаты = clone.СрокОплаты,
                ДатаЗаявки = clone.ДатаЗаявки,
                ПокупкаСовершена = clone.ПокупкаСовершена,
                НТипаДоговора = clone.НТипаДоговора,
                НОрг = clone.НОрг
            };
        }

        public override string ToString()
        {
            return $"НЗаявки: {НЗаявки}, НКл: {НКл}, НС: {НС}, ДатаОплаты: {(ДатаОплаты.HasValue ? ДатаОплаты.Value.ToShortDateString() : "не указана")}, " +
                   $"РазмерШтрафа: {РазмерШтрафа}, СуммаКОплате: {СуммаКОплате}, НУслуги: {НУслуги}, Количество_Ед: {Количество_Ед}, " +
                   $"Сумма: {Сумма}, СрокОплаты: {СрокОплаты.ToShortDateString()}, ДатаЗаявки: {ДатаЗаявки.ToShortDateString()}, " +
                   $"ПокупкаСовершена: {(ПокупкаСовершена ? "Да" : "Нет")}, НТипаДоговора: {(НТипаДоговора.HasValue ? НТипаДоговора.ToString() : "не указан")}, " +
                   $"НОрг: {(НОрг.HasValue ? НОрг.ToString() : "не указана")}";
        }
    }
}