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
    internal interface IUchetZayavokUslug
    {
        Task<Result<List<UchetZayavokUslug>>> Get();
        //добавление
        Task<Result<int>> Add(UchetZayavokUslug objOfTable);
        //удаление
        Task<Result<int>> Remove(UchetZayavokUslug objOfTable);
        //обновление
        Task<Result<int>> Update(UchetZayavokUslug objOfTable);
        Task<DataTable> GetByRequest(int НЗаявки, int НКл);
    }
}
