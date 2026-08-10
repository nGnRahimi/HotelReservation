using Domain.Models.Hotels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Command
{
    internal class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, bool>
    {
        private readonly IHotelRepository _hotelrepository;
        public UpdateHotelCommandHandler(IHotelRepository hotelrepository)
        {
            _hotelrepository = hotelrepository; 
        }
        public async Task<bool> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelrepository.FindAsync(request.HotelId.Value);
            if (hotel is not null)
            { 
            hotel.Name = request.Name;
                hotel.Email = request.Email;
                hotel.City = request.City;
                hotel.Phone = request.Phone;
                hotel.Star = request.Star;
                hotel.State = request.State;
                hotel.Address = request.Address;
                hotel.Description = request.Description;

                _hotelrepository.Update(hotel);
                await _hotelrepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            return true;
        }
    }
}
