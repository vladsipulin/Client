using Client.Models;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    internal interface IPosition
    {
        Task<Result<List<Position>>> GetPosition();
        //добавление
        Task<Result<int>> AddPosition(Position objOfTable);
        //удаление
        Task<Result<int>> RemovePosition(int id);
        //обновление
        Task<Result<int>> UpdatePosition(Position objOfTable, int НОргОлд);
    }
}
