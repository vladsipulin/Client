using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;
using Client.Utils;

namespace Client.Interfaces
{
    public interface IDbClientRepository
    {
        //получение всех
        Task<Result<List<DbClient>>> GetDbClients();
        //добавление
        Task<Result<int>> AddDbClient(DbClient client);
        //удаление
        Task<Result<int>> RemoveDbClient(int id);
        //обновление
        Task<Result<int>> UpdateDbClient(DbClient client, int НКлОлд);
    }
}
