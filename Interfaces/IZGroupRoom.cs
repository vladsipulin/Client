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
    internal interface IZGroupRoom 
    {
        Task<Result<List<ZGroupRoom>>> Get();
        Task<DataTable> GetByRequest(int НЗаселенияГруппы);
        //добавление
        Task<Result<int>> Add(ZGroupRoom objOfTable);
        //удаление
        Task<Result<int>> Remove(int НЗаселенияГруппы);
        //обновление
        Task<Result<int>> Update(ZGroupRoom objOfTable);

    }
}
