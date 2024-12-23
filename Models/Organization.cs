using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class Organization
    {
        //поля таблицы "Организация"
        public int НОрг { get; set; }
        public string Наименование { get; set; }
        public string СфераДеятельности { get; set; }
        public string ДатаРегистрации { get; set; }
        public int OrderNumber { get; set; }
        public int НОргОлд { get; set; }

        public Organization(int НОрг = 0, string Наименование = "<Наименование>", 
                            string СфераДеятельности = "<СфераДеятельности>", DateTime ДатаРегистрации = default)
        {
            this.НОрг = НОрг;
            this.Наименование = Наименование;
            this.СфераДеятельности = СфераДеятельности;
            this.ДатаРегистрации = ДатаРегистрации.ToShortDateString();
        }

        /// <summary>
        /// Получение клонированного экземпляра
        /// </summary>
        /// <param name="organization">существующий экземпляр</param>
        /// <returns>клон существующего сотрудника</returns>
        public static Organization GetClone(Organization org)
        {
            if (org is null)
                throw new ArgumentNullException(nameof(org));

            return new Organization(org.НОрг)
            {
                Наименование = org.Наименование,
                СфераДеятельности = org.СфераДеятельности,
                ДатаРегистрации = org.ДатаРегистрации,
            };
        }

        public override string ToString()
        {
            return $"{НОрг}: {Наименование} {СфераДеятельности} {ДатаРегистрации}";
        }
    }
}
