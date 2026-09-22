using Domain.Models.Prices;
using Infrastructure.BaseRepository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class RoomPriceRepository : BaseRepository<RoomPrice, int>, IRoomPriceRepository
    {
        public RoomPriceRepository(ApplicationDbContext context) : base(context) 
        {
        }

        public void AddHistory(RoomPriceHistory history)
        {
          _context.RoomPriceHistories.Add(history);
        }
    }
}
