using Client.Models;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    internal interface IReturnPayment
    {
        Task<Result<int>> Update(ReturnPaymentModel objOfTable);
    }
}
