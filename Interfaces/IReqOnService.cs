using Client.Models;
using Client.Utils;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    internal interface IReqOnService
    {
        Task<Result<List<ReqOnService>>> Get();
        Task<Result<int>> Add(ReqOnService objOfTable);
        Task<Result<int>> Remove(int НЗаявки, int НУслуги, int НКл); 
        Task<Result<int>> Update(ReqOnService objOfTable, int НЗаявкиОлд, int НУслугиОлд, int НКлОлд); 
        Task<int> GetIdByRequest(int НЗаявки);
    }
}