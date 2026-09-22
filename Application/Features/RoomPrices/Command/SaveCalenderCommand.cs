
using Application.Common.MediatR;
using MediatR;

namespace Application.Features.RoomPrices.Command
{
    public class SaveCalenderCommand : BaseCommandRequest , IRequest<bool>
    {
        public List<PriceList> PriceLists { get; set; }
    }


    public class PriceList
    {
        public int RoomId { get; set; }
        public DateTime DateVal {  get; set; }
        public long Price { get; set; }
        public long BedPrice { get; set; }
    }




}
