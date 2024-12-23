using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class UserRecover
    {
        public string ЭлПочта { get; set; }
        public string ФИО { get; set; }

        public UserRecover(string ЭлПочта = "<ЭлПочта>", string ФИО = "<ФИО>")
        {
            this.ЭлПочта = ЭлПочта;
            this.ФИО = ФИО;
        }
    }
}
