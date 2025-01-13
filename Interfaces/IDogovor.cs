using Client.Models;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    internal interface IDogovor
    {
        Task<Result<List<Dogovor>>> Get();
        //добавление
        Task<Result<int>> Add(Dogovor objOfTable);
        //удаление
        Task<Result<int>> Remove(int НДоговора);
        //обновление
        Task<Result<int>> Update(Dogovor objOfTable, int НДоговораОлд);
        Task<DataTable> GetOrgDetails(int НОрг);
        Task<int> FindExistingNumOfDogovor(int НДоговора);
    }
}
