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
    internal interface IReview
    {
        Task<Result<List<Review>>> Get();
        //добавление
        Task<Result<int>> Add(Review objOfTable);
        //удаление
        Task<Result<int>> Remove(int НЗаявки, int НКл);
        //обновление
        Task<Result<int>> Update(Review objOfTable, int НЗаявкиОлд, int НКлОлд);
        Task<DataTable> GetByRequest(int НЗаявки, int НКл);
    }
}
