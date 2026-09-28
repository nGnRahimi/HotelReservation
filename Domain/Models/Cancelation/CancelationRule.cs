

namespace Domain.Models.Cancelation
{
    public class CancelationRule : BaseEntity<int>
    {
        public int Hours { get; set; }
        public int RefundPercent { get; set; }

        public int CancelationPolicyId { get; set; }
        public CancelationPolicy CancelationPolicy { get; set; }
    }
}
