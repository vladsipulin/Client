using Client.Models;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    internal interface IReqOnService
    {
        Task<Result<List<ReqOnService>>> Get();
        //добавление
        Task<Result<int>> Add(ReqOnService objOfTable);
        //удаление
        Task<Result<int>> Remove(int НЗаявки, int НКл);
        //обновление
        Task<Result<int>> Update(ReqOnService objOfTable, int НЗаявкиОлд, int НКлОлд);
        Task<int> GetIdByRequest(int НЗаявки);
    }
}
