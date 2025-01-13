using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class Group
    {
        public int НГр { get; set; }
        public int НОрг { get; set; }
        public string ДатаРегистрации { get; set; }
        public int ЧисленностьГруппы { get; set; }

        public Group(int НГр = 0, int НОрг = 0, DateTime ДатаРегистрации = default, int ЧисленностьГруппы = 0)
        {
            this.НГр = НГр;
            this.НОрг = НОрг;
            this.ДатаРегистрации = ДатаРегистрации.ToShortDateString();
            this.ЧисленностьГруппы = ЧисленностьГруппы;
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="Group">существующий экземпляр</param>
        /// <returns>клон существующего Group</returns>
        public static Group GetClone(Group clone)
        {
            if (clone is null)
                throw new ArgumentNullException(nameof(clone));

            return new Group(clone.НГр)
            {
                НГр = clone.НГр,
                НОрг = clone.НОрг,
                ДатаРегистрации = clone.ДатаРегистрации,
                ЧисленностьГруппы = clone.ЧисленностьГруппы,
            };
        }

        public override string ToString()
        {
            return $"НГр: {НГр} НОрг: {НОрг} ДатаРегистрации: {ДатаРегистрации} ЧисленностьГруппы: {ЧисленностьГруппы}";
        }
    }
}
