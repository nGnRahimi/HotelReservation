using Domain.Models.Capacities;
using Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoomCapacityRepository : BaseRepository<RoomCapacity, int>, IRoomCapacityRepository
    {
        public RoomCapacityRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void AddHistory(RoomCapacityHistory history)
        {
            _context.RoomCapacityHistory.Add(history);
        }
    }
}
