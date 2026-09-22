

namespace Application.Features.RoomCapacities.Dto
{
    public class RoomCapacityDto
    {
        public List<RoomCapacityName> Rooms { get; set; }
        public List<DateTime> Days { get; set; }   
    }

    public class RoomCapacityName
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public List<RoomCapacityList> RoomCapacities { get; set; }
    }

    public class RoomCapacityList
    {
        public DateTime DateVal { get; set; }
        public int Qty { get; set; }
        public bool State {  get; set; }
    }

}
