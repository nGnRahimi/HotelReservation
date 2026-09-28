
using Domain.Models.Hotels;

namespace Domain.Models.Cancelation
{
    public class CancelationPolicy : BaseEntity<int>
    {
        public string Title { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<CancelationRule> CancelationRules { get; set; } = new List<CancelationRule>();

        public void AddCancelationRules(CancelationRule rule)
        {
            CancelationRules.Add(rule);
        }
    }
}
