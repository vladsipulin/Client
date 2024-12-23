using Client.Models;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    internal interface IOrganization
    {
        Task<Result<List<Organization>>> GetOrganization();
        //добавление
        Task<Result<int>> AddOrganization(Organization client);
        //удаление
        Task<Result<int>> RemoveOrganization(int id);
        //обновление
        Task<Result<int>> UpdateOrganization(Organization client, int НОргОлд);
    }
}
