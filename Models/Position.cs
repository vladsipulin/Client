using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class Position
    {
        //поля таблицы "Должность"
        public int НД { get; set; }
        public string Название { get; set; }
        public float Оклад { get; set; }
        public int Занятость { get; set; }
        public int OrderNumber { get; set; }
        public int НДОлд { get; set; }

        public Position(int НД = 0, string Название = "<Название>",
                            float Оклад = (float)0.0, int Занятость = 0)
        {
            this.НД = НД;
            this.Название = Название;
            this.Оклад = Оклад;
            this.Занятость = Занятость;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="Position">существующий экземпляр</param>
        /// <returns>клон существующего сотрудника</returns>
        public static Position GetClone(Position objectOfTable)
        {
            if (objectOfTable is null)
                throw new ArgumentNullException(nameof(objectOfTable));

            return new Position(objectOfTable.НД)
            {
                Название = objectOfTable.Название,
                Оклад = objectOfTable.Оклад,
                Занятость = objectOfTable.Занятость,
            };
        }

        public override string ToString()
        {
            return $"{НД}: {Название} {Оклад} {Занятость}";
        }
    }
}
