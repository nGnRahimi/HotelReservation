using Domain.Models.Reservation;
using Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReserveRepository : BaseRepository<Reserve, int>, IReserveRepository
    {
        public ReserveRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
