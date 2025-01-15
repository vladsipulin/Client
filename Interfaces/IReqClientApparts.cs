using Client.Models;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    internal interface IReqClientApparts
    {
        Task<Result<List<RequestClientAppartmnts>>> Get();
        //добавление
        Task<Result<int>> Add(RequestClientAppartmnts objOfTable);
        //удаление
        Task<Result<int>> Remove(RequestClientAppartmnts objOfTable, string Статус, int НКомнатыОлд, int НКОлд, int НЭОлд, int НГОлд, DateTime ДЗ, DateTime ДВ);
        //обновление
        Task<Result<int>> Update(RequestClientAppartmnts objOfTable, int НКомнатыОлд, int НКОлд, int НЭОлд, int НГОлд);
    }
}
