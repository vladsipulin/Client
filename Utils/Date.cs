using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utils
{
    public class Date
    {
        private string _date; // Приватное поле для хранения значения итоговой даты
        public string year { get; private set; }
        public string month { get; private set; }
        public string day { get; private set; }

        public string date
        {
            get { return _date; }
            private set { _date = value; }
        }

        public Date(DateTime date)
        {
            this.year = date.Year.ToString();
            this.month = date.Month.ToString().PadLeft(2, '0'); // Добавляем ведущие нули
            this.day = date.Day.ToString().PadLeft(2, '0');     // Добавляем ведущие нули
            this.date = $"{this.year}-{this.month}-{this.day}";
        }
    }
}
