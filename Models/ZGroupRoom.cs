using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class ZGroupRoom
    {
        public int НЗаселенияГруппы { get; set; }
        public int НГ { get; set; }
        public int НК { get; set; }
        public int НЭ { get; set; }
        public int НКомнаты { get; set; }

        public ZGroupRoom(int НЗаселенияГруппы = 0, int НГ = 0, int НК = 0, int НЭ = 0, int НКомнаты = 0)
        {
            this.НЗаселенияГруппы = НЗаселенияГруппы;
            this.НГ = НГ;
            this.НК = НК;
            this.НЭ = НЭ;
            this.НКомнаты = НКомнаты;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="ZGroupRoom">существующий экземпляр</param>
        /// <returns>клон существующего ZGroupRoom</returns>
        public static ZGroupRoom GetClone(ZGroupRoom clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new ZGroupRoom(clone.НЗаселенияГруппы)
            {
                НЗаселенияГруппы = clone.НЗаселенияГруппы,
                НГ = clone.НГ,
                НК = clone.НК,
                НЭ = clone.НЭ,
                НКомнаты = clone.НКомнаты,
            };
        }

        public override string ToString()
        {
            return $"НЗаселенияГруппы: {НЗаселенияГруппы} НГ: {НГ} НК: {НК} НЭ: {НЭ} НКомнаты: {НКомнаты}";
        }
    }
}
