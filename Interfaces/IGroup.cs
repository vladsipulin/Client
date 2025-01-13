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
    interface IGroup
    {
        Task<Result<List<Group>>> Get();
        //добавление
        Task<Result<int>> Add(Group objOfTable);
        //удаление
        Task<Result<int>> Remove(int НГр);
        //обновление
        Task<Result<int>> Update(Group objOfTable, int НГрОлд);
        Task<DataTable> GetOrgDetails(int НОрг);
        Task<int> FindExistingNumOfGroup(int НГр);
    }
}
