
namespace Application.Features.Search.Dto
{
    public class HotelInfoDto 
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Star { get; set; }
        public string Address { get; set; }
      public List<string> Images { get; set; }
    }
}
