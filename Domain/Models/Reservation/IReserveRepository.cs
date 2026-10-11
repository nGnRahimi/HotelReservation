using Domain.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Reservation
{
    public interface IReserveRepository : IBaseRepository<Reserve, int>
    {
    }
}
