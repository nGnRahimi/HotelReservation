
using Domain.BaseRepository;

namespace Domain.Models.Cancelation
{
    public interface ICancelationRepository : IBaseRepository<CancelationPolicy , int>
    {
        void AddRule(CancelationRule rule);

    }
}
