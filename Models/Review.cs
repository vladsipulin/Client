using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class Review
    {
        public int НЗаявки { get; set; }
        public int НКл { get; set; }
        public int Оценка { get; set; }

        public Review(int НЗаявки = 0, int НКл = 0, int Оценка = 0)
        {
            this.НЗаявки = НЗаявки;
            this.НКл = НКл;
            this.Оценка = Оценка;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="Review">существующий экземпляр</param>
        /// <returns>клон существующего сотрудника</returns>
        public static Review GetClone(Review clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new Review(clone.НЗаявки)
            {
                НЗаявки = clone.НЗаявки,
                НКл = clone.НКл,
                Оценка = clone.Оценка,
            };
        }

        public override string ToString()
        {
            return $"НЗаявки: {НЗаявки} НКл: {НКл} Оценка: {Оценка}";
        }
    }
}
