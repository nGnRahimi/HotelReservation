using Domain.BaseRepository;

namespace Domain.Models.Prices
{
    public interface IRoomPriceRepository : IBaseRepository<RoomPrice , int>
    {
        void AddHistory(RoomPriceHistory history) ;
    }
}
