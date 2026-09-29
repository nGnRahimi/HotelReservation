

using Application.Common.Extentions;

namespace Application.Features.Cancelation.Dto
{
    public class PolicyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime From { get; set; }
        public string FromPersian { get { return From.ToPersianDate(); } }
        public DateTime To { get; set; }
        public string ToPersian { get { return To.ToPersianDate(); } }
        public List<RuleDto> Rules { get; set; }
    }

    public class RuleDto
    {
        public int Hours { get; set; }
        public int RefundPercent { get; set; }
    }



}
