

using Application.Common.MediatR;
using MediatR;

namespace Application.Features.RoomCapacites.Command
{
    public class SaveCapacityCalenderCommand : BaseCommandRequest , IRequest<bool>
    {
        public List<CapacityList> CapacityLists { get; set; }
    }

    public class CapacityList
    {
        public int RoomId { get; set; } 
        public DateTime DateVal { get; set; }
        public int Qty { get; set; }
        public bool State {  get; set; }
    }

}
