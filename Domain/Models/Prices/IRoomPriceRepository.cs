using Domain.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Prices
{
    public interface IRoomPriceRepository : IBaseRepository<RoomPrice , int>
    {
    }
}
