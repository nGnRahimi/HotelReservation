using Domain.BaseRepository;

namespace Domain.Models.Capacities
{
    public interface IRoomCapacityRepository : IBaseRepository<RoomCapacity, int>
    {
        void AddHistory(RoomCapacityHistory history);
    }
}