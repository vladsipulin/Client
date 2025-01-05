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
        Task<Result<int>> Remove(int НКомнаты, int НК, int НЭ, int НГ, int НКл);
        //обновление
        Task<Result<int>> Update(RequestClientAppartmnts objOfTable, int НКомнатыОлд, int НКОлд, int НЭОлд, int НГОлд);
    }
}
