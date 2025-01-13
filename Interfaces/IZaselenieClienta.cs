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
    internal interface IZaselenieClienta
    {
        Task<Result<List<ZaselenieClienta>>> Get();
        //добавление
        Task<Result<int>> Add(ZaselenieClienta objOfTable);
        //удаление
        Task<Result<int>> Remove(int НЗаявки, int НКл);
        //обновление
        Task<Result<int>> Update(ZaselenieClienta objOfTable, int НЗаявкиОлд, int НКлОлд);
        Task<DataTable> GetZayavkaDetails(int НЗаявки, int НКл);
    }
}
