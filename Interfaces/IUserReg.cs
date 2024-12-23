using Client.Models;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    internal interface IUserReg
    {
        void CheckIfUserExists(string login, string email);
        //добавление
        Task<Result<int>> AddNewUser(UserReg client);
    }
}
