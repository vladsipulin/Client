using Client.Models;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface IUserAuth
    {
        //получение всех
        Task<Result<List<UserAuth>>> GetUser();
        //добавление
        Task<Result<int>> AddUser(UserAuth client);
        //удаление
        Task<Result<int>> RemoveUser(int id);
        //обновление
        Task<Result<int>> UpdateUser(UserAuth client, int НКлОлд);
    }
}
