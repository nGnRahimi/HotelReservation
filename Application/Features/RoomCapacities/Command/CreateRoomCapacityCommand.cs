

using Application.Common.MediatR;
using MediatR;

namespace Application.Features.RoomCapacites.Command
{
    public class CreateRoomCapacityCommand : BaseCommandRequest , IRequest<bool>
    {
        public string From { get; set; }
        public string To { get; set; }
        public int Qty { get; set; }
        public bool State {  get; set; }
        public List<int> Rooms { get; set; }
    }
}
