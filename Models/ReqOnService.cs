using System;

namespace Client.Models
{
    internal class ReqOnService
    {
        public int НЗаявки { get; set; }
        public int НУслуги { get; set; } 
        public DateTime СрокОплаты { get; set; } 
        public int НКл { get; set; }
        public int? НТипаДоговора { get; set; } 
        public int? НОрг { get; set; } 
        public int Количество_Ед { get; set; }
        public float Сумма { get; set; }
        public DateTime ДатаЗаявки { get; set; } 
        public bool ПокупкаСовершена { get; set; }
        public int НС { get; set; } 
        public DateTime? ДатаОплаты { get; set; } 
        public float РазмерШтрафа { get; set; } 
        public float СуммаКОплате { get; set; } 

        public ReqOnService(
            int нЗаявки = 0,
            int нУслуги = 0,
            DateTime срокОплаты = default,
            int нКл = 0,
            int? нТипаДоговора = null,
            int? нОрг = null,
            int количество_Ед = 0,
            float сумма = 0,
            DateTime датаЗаявки = default,
            bool покупкаСовершена = false,
            int нС = 0,
            DateTime? датаОплаты = null,
            float размерШтрафа = 0,
            float суммаКОплате = 0)
        {
            НЗаявки = нЗаявки;
            НУслуги = нУслуги;
            СрокОплаты = срокОплаты;
            НКл = нКл;
            НТипаДоговора = нТипаДоговора;
            НОрг = нОрг;
            Количество_Ед = количество_Ед;
            Сумма = сумма;
            ДатаЗаявки = датаЗаявки;
            ПокупкаСовершена = покупкаСовершена;
            НС = нС;
            ДатаОплаты = датаОплаты;
            РазмерШтрафа = размерШтрафа;
            СуммаКОплате = суммаКОплате;
        }

        public static ReqOnService GetClone(ReqOnService clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new ReqOnService(
                clone.НЗаявки,
                clone.НУслуги,
                clone.СрокОплаты,
                clone.НКл,
                clone.НТипаДоговора,
                clone.НОрг,
                clone.Количество_Ед,
                clone.Сумма,
                clone.ДатаЗаявки,
                clone.ПокупкаСовершена,
                clone.НС,
                clone.ДатаОплаты,
                clone.РазмерШтрафа,
                clone.СуммаКОплате);
        }

        public override string ToString()
        {
            return $"НЗаявки: {НЗаявки}, НУслуги: {НУслуги}, СрокОплаты: {СрокОплаты:yyyy-MM-dd HH:mm:ss}, " +
                   $"НКл: {НКл}, НТипаДоговора: {НТипаДоговора ?? 0}, НОрг: {НОрг ?? 0}, " +
                   $"Количество_Ед: {Количество_Ед}, Сумма: {Сумма}, ДатаЗаявки: {ДатаЗаявки:yyyy-MM-dd HH:mm:ss}, " +
                   $"ПокупкаСовершена: {ПокупкаСовершена}, НС: {НС}, ДатаОплаты: {(ДатаОплаты.HasValue ? ДатаОплаты.Value.ToString("yyyy-MM-dd") : "null")}, " +
                   $"РазмерШтрафа: {РазмерШтрафа}, СуммаКОплате: {СуммаКОплате}";
        }
    }
}