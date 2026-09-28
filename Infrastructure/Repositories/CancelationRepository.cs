
using Domain.Models.Cancelation;
using Infrastructure.BaseRepository;

namespace Infrastructure.Repositories
{
    public class CancelationRepository : BaseRepository<CancelationPolicy, int>, ICancelationRepository
    {
        public CancelationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddRule(CancelationRule rule)
        {
           _context.CancelationRules.Add(rule);
        }
    }
}
