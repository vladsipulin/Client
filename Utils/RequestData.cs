using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utils
{
    internal class RequestData
    {
        public int RequestId { get; set; } // НЗаявки
        public double TotalPrice { get; set; } // Сумма заявки
        public DateTime RequestDate { get; set; } // ДатаЗаявки
        public Dictionary<string, int> Services { get; set; } // Список услуг: Наименование -> Количество_Ед

        public RequestData(int requestId, double totalPrice, DateTime requestDate)
        {
            RequestId = requestId;
            TotalPrice = totalPrice;
            RequestDate = requestDate;
            Services = new Dictionary<string, int>();
        }

        public void AddService(string serviceName, int quantity)
        {
            if (Services.ContainsKey(serviceName))
            {
                Services[serviceName] += quantity;
            }
            else
            {
                Services[serviceName] = quantity;
            }
        }
    }
}
