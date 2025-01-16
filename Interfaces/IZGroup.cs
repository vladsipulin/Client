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
    internal interface IZGroup
    {
        Task<Result<List<ZGroup>>> Get();
        //добавление
        Task<Result<int>> Add(ZGroup objOfTable);
        //удаление
        Task<Result<int>> Remove(ZGroup objOfTable, int НЗаселенияГруппы);
        //обновление
        Task<Result<int>> Update(ZGroup objOfTable, int НЗаселенияГруппыОлд);
        Task<int> FindExistingNumZaselenie(int НЗаселенияГруппы);

    }
}
