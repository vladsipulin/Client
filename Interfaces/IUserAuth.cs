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
        Task<Result<List<Employer>>> GetEmployer();
        Task<Result<List<UserRecover>>> GetClient();
        UserAuth FindUser(string email);
    }
}
