using Domain.Models.Hotels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Query
{
    public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, Hotel>
    {
        private readonly IHotelRepository _hotelrepository;      
        public GetHotelQueryHandler(IHotelRepository hotelrepository)
        {
            _hotelrepository = hotelrepository;
        }

        public async Task<Hotel> Handle(GetHotelQuery request, CancellationToken cancellationToken)
        {
            var data = await _hotelrepository.FindAsync(request.HotelId.Value);
            return data;
        }
    }
}
