using System;

namespace Client.Models
{
    internal class ReturnPaymentModel
    {
        public int НЗаявки { get; set; }
        public int НУслуги { get; set; }
        public int НКл { get; set; }
        public float СуммаВозврата { get; set; }
        public DateTime ДатаВозврата { get; set; }
        public int НС { get; set; }
        public string СпособВозврата { get; set; }

        public ReturnPaymentModel(
            int нЗаявки = 0,
            int нУслуги = 0,
            int нКл = 0,
            float суммаВозврата = 0,
            DateTime датаВозврата = default,
            int нС = 0,
            string способВозврата = null)
        {
            НЗаявки = нЗаявки;
            НУслуги = нУслуги;
            НКл = нКл;
            СуммаВозврата = суммаВозврата;
            ДатаВозврата = датаВозврата;
            НС = нС;
            СпособВозврата = способВозврата;
        }

        public static ReturnPaymentModel GetClone(ReturnPaymentModel clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new ReturnPaymentModel(
                clone.НЗаявки,
                clone.НУслуги,
                clone.НКл,
                clone.СуммаВозврата,
                clone.ДатаВозврата,
                clone.НС,
                clone.СпособВозврата);
        }

        public override string ToString()
        {
            return $"НЗаявки: {НЗаявки}, НУслуги: {НУслуги}, НКл: {НКл}, " +
                   $"СуммаВозврата: {СуммаВозврата}, ДатаВозврата: {ДатаВозврата:yyyy-MM-dd HH:mm:ss}," +
                   $"НС: {НС}, СпособВозврата: {СпособВозврата}";
        }
    }
}